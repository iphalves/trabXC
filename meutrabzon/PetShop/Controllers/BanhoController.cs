using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{
    public class BanhoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public JsonResult Cadastrar([FromBody]Dictionary<string,string>dados)
        //{
        //    string msg=""; bool operacao = false;
        //    Models.Banho b = new Models.Banho();
        //    b.NomeCliente = dados["nomeCliente"]; b.NomePet = dados["nomePet"];b.TamanhoPet = dados["tamanhoPet"];
        //    b.TipoServico = dados["tipoServico"];b.HorarioData = dados["horarioData"]; b.HorarioHora = dados["horarioHora"];

        //    operacao = b.Gravar(out msg);
        //    var retorno = new
        //    {
        //        msg = msg,
        //        operacao = operacao
        //    };
        //    return Json(retorno);
        //}
        public IActionResult TelaLogar()
        {
            return Redirect("/Funcionario/Index");
        }
    }
}