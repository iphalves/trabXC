using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetShop.Controllers
{   
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CadLogin()
        {
            return Redirect("Funcionario/Cadastro");
        }
        public IActionResult TelaLogar()
        {
           return Redirect("/Funcionario/Index");
        }
    }
}