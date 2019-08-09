using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                try
                {
                    model.Id = bo.Incluir(new Beneficiario()
                    {
                        Nome        = model.Nome,
                        CPF         = model.CPF,
                        IdCliente   = model.Idcliente
                    });

                    return Json("Cadastro do beneficiario efetuado com sucesso");
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }

            }
        }

        [HttpPost]
        public JsonResult BeneficiarioList(long idcliente)
        {
            try
            {
                List<Beneficiario> beneficiarios = new BoBeneficiario().Listar(idcliente);
                //Return result to jTable
                return Json(new { Result = "OK", Records = beneficiarios});
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Excluir (long id)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();

                bo.Excluir(id);
                return Json("Eeneficiario excluido com sucesso");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}