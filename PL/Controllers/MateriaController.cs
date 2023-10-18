using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        string urlAPI = System.Configuration.ConfigurationManager.AppSettings["API_URI"];
        string palabrasNoPermitidas = System.Configuration.ConfigurationManager.AppSettings["ValoresNoPermitidos"];
        char caracterInvalido;

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            materia.Materias = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("materia");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Materia materiaItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        materia.Materias.Add(materiaItem);
                    }
                }
            }
            return View(materia);
        }

        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            if (IdMateria == null)
            {
                return View(materia);
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.GetAsync("materia/" + IdMateria);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Materia resultItem = new ML.Materia();
                        resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());

                        materia = resultItem;
                    }
                    return View(materia);
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if (ModelState.IsValid)
            {
                bool resultNombre = ValidarNombre(materia.Nombre);
                if (resultNombre)
                {
                    if (materia.IdMateria == 0)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(urlAPI);

                            var postTask = client.PostAsJsonAsync<ML.Materia>("materia", materia);
                            var result = postTask.Result;

                            if (result.IsSuccessStatusCode)
                            {
                                return RedirectToAction("GetAll");
                            }
                            else
                            {
                                ViewBag.Message = "Ocurrio un problema al insertar la Materia";
                            }
                        }
                        return View("GetAll");
                    }
                    else
                    {
                        using (var client = new HttpClient())
                        {
                            int id = materia.IdMateria;
                            client.BaseAddress = new Uri(urlAPI);

                            var postTasks = client.PutAsJsonAsync<ML.Materia>("materia/" + id, materia);
                            postTasks.Wait();

                            var result = postTasks.Result;

                            if (result.IsSuccessStatusCode)
                            {
                                ViewBag.Message = "Se ha actualizado correctamente la Materia";
                            }
                            else
                            {
                                ViewBag.Message = "Ocurrio un problema al actualizar la Materia";
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "No se pudo insertar la materia debido a que el caracter '" + caracterInvalido + "' no es un valor permitido, los valores no permitidos son: " + palabrasNoPermitidas;
                }
                return PartialView("Modal");
            }
            return View(materia);
        }

        [HttpGet]
        public ActionResult Delete(int IdMateria)
        {
            ML.Result resultListMateria = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var postTasks = client.DeleteAsync("materia/" + IdMateria);
                postTasks.Wait();

                var result = postTasks.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha eliminado la Materia correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un problema al eliminar la Materia.";
                }
                return PartialView("Modal");
            }
        }

        public bool ValidarNombre(string Nombre)
        {
            ML.Result result = new ML.Result();

            foreach (var name in Nombre)
            {
                foreach (var palabrasItem in palabrasNoPermitidas)
                {

                    if (name != palabrasItem)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        caracterInvalido = name;
                        return result.Correct;
                    }

                }
            }
            return result.Correct;
        }
    }
}