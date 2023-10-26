using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_Caso1.Controllers
{
    public class RegistrarController : ApiController
    {
        [HttpPost]
        [Route("RegistrarMatricula")]
        public string RegistroCarro(matriculaEntity matricula)
        {
            try
            {
                using (var context = new CasoEstudioMNEntities())
                {
                    context.RegistrarMatriculaV2(matricula.Nombre, matricula.Monto, matricula.TipoCurso);
                    return "Registro exitoso";
                }
            }
            catch (Exception ex)
            {
                return "Error en el proceso de registro: " + ex.Message;
            }
        }


        [HttpGet]
        [Route("RealizarConsulta")]
        public List<Consultas> ConsultaUsuarios()
        {
            try
            {
                using (var context = new CasoEstudioMNEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var datos = (from x in context.Consultas
                                 select x).ToList();

                    return datos;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
