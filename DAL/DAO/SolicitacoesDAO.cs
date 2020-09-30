using DAL;
using DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SolicitacoesDAO
    {
        //Método para inserir Solicitações
        public void InserirSolicitacoes(tb_solicitacoes objEntrada)
        {
            //Cria o Banco
            BaseDados objBanco = new BaseDados();

            //Adciona o Objeto para gravar
            objBanco.AddTotb_solicitacoes(objEntrada);

            //Salvar Operação
            objBanco.SaveChanges();
        }
              
        //Método para Alterar  Solicitações
        public void AlterarSolicitacoes(tb_solicitacoes objAlterar)
        {
            BaseDados objBanco = new BaseDados();

            tb_solicitacoes objSolicitacoes = objBanco.tb_solicitacoes.Where(s => s.id == objAlterar.id).ToList().FirstOrDefault();

            objSolicitacoes.codigo = objAlterar.codigo;
            //objSolicitacoes.clientes_id = objAlterar.clientes_id;
            objSolicitacoes.status_id = objAlterar.status_id;
            //objSolicitacoes.solicitante = objAlterar.solicitante;
            //objSolicitacoes.telefone = objAlterar.telefone;
            //objSolicitacoes.email = objAlterar.email;
            //objSolicitacoes.descricao = objAlterar.descricao;
            objSolicitacoes.detalhamento = objAlterar.detalhamento;
            //objSolicitacoes.data_cadastro = objAlterar.data_cadastro;
            objSolicitacoes.data_final = objAlterar.data_final;
            //objSolicitacoes.tipo_id = objEntrada.tipo_id;
            objSolicitacoes.usuario_cadastro_id = objAlterar.usuario_cadastro_id;
            //objSolicitacoes.descricao = objAlterar.tb_LogSolicitacoes


            //tb_LogSolicitacoes objLog = new tb_LogSolicitacoes();

            //objLog.solicitacoes_id = objAlterar.id;
            //objLog.detalhamento = objAlterar.detalhamento;
            //objLog.data_registro = objAlterar.data_final;
            //objLog.status_id = objAlterar.status_id;
            //objLog.usuario_cadastro_id = objAlterar.usuario_cadastro_id;
            
            //LogDAO logDAO = new LogDAO();
            //logDAO.InserirLog(objLog);

     

            objBanco.SaveChanges();

        }

        ////Método para consultar  Solicitações
        //public List<SolicitacoesVO> ConsultarSolicitacao(int codsolicitacao = 0)
        //{
        //    List<tb_solicitacoes> lstRetorno = new List<tb_solicitacoes>();

        //    BaseDados objbanco = new BaseDados();
        //    if (codsolicitacao == 0)
        //    {
        //        lstRetorno = objbanco.tb_solicitacoes.Include("tb_cidades").Include("tb_tipos").Include("tb_clientes").Include("tb_usuarios")
        //                     .Where(c => c.id == codsolicitacao).ToList();
        //    }
        //    else
        //    {
        //        lstRetorno = objbanco.tb_clientes.Include("tb_cidades").Include("tb_tipos").Include("tb_clientes").Include("tb_usuarios")
        //                      .Where(c => c.id == codsolicitacao).ToList();
        //    }



        //    List<ClientesVO> lstTratada = new List<ClientesVO>();

        //    for (int i = 0; i < lstRetorno.Count; i++)
        //    {
        //        ClientesVO objClientesVO = new ClientesVO();

        //        objClientesVO.CodigoCliente = lstRetorno[i].id;
        //        objClientesVO.CPF_CNPJ = lstRetorno[i].cpf_cnpj;
        //        objClientesVO.RG = lstRetorno[i].rg;
        //        objClientesVO.NomeCliente = lstRetorno[i].nome;
        //        objClientesVO.RazaoSocial = lstRetorno[i].razao_social;
        //        objClientesVO.NomeFantasia = lstRetorno[i].nome_fantasia;
        //        objClientesVO.NumeroTelefone = lstRetorno[i].telefone;
        //        objClientesVO.NumeroCelular = lstRetorno[i].celular;
        //        objClientesVO.NomeRua = lstRetorno[i].rua;
        //        objClientesVO.NumeroCasa = (int)lstRetorno[i].numero;
        //        objClientesVO.ComplementoCasa = lstRetorno[i].complemente;
        //        objClientesVO.NomeBairro = lstRetorno[i].bairro;
        //        objClientesVO.NumeroCep = lstRetorno[i].cep;
        //        objClientesVO.CidadeId = lstRetorno[i].tb_cidades.id;
        //        objClientesVO.EstadoId = (int)lstRetorno[i].tb_cidades.estado_id;



        //        lstTratada.Add(objClientesVO);
        //    }

        //    return lstTratada;
        //}


        //Consultar Tipos de Chamados
        public List<tb_tipos> ConsultarTipos()
        {
            //Cria o Banco
            BaseDados objbanco = new BaseDados();

            //Será criada uma lista com todos os tipos de chamados e armazenada no objeto lstRetorno
            List<tb_tipos> lstRetorno = objbanco.tb_tipos.ToList();

            return lstRetorno;
        }

        public List<tb_status> ConsultarStatus()
        {
            //Cria o Banco
            BaseDados objbanco = new BaseDados();

            //Será criada uma lista com todos os status de chamados e armazenada no objeto lstRetorno
            List<tb_status> lstRetorno = objbanco.tb_status.ToList();

            return lstRetorno;

        }

        public List<SolicitacoesVO> ConsultarChamados(int CodSolicitacao = 0)
        {
            List<tb_solicitacoes> lstRetorno = new List<tb_solicitacoes>();

            BaseDados objbanco = new BaseDados();
            if (CodSolicitacao == 0)
            {
                lstRetorno = objbanco.tb_solicitacoes.Include("tb_status").Include("tb_tipos").Include("tb_clientes").ToList();
            }
            else
            {
                lstRetorno = objbanco.tb_solicitacoes.Include("tb_status").Include("tb_tipos").Include("tb_clientes")
                              .Where(c => c.id == CodSolicitacao).ToList();
            }



            List<SolicitacoesVO> lstTratada = new List<SolicitacoesVO>();

            for (int i = 0; i < lstRetorno.Count; i++)
            {
                SolicitacoesVO objChamados = new SolicitacoesVO();

                objChamados.CodSolicitacao = lstRetorno[i].id;
                objChamados.NomeSolicitante = lstRetorno[i].solicitante;
                objChamados.ClienteId = lstRetorno[i].clientes_id;
                objChamados.NomeCliente = lstRetorno[i].tb_clientes.nome;
                objChamados.NumeroTelefone = lstRetorno[i].telefone;
                objChamados.EnderecoEmail = lstRetorno[i].email;
                objChamados.Descricao = lstRetorno[i].descricao;
                objChamados.Detalhamento = lstRetorno[i].detalhamento;
                objChamados.DataCadastro = Convert.ToDateTime(lstRetorno[i].data_cadastro);
                objChamados.DataFinal = Convert.ToDateTime(lstRetorno[i].data_final);
                objChamados.TipoId = lstRetorno[i].tipo_id;
                objChamados.TipoChamado = lstRetorno[i].tb_tipos.nome;
                objChamados.StatusChamadoId = (int)lstRetorno[i].status_id;
                objChamados.TipoStatus = lstRetorno[i].tb_status.descricao;
                objChamados.NomeAtendenteId = lstRetorno[i].usuario_cadastro_id;



                lstTratada.Add(objChamados);
            }

            return lstTratada;
        }

        public List<LogSolicitacoesVO> ConsultarLog(int CodSolicitacao = 0)
        {
            List<tb_LogSolicitacoes> lstRetorno = new List<tb_LogSolicitacoes>();

            BaseDados objbanco = new BaseDados();
            if (CodSolicitacao == 0)
            {
                lstRetorno = objbanco.tb_LogSolicitacoes.Include("tb_status").Include("tb_solicitacoes").Include("tb_usuarios").ToList();
            }
            else
            {
                lstRetorno = objbanco.tb_LogSolicitacoes.Include("tb_status").Include("tb_solicitacoes").Include("tb_usuarios")
                              .Where(c => c.solicitacoes_id == CodSolicitacao).ToList();
            }

            List<LogSolicitacoesVO> lstTratada = new List<LogSolicitacoesVO>();

            for (int i = 0; i < lstRetorno.Count; i++)
            {
                LogSolicitacoesVO objLog = new LogSolicitacoesVO();

                objLog.CodLog = lstRetorno[i].id;
                objLog.DataRegistro = Convert.ToDateTime(lstRetorno[i].tb_solicitacoes.data_final);
                objLog.CodSolicitacao = lstRetorno[i].tb_solicitacoes.id;
                objLog.Detalhamento = lstRetorno[i].tb_solicitacoes.detalhamento;
                objLog.NomeAtendenteId = lstRetorno[i].tb_usuarios.id;
                objLog.StatusChamadoId = lstRetorno[i].tb_status.id;


                lstTratada.Add(objLog);
            }

            return lstTratada;
        }

        public string GerarCodigo(int tipo)
        {
            BaseDados banco = new BaseDados();           

            string codigo = "S";

            if (tipo == (int)ETipo.Incidente)
                codigo = "I";

            int count = 0;
            count = banco.tb_solicitacoes.Where(a => a.data_cadastro.Value.Year == DateTime.Now.Year).ToList().Count();

            DateTime teste = DateTime.Now;           

            for (int i = 0; i < (4 - count.ToString().Length); i++)
                codigo += "0";

            codigo += (count + 1);

            return codigo;
        }

        //public void ExcluirChamados(int id)
        //{
        //    BaseDados objBanco = new BaseDados();

        //    tb_solicitacoes objCliente = objBanco.tb_solicitacoes.Where(a => a.id == id).ToList().FirstOrDefault();

        //    objCliente.codigo = true;

        //    objBanco.SaveChanges();

        //}


    }
}
