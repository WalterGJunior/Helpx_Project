using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LogSolicitacoesVO
    {
        public int CodLog { get; set; }
        public DateTime DataRegistro { get; set; } 
        public int CodSolicitacao { get; set; }
        public string Detalhamento { get; set; }
        public int NomeAtendenteId { get; set; }
        public int StatusChamadoId { get; set; }
    }
}
