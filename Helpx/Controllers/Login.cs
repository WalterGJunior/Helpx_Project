using DAL;
using DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Helpx.Controllers
{
    public class LoginController : PageBase
    {
        //
        // GET: /Base/

        public ActionResult Login()
        {
            return View();
        }       

        public ActionResult ValidarLogin(string email, string senha)
        {
            if (email.Trim() == string.Empty || senha.Trim() == string.Empty)
            {
                ViewBag.Validar = 0;
            }
            else
            {
                UsuariosDAO objDAO = new UsuariosDAO();
                tb_usuarios objUser = objDAO.ValidarLogin(email, senha);

                if (objUser != null)
                {
                    CodigoUsuarioLogado = objUser.id;
                    NomeUsuarioLogado = objUser.nome;
                    Response.Redirect("/Home/Home", false);

                }
                else
                {
                    ViewBag.Validar = -1;
                }

            }
            return View("Login");
        }

        public void Deslogar()
        {
            CodigoUsuarioLogado = 0;
            NomeUsuarioLogado = string.Empty;

            Response.Redirect("/Login/Login");
        }



        public ActionResult _Layout()
        {
            return View();
        }

    }
}
