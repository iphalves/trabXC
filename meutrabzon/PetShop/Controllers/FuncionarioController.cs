using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{
    public class FuncionarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Logar([FromBody]Dictionary<string, string> dados)
        {

            string usuario = dados["usuario"];
            string senha = dados["senha"];

            Models.Funcionario f = new Models.Funcionario();
            bool logado = f.Autenticar(usuario, senha);
            string url = "";
            if (logado)
                url = "/Funcionario/Cadastro";

            var retorno = new
            {
                logado = logado,
                url = url
            };

            return Json(retorno);
        }

        public IActionResult Cadastro()
        {
            return View();
        }
        public JsonResult Cadastrar([FromBody] Dictionary<string,string> dados)
        {
            string msg = "";
            bool operacao = false;
            Models.Funcionario f = new Models.Funcionario();

            if (dados["id"] != "0")
                f.SetId(Convert.ToInt32(dados["id"]));

            f.SetNomeCompleto(dados["nome"]);
            f.SetRg(dados["rg"]);
            f.SetSexo(dados["sexo"]);
            f.SetCpf(dados["cpf"]);

            int auxSenha;

            if (f.GetId() == 0)
            {
                if (int.TryParse(dados["senha"], out auxSenha))
                {
                    f.SetSenha(auxSenha);
                }
                else
                {

                    msg = "Senha inválida.";
                }
            }

            if (msg == "")
            {
                //está tudo certo para gravar
                operacao = f.Gravar(out msg);
            }


            var retorno = new
            {
                operacao = operacao,
                msg = msg
            };


            return Json(retorno);
        }
        public JsonResult Deletar([FromBody]Dictionary<string,string>dados)
        {
            string msg =""; bool operacao = false;
            Models.Funcionario f = new Models.Funcionario();
            int id = Convert.ToInt32(dados["id"]);
            operacao = f.Deletar(id,out msg);
            var retorno = new
            {
                operacao = operacao,
                msg = msg
            };
            return Json(retorno);

        }
        public JsonResult ObterPorId([FromBody] Dictionary<string, string> dados)
        {
            Models.Funcionario f = new Models.Funcionario();

            int id = Convert.ToInt32((dados["id"]));
            f.ObterPorId(id);

            var retorno = new
            {
                id = (f.GetId()),
                nomeCompleto = f.GetNomeCompleto(),
                cpf = f.GetCpf()
            };

            return Json(retorno);
        }
        public IActionResult TelaLogar()
        {
            return View("Index");
        }
        public IActionResult Pesquisar()
        {
            return View();
        }
         public JsonResult EnvPesquisar([FromBody] Dictionary<string, string> dados)
        {
            Models.Funcionario f = new Models.Funcionario();

            string nome = dados["nome"];
            List<object> retorno = new List<object>();

            if (nome.Trim().Length > 2)
            {
                List<Models.Funcionario> fs = f.Pesquisar(nome);

                foreach (Models.Funcionario fItem in fs)
                {
                    var obj = new
                    {
                        //id = fItem.GetId,
                        //nome = fItem.GetNomeCompleto
                    };

                    retorno.Add(obj);
                }

            }
            //return Json(fs);

            return Json(retorno);
        }

    }
}