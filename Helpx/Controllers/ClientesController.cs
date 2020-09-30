using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL;

namespace Helpx.Controllers
{
    public class ClientesController : PageBase
    {
        //
        // GET: /Clientes/
        public ActionResult Index()
        {
            if (Session["codUser"] == null || (int)Session["codUser"] == 0)
                return Redirect("/Login/Login");

            ViewBag.ListaClientes = new ClientesDAO().ConsultarClientes();
            return View();
        }

        public ActionResult Clientes(int id = 0)
        {
            var obj = new ClientesVO();
            if (id > 0)
                obj = new ClientesDAO().ConsultarClientes(id)[0];

            CarregarEstados();
            CarregarCidades();
            return View(obj);
        }

        [HttpPost]
        public ContentResult GravarClientes(FormCollection formData)
        {            
            try
            {
                //Cria o Objeto DAO
                ClientesDAO objDAO = new ClientesDAO();

                //Cria o objeto para gravar
                tb_clientes objClientes = new tb_clientes();

                objClientes.id = Convert.ToInt32(formData["id"].ToString());
                objClientes.cpf_cnpj = formData["cpf_cnpj"].ToString();
                objClientes.rg = formData["rg"].ToString();
                objClientes.nome = formData["nome"].ToString();
                objClientes.nome_fantasia = formData["nome_fantasia"].ToString();
                objClientes.razao_social = formData["razao_social"].ToString();
                objClientes.telefone = formData["telefone"].ToString();
                objClientes.celular = formData["celular"].ToString();
                objClientes.rua = formData["rua"].ToString();
                objClientes.numero = Convert.ToInt32(formData["numero"].ToString());
                objClientes.complemente = formData["complemente"].ToString();
                objClientes.bairro = formData["bairro"].ToString();
                objClientes.cep = formData["cep"].ToString();
                objClientes.cidade_id = Convert.ToInt32(formData["cidade"].ToString());



                //Verifica se é uma inserção
                if (objClientes.id == 0)
                {
                    objDAO.InserirClientes(objClientes);
                }
                else
                {
                    //objClientes.cpf_cnpj = cpf_cnpj.Trim();
                    objDAO.AlterarClientes(objClientes);
                }              

            }
            catch (Exception ex)
            {
                return Content("{\"success\":false}", "application/json");
            }
            return Content("{\"success\":true}", "application/json");
        }
        
        [HttpPost]
        public ContentResult ExcluirCliente(int id)
        {
            try
            {       
                if (id == 0)
                    return Content("{\"success\":false}", "application/json");

                ClientesDAO objDAO = new ClientesDAO();

                objDAO.ExcluirCliente(id);
            }
            catch (Exception ex)
            {
                return Content("{\"success\":false}", "application/json");
            }
            return Content("{\"success\":true}", "application/json");
        }

        public ActionResult AlterarClientes(int? id, string cpf_cnpj, string rg, string nome, string nome_fantasia, string razao_social, string telefone, string celular,
                                            string rua, string numero, string complemente, string bairro, string cep, int estado, int cidade)
        {
            ViewBag.CodigoCliente = id;
            ViewBag.CPF_CNPJ = cpf_cnpj;
            ViewBag.RG = rg;
            ViewBag.RazaoSocial = nome;
            ViewBag.NomeFantasia = nome_fantasia;
            ViewBag.NumeroTelefone = telefone;
            ViewBag.NumeroCelular = celular;
            ViewBag.NomeRua = rua;
            ViewBag.NumeroCasa = numero;
            ViewBag.ComplementoCasa = complemente;
            ViewBag.NomeBairro = bairro;
            ViewBag.NumeroCep = cep;
            ViewBag.NomeCidade = cidade;

            ConsultarClientes();
            return View("Clientes");
        }

        public void CarregarCidades(int id = 0)
        {

            ClientesDAO ObjCidades = new ClientesDAO();                      
            
            ViewBag.ListaCidades = ObjCidades.ConsultarCidades().ToList();


        }

        public void CarregarEstados()
        {
            ClientesDAO ObjEstados = new ClientesDAO();

            ViewBag.ListaEstados = ObjEstados.ConsultarEstados().ToList();
        }

        private void ConsultarClientes()
        {
            ClientesDAO objDAO = new ClientesDAO();
            List<ClientesVO> lstCat = objDAO.ConsultarClientes();

            ViewBag.ListaEmpresa = lstCat;
        }

    }
}
