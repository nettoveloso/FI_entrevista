using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario do cliente
        /// </summary>
        /// <param name="beneficiario">Objeto do beneficiario</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            this.existeCPF(beneficiario, beneficiario.IdCliente);
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiario do cliente
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            benef.Alterar(beneficiario);
        }


        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            benef.Excluir(id);
        }

        /// <summary>
        /// Lista os benefiarios do cliente
        /// </summary>
        public List<DML.Beneficiario> Listar(long idcliente)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Listar(idcliente);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long idcliente)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.VerificarExistencia(CPF, idcliente);
        }

        /// <summary>
        /// Trata o retorno do CPF
        /// </summary>
        /// <param name="CPF"></param>
        private void existeCPF(DML.Beneficiario beneficiario, long idcliente)
        {
            if (this.VerificarExistencia(beneficiario.CPF, idcliente))
            {
                throw new Exception($"CPF {beneficiario.CPF} já existe cadastrado na base de dados ao cliente");
            }
        }
    }
}
