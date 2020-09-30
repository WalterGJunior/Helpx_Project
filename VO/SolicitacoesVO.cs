using DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SolicitacoesVO
    {
        public int CodSolicitacao { get; set; }
        public string NomeSolicitante { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public string NumeroTelefone { get; set; }
        public string EnderecoEmail { get; set; }
        public string Descricao { get; set; }
        public string Detalhamento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataFinal { get; set; }
        public int TipoId { get; set; }
        public string TipoChamado { get; set; }
        public string TipoStatus { get; set; }         
        public int StatusChamadoId { get; set; }
        public int NomeAtendenteId { get; set; }
        public int AnalistaLogadoId { get; set; }
        public int AnalistaCadastroId { get; set; }
        

    }
}
