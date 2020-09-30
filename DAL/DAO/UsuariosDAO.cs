using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class UsuariosDAO
    {
        public tb_usuarios ValidarLogin(string email, string senha)
        {
            DBHelpX objBanco = new DBHelpX();

            tb_usuarios objUser = objBanco.tb_usuarios.FirstOrDefault(p => p.email == email && p.senha == senha);
            return objUser;
        }

        //Método para Inserir  Usuários
        public void InserirUsuarios(tb_usuarios objEntrada)
        {
            //Cria o Banco
            DBHelpX objBanco = new DBHelpX();

            //Adciona o objeto para gravar
            objBanco.AddTotb_usuarios(objEntrada);

            //salva a operacao
            objBanco.SaveChanges();
        }

        //Método para Alterar  Usuários
        public void AlterarUsuarios(tb_usuarios objEntrada)
        {
            DBHelpX objBanco = new DBHelpX();

            tb_usuarios objUsuario = objBanco.tb_usuarios.Where(u => u.id == objEntrada.id).ToList().FirstOrDefault();

            objUsuario.nome = objEntrada.nome;
            objUsuario.senha = objEntrada.senha;
            objUsuario.email = objEntrada.email;

            objBanco.SaveChanges();

        }

        //Método para Excluir  Usuários
        public void ExcluirUsuarios(int id)
        {
            DBHelpX objBanco = new DBHelpX();

            tb_usuarios objUsuario = objBanco.tb_usuarios.Where(u => u.id == id).ToList().FirstOrDefault();

            objUsuario.status = true;

            objBanco.SaveChanges();

        }

        //Método para Consultar  Usuários
        public List<UsuariosVO> ConsultarUsuarios(int codUsuarios = 0)
        {
            List<tb_usuarios> lstRetorno = new List<tb_usuarios>();

            DBHelpX objbanco = new DBHelpX();
            if (codUsuarios == 0)
            {
                lstRetorno = objbanco.tb_usuarios.Where(c => !c.status).ToList();
            }
            else
            {               
                lstRetorno = objbanco.tb_usuarios.Where(c => c.id == codUsuarios).ToList();
            }



            List<UsuariosVO> lstTratada = new List<UsuariosVO>();

            for (int i = 0; i < lstRetorno.Count; i++)
            {
                UsuariosVO objUsuariosVO = new UsuariosVO();

                objUsuariosVO.CodigoUsuario = lstRetorno[i].id;
                objUsuariosVO.NomeUsuario = lstRetorno[i].nome;
                objUsuariosVO.SenhaUsuario = lstRetorno[i].senha;
                objUsuariosVO.EmailUsuario = lstRetorno[i].email;


                lstTratada.Add(objUsuariosVO);
            }

            return lstTratada;
        }    
      
    }
}
