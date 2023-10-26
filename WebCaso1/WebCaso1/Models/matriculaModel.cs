using System.Net.Http.Json;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebCaso1.Entity;

namespace WebCaso1.Models
{
    public class matriculaModel
    {
        private string urlApi = ConfigurationManager.AppSettings["urlApi"];
        public string RegistrarMatricula(matriculaEntity matricula)
        {
            using (var client = new HttpClient())
            {
                string url = urlApi + "RegistrarMatricula";
                JsonContent contenido = JsonContent.Create(matricula);
                using (HttpResponseMessage resp = client.PostAsync(url, contenido).Result)
                {
                    return resp.Content.ReadFromJsonAsync<string>().Result;
                }
            }
        }
    }
}