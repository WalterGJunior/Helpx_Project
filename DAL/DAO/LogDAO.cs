using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class LogDAO
    {
        //Método para inserir Solicitações
        public void InserirLog(tb_LogSolicitacoes objEntrada)
        {
            //Cria o Banco
            BaseDados objBanco = new BaseDados();

            //Adciona o Objeto para gravar
            objBanco.AddTotb_LogSolicitacoes(objEntrada);

            //Salvar Operação
            objBanco.SaveChanges();
        }
    }
}
