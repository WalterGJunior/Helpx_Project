using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL;

namespace DAL.DAO
{
    public class ClientesDAO
    {
        //Método para Inserir  Categoria
        public void InserirClientes(tb_clientes objEntrada)
        {
            //Cria o Banco
            BaseDados objBanco = new BaseDados();

            //Adciona o objeto para gravar
            objBanco.AddTotb_clientes(objEntrada);

            //salva a operacao
            objBanco.SaveChanges();

        }

        //Método que será utilizado para criar uma lista com todos as cidades cadastradas 
        public List<tb_cidades> ConsultarCidades()
        {
            //Cria o Banco
            BaseDados objbanco = new BaseDados();

            //Será criada uma lista com nome de todas as cidades e armazenada no objeto lstRetorno
            List<tb_cidades> lstRetorno = objbanco.tb_cidades.ToList();

            return lstRetorno;
        }

        public List<tb_estados> ConsultarEstados()
        {
            //Cria o Banco
            BaseDados objbanco = new BaseDados();

            //Será criada uma lista com nome de todas as cidades e armazenada no objeto lstRetorno
            List<tb_estados> lstRetorno = objbanco.tb_estados.ToList();

            return lstRetorno;
        }

        public List<ClientesVO> ConsultarClientes(int codcliente = 0)
        {
            List<tb_clientes> lstRetorno = new List<tb_clientes>();

            BaseDados objbanco = new BaseDados();
            if (codcliente == 0)
            {
                lstRetorno = objbanco.tb_clientes.Include("tb_cidades").Where(c => !c.status).ToList();
            }
            else
            {
                lstRetorno = objbanco.tb_clientes.Include("tb_cidades")
                              .Where(c => c.id == codcliente).ToList();
            }



            List<ClientesVO> lstTratada = new List<ClientesVO>();

            for (int i = 0; i < lstRetorno.Count; i++)
            {
                ClientesVO objClientesVO = new ClientesVO();

                objClientesVO.ClienteId = lstRetorno[i].id;
                objClientesVO.CPF_CNPJ = lstRetorno[i].cpf_cnpj;
                objClientesVO.RG = lstRetorno[i].rg;
                objClientesVO.NomeCliente = lstRetorno[i].nome;
                objClientesVO.RazaoSocial = lstRetorno[i].razao_social;
                objClientesVO.NomeFantasia = lstRetorno[i].nome_fantasia;
                objClientesVO.NumeroTelefone = lstRetorno[i].telefone;
                objClientesVO.NumeroCelular = lstRetorno[i].celular;
                objClientesVO.NomeRua = lstRetorno[i].rua;
                objClientesVO.NumeroCasa = (int)lstRetorno[i].numero;
                objClientesVO.ComplementoCasa = lstRetorno[i].complemente;
                objClientesVO.NomeBairro = lstRetorno[i].bairro;
                objClientesVO.NumeroCep = lstRetorno[i].cep;
                objClientesVO.CidadeId = lstRetorno[i].tb_cidades.id;
                objClientesVO.EstadoId = (int)lstRetorno[i].tb_cidades.estado_id;



                lstTratada.Add(objClientesVO);
            }

            return lstTratada;
        }

        public void AlterarClientes(tb_clientes objEntrada)
        {
            BaseDados objBanco = new BaseDados();

            tb_clientes objClientes = objBanco.tb_clientes.Where(a => a.id == objEntrada.id).ToList().FirstOrDefault();

            objClientes.cpf_cnpj = objEntrada.cpf_cnpj;
            objClientes.rg = objEntrada.rg;
            objClientes.nome = objEntrada.nome;
            objClientes.razao_social = objEntrada.razao_social;
            objClientes.telefone = objEntrada.telefone;
            objClientes.celular = objEntrada.celular;
            objClientes.rua = objEntrada.rua;
            objClientes.numero = objEntrada.numero;
            objClientes.complemente = objEntrada.complemente;
            objClientes.bairro = objEntrada.bairro;
            objClientes.cidade_id = objEntrada.cidade_id;

            objBanco.SaveChanges();

        }

        public void ExcluirCliente(int id)
        {
            BaseDados objBanco = new BaseDados();

            tb_clientes objCliente = objBanco.tb_clientes.Where(a => a.id == id).ToList().FirstOrDefault();

            objCliente.status = true;

            objBanco.SaveChanges();

        }

    }
}
