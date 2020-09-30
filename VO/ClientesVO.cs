using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClientesVO
    {
        // Todos os campos que estão na tabela cliente
        public int ClienteId { get; set; }
        public int CodigoCliente { get; set; }
        public string CPF_CNPJ { get; set; }
        public string RG { get; set; }
        public string NomeCliente { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NumeroTelefone { get; set; }
        public string NumeroCelular { get; set; }
        public string NomeRua { get; set; }
        public int NumeroCasa { get; set; }
        public string ComplementoCasa { get; set; }
        public string NomeBairro { get; set; }
        public string NumeroCep { get; set; }
        public int EstadoId { get; set; }
        public int CidadeId { get; set; }      
    }
}
