using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCaso1.Entity;
using WebCaso1.Models;

namespace WebCaso1.Controllers
{
    public class RegistrarController : Controller
    {
        [HttpGet]
        public ActionResult RegistrarMatricula()
        {

            return View();
        }


        [HttpPost]
        public ActionResult RegistrarMatricula(matriculaEntity matricula)
        {
            matriculaModel model = new matriculaModel();

            if (ModelState.IsValid) 
            {
                model.RegistrarMatricula(matricula);
                ViewBag.ConfirmationMessage = "Matricula registrada con éxito";
            }
            else
            {
                ViewBag.ErrorMessage = "Se encontraron errores en el formulario. Corríjalos antes de continuar.";
            }

            return View();
        }
    }


}
