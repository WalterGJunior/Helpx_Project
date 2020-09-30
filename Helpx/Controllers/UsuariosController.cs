using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL;

namespace Helpx.Controllers
{
    public class UsuariosController : Controller
    {
        //
        // GET: /Usuarios/
        public ActionResult Index()
        {
            if (Session["codUser"] == null || (int)Session["codUser"] == 0)
                return Redirect("/Login/Login");

            ViewBag.ListaUsuarios = new UsuariosDAO().ConsultarUsuarios();
            return View();
        }

        public ActionResult Usuarios(int id = 0)
        {
            var obj = new UsuariosVO();
            if (id > 0)
                obj = new UsuariosDAO().ConsultarUsuarios(id)[0];

            return View(obj);
        }
        
        //Método para Gravar Usuários
        [HttpPost]
        public ContentResult GravarUsuarios(FormCollection formData)
        {
            try
            {
                //Cria o Objeto DAO
                UsuariosDAO objDAO = new UsuariosDAO();

                //Cria o objeto para gravar
                tb_usuarios objUsuarios = new tb_usuarios();

                objUsuarios.id = Convert.ToInt32(formData["id"].ToString());
                objUsuarios.nome = formData["nome"].ToString();
                objUsuarios.senha = formData["senha"].ToString();
                objUsuarios.email = formData["email"].ToString();

                //Verifica se é uma inserção
                if (objUsuarios.id == 0)
                {
                    objDAO.InserirUsuarios(objUsuarios);
                }
                else
                {
                    objDAO.AlterarUsuarios(objUsuarios);
                }

            }
            catch (Exception ex)
            {
                return Content("{\"success\":false}", "application/json");
            }
            return Content("{\"success\":true}", "application/json");
        }

        [HttpPost]
        public ContentResult ExcluirUsuarios(int id)
        {
            try
            {
                if (id == 0)
                    return Content("{\"success\":false}", "application/json");

                UsuariosDAO objDAO = new UsuariosDAO();

                objDAO.ExcluirUsuarios(id);
            }
            catch (Exception ex)
            {
                return Content("{\"success\":false}", "application/json");
            }
            return Content("{\"success\":true}", "application/json");
        }

        public ActionResult AlterarClientes(int? id, string nome, string senha, string email)
        {
            ViewBag.CodigoCliente = id;
            ViewBag.NomeUsuario = nome;
            ViewBag.SenhaUsuario = senha;
            ViewBag.EmailUsuario = email;
            

            ConsultarUsuarios();
            return View("Usuarios");
        }

        private void ConsultarUsuarios()
        {
            UsuariosDAO objDAO = new UsuariosDAO();
            List<UsuariosVO> lstUser = objDAO.ConsultarUsuarios();

            ViewBag.ListaUsuarios = lstUser;
        }

    }
}
