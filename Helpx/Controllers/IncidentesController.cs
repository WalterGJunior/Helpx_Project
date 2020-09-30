using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DAO;
using DAL;

namespace Helpx.Controllers
{
    public class IncidentesController : PageBase
    {
        //
        // GET: /Incidentes/

        public ActionResult Index()
        {
            if (Session["codUser"] == null || (int)Session["codUser"] == 0)
                return Redirect("/Login/Login");

            ViewBag.ListaChamados = new SolicitacoesDAO().ConsultarChamados();            
            return View();
        }

        public ActionResult Incidentes(int id = 0)
        {
            var obj = new SolicitacoesVO();
            if (id > 0)
                obj = new SolicitacoesDAO().ConsultarChamados(id)[0];

            CarregarClientes();
            CarregarTipos();
            CarregarStatus();
            return View(obj);
        }

        public ActionResult Details(int id = 0)
        {
            ViewBag.ListaLog = new SolicitacoesDAO().ConsultarLog(id);
            return View();
        }
        [HttpPost]
        public ContentResult GravarSolicitacoes(FormCollection formData)
        {
            try
            {
                //Cria o Objeto DAO
                SolicitacoesDAO objDAO = new SolicitacoesDAO();

                //Cria o objeto para gravar na tabela solicitações
                tb_solicitacoes objSolicitacoes = new tb_solicitacoes();

                objSolicitacoes.id = Convert.ToInt32(formData["id"].ToString());
                objSolicitacoes.status_id = Convert.ToInt32(formData["StatusChamadoId"].ToString());
                objSolicitacoes.data_final = Convert.ToDateTime(formData["DataFinal"].ToString());
                objSolicitacoes.detalhamento = formData["Detalhamento"].ToString();
                objSolicitacoes.usuario_cadastro_id = (int)Session["codUser"];

                //Verifica se é uma inserção
                if (objSolicitacoes.id == 0)
                {
                    objSolicitacoes.codigo = objDAO.GerarCodigo(objSolicitacoes.tipo_id);
                    objSolicitacoes.tipo_id = Convert.ToInt32(formData["TipoId"].ToString());
                    objSolicitacoes.data_cadastro = Convert.ToDateTime(formData["DataCadastro"].ToString());
                    objSolicitacoes.clientes_id = Convert.ToInt32(formData["ClienteId"].ToString());
                    objSolicitacoes.solicitante = formData["NomeSolicitante"].ToString();
                    objSolicitacoes.telefone = formData["NumeroTelefone"].ToString();
                    objSolicitacoes.email = formData["EnderecoEmail"].ToString();
                    objSolicitacoes.descricao = formData["Descricao"].ToString();

                    //Insere as informações na tabela de solicitação
                    objDAO.InserirSolicitacoes(objSolicitacoes);


                    //Cria objeto para gravar as informações na tabela de Log
                    //tb_LogSolicitacoes objLog = new tb_LogSolicitacoes();

                    //objLog.solicitacoes_id = objSolicitacoes.id;                 
                    //objLog.detalhamento = formData["Detalhamento"].ToString();
                    //objLog.data_registro = DateTime.Now;
                    //objLog.status_id = Convert.ToInt32(formData["StatusChamadoId"].ToString());  
                    //objLog.usuario_cadastro_id = (int)Session["codUser"];

                    //LogDAO logDAO = new LogDAO();

                    //logDAO.InserirLog(objLog);


                }
                else
                {
                    objDAO.AlterarSolicitacoes(objSolicitacoes);
                }

                GravarLog(objSolicitacoes);
            }
            catch (Exception ex)
            {
                return Content("{\"success\":false}", "application/json");
            }
            return Content("{\"success\":true}", "application/json");
        }

        public ActionResult AlterarChamados(int? id, string solicitante, string telefone, string email, string descricao, string detalhamento, DateTime data_cadastro,
                                           DateTime data_final, int tipo_id, int status_id, int usuario_cadastro_id, int clientes_id)
        {
            ViewBag.CodSolicitacao = id;
            ViewBag.TipoId = solicitante;
            ViewBag.StatusChamadoId = status_id;
            ViewBag.DataCadastro = data_cadastro;
            ViewBag.DataFinal = data_final;
            ViewBag.ClienteId = clientes_id;
            ViewBag.NomeSolicitante = solicitante;
            ViewBag.NumeroTelefone = telefone;
            ViewBag.EnderecoEmail = email;
            ViewBag.Descricao = descricao;
            ViewBag.Detalhamento = detalhamento;

            ConsultarChamados();
            return View("Incidentes");
        }

        public void CarregarTipos(int id = 0)
        {
            SolicitacoesDAO ObjTipos = new SolicitacoesDAO();

            ViewBag.ListaTipos = ObjTipos.ConsultarTipos().ToList();

        }

        public void CarregarStatus(int id = 0)
        {
            SolicitacoesDAO ObjStatus = new SolicitacoesDAO();

            ViewBag.ListaStatus = ObjStatus.ConsultarStatus().ToList();

        }

        public void CarregarClientes(int id = 0)
        {
            ClientesDAO objClientes = new ClientesDAO();

            ViewBag.ListaClientes = objClientes.ConsultarClientes().ToList();

        }

        private void ConsultarChamados()
        {
            SolicitacoesDAO objDAO = new SolicitacoesDAO();
            List<SolicitacoesVO> lstCat = objDAO.ConsultarChamados();

            ViewBag.ListaEmpresa = lstCat;
        }

        private void GravarLog(tb_solicitacoes objSolicitacoes)
        {
            tb_LogSolicitacoes objLog = new tb_LogSolicitacoes();

            objLog.solicitacoes_id = objSolicitacoes.id;
            objLog.detalhamento = objSolicitacoes.detalhamento;
            objLog.data_registro = DateTime.Now;
            objLog.status_id = Convert.ToInt32(objSolicitacoes.status_id);
            objLog.usuario_cadastro_id = (int)Session["codUser"];

            LogDAO logDAO = new LogDAO();

            logDAO.InserirLog(objLog);
        }

        //[HttpPost]
        //public ContentResult ExcluirChamados(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //            return Content("{\"success\":false}", "application/json");

        //        ClientesDAO objDAO = new ClientesDAO();

        //        objDAO.ExcluirChamados(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("{\"success\":false}", "application/json");
        //    }
        //    return Content("{\"success\":true}", "application/json");
        //}

    }

}
