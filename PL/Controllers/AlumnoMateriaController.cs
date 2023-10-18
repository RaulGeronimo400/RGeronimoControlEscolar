using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        string urlAPI = System.Configuration.ConfigurationManager.AppSettings["API_URI"];

        [HttpGet]
        public ActionResult AlumnoGetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = new List<object>();

            ServiceAlumno.AlumnoClient service = new ServiceAlumno.AlumnoClient();
            var result = service.GetAll();

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects.ToList();
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
            }
            return View(alumno);
        }

        //[HttpGet]
        //public ActionResult GetMateriasAsignadasByIdAlumno(int? IdAlumno)
        //{
        //    ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
        //    alumnoMateria.materias = new List<object>();
        //    alumnoMateria.alumno = new ML.Alumno();

        //    ML.Result resultMaterias = BL.AlumnoMateria.GetMateriasAsignadas(IdAlumno.Value);
        //    if (resultMaterias.Correct)
        //    {
        //        alumnoMateria.materias = resultMaterias.Objects.ToList();

        //        ML.Result result = BL.Alumno.GetById(IdAlumno.Value);
        //        if (result.Correct)
        //        {
        //            alumnoMateria.alumno.IdAlumno = ((ML.Alumno)result.Object).IdAlumno;
        //            alumnoMateria.alumno.Nombre = ((ML.Alumno)result.Object).Nombre;
        //            Session["IdAlumno"] = alumnoMateria.alumno.IdAlumno;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = resultMaterias.ErrorMessage;
        //    }
        //    return View(alumnoMateria);
        //}
        [HttpGet]
        public ActionResult GetMateriasAsignadasByIdAlumno(int? IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Materias = new List<object>();
            alumnoMateria.Alumno = new ML.Alumno();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("materiasasignadas/" + IdAlumno);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.AlumnoMateria materiaItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.AlumnoMateria>(resultItem.ToString());
                        alumnoMateria.Materias.Add(materiaItem);
                    }

                    ServiceAlumno.AlumnoClient service = new ServiceAlumno.AlumnoClient();
                    var resultAlumno = service.GetById(IdAlumno.Value);

                    if (resultAlumno.Correct)
                    {
                        alumnoMateria.Alumno.IdAlumno = ((ML.Alumno)resultAlumno.Object).IdAlumno;
                        alumnoMateria.Alumno.Nombre = ((ML.Alumno)resultAlumno.Object).Nombre;
                        alumnoMateria.Alumno.ApellidoPaterno = ((ML.Alumno)resultAlumno.Object).ApellidoPaterno;
                        alumnoMateria.Alumno.ApellidoMaterno = ((ML.Alumno)resultAlumno.Object).ApellidoMaterno;
                        Session["IdAlumno"] = alumnoMateria.Alumno.IdAlumno;
                    }
                }
            }
            return View(alumnoMateria);
        }

        //[HttpPost]
        //public ActionResult GetMateriasAsignadasByIdAlumno(List<int> MateriasSeleccionadas)
        //{
        //    foreach (int idMateria in MateriasSeleccionadas)
        //    {
        //        ML.Result result = BL.AlumnoMateria.Delete(idMateria);
        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Se ha eliminado correctamente la materia";
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Ocurrio un problema al eliminar el alumno de la materia";
        //        }
        //    }
        //    return PartialView("Modal");
        //}
        [HttpPost]
        public ActionResult GetMateriasAsignadasByIdAlumno(List<int> MateriasSeleccionadas)
        {
            if (MateriasSeleccionadas != null)
            {
                int correct = 0;
                int incorrect = 0;

                foreach (int IdAlumnoMateria in MateriasSeleccionadas)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);

                        var postTasks = client.DeleteAsync("materiasasignadas/" + IdAlumnoMateria);
                        postTasks.Wait();

                        var result = postTasks.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            correct++;
                        }
                        else
                        {
                            incorrect++;
                        }
                    }
                }
                if (incorrect > 0)
                {
                    ViewBag.Message = "El alumno no se elimino en " + incorrect + " materias";
                }
                else
                {
                    ViewBag.Message = "El alumno se elimino en " + correct + " materias";
                }
            }
            else
            {
                ViewBag.Message = "No se elimino ninguna materia";
            }
            return PartialView("Modal");
        }

        //[HttpGet]
        //public ActionResult GetMateriasNoAsignadasByIdAlumno(int? IdAlumno)
        //{
        //    //Lista de Materias
        //    ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
        //    alumnoMateria.Materias = new List<object>();
        //    alumnoMateria.Alumno = new ML.Alumno();

        //    alumnoMateria.Alumno.IdAlumno = (alumnoMateria.Alumno.IdAlumno != 0) ? alumnoMateria.Alumno.IdAlumno : (int)Session["IdAlumno"];

        //    ML.Result resultMaterias = BL.AlumnoMateria.GetMateriasNoAsignadas(IdAlumno.Value);
        //    if (resultMaterias.Correct)
        //    {
        //        alumnoMateria.Materias = resultMaterias.Objects.ToList();

        //        ML.Result result = BL.Alumno.GetById(IdAlumno.Value);
        //        if (result.Correct)
        //        {
        //            alumnoMateria.Alumno.IdAlumno = ((ML.Alumno)result.Object).IdAlumno;
        //            alumnoMateria.Alumno.Nombre = ((ML.Alumno)result.Object).Nombre;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = resultMaterias.ErrorMessage;
        //    }
        //    return View(alumnoMateria);
        //}
        [HttpGet]
        public ActionResult GetMateriasNoAsignadasByIdAlumno(int? IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Materias = new List<object>();
            alumnoMateria.Alumno = new ML.Alumno();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("materiasnoasignadas/" + IdAlumno);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.AlumnoMateria materiaItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.AlumnoMateria>(resultItem.ToString());
                        alumnoMateria.Materias.Add(materiaItem);
                    }

                    ServiceAlumno.AlumnoClient service = new ServiceAlumno.AlumnoClient();
                    var resultAlumno = service.GetById(IdAlumno.Value);

                    if (resultAlumno.Correct)
                    {
                        alumnoMateria.Alumno.IdAlumno = ((ML.Alumno)resultAlumno.Object).IdAlumno;
                        alumnoMateria.Alumno.Nombre = ((ML.Alumno)resultAlumno.Object).Nombre;
                        alumnoMateria.Alumno.ApellidoPaterno = ((ML.Alumno)resultAlumno.Object).ApellidoPaterno;
                        alumnoMateria.Alumno.ApellidoMaterno = ((ML.Alumno)resultAlumno.Object).ApellidoMaterno;
                    }
                }
            }
            return View(alumnoMateria);
        }

        //[HttpPost]
        //public ActionResult GetMateriasNoAsignadasByIdAlumno(List<int> MateriasSeleccionadas)
        //{
        //    ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
        //    alumnoMateria.Materia = new ML.Materia();
        //    alumnoMateria.Alumno = new ML.Alumno();

        //    var IdAlumno = (int)Session["IdAlumno"];
        //    alumnoMateria.Alumno.IdAlumno = IdAlumno;

        //    foreach (int idMateria in MateriasSeleccionadas)
        //    {
        //        alumnoMateria.Materia.IdMateria = idMateria;
        //        ML.Result result = BL.AlumnoMateria.Add(alumnoMateria);
        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Se ha insertado correctamente la materia";
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Ocurrio un problema al insertar el alumno de la materia";
        //        }
        //    }
        //    return PartialView("Modal");
        //}
        [HttpPost]
        public ActionResult GetMateriasNoAsignadasByIdAlumno(List<int> MateriasSeleccionadas)
        {
            if(MateriasSeleccionadas != null)
            {
                ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                alumnoMateria.Materia = new ML.Materia();
                alumnoMateria.Alumno = new ML.Alumno();

                var IdAlumno = (int)Session["IdAlumno"];
                alumnoMateria.Alumno.IdAlumno = IdAlumno;

                int correct = 0;
                int incorrect = 0;

                foreach (int idMateria in MateriasSeleccionadas)
                {
                    alumnoMateria.Materia.IdMateria = idMateria;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);

                        var postTasks = client.PostAsJsonAsync("materiasasignadas", alumnoMateria);
                        postTasks.Wait();

                        var result = postTasks.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            correct++;
                        }
                        else
                        {
                            incorrect++;
                        }
                    }
                }
                if (incorrect > 0)
                {
                    ViewBag.Message = "El alumno no se inserto en " + incorrect + " materias";
                }
                else
                {
                    ViewBag.Message = "El alumno se inserto en " + correct + " materias";
                }
            }
            else
            {
                ViewBag.Message = "No se inserto ninguna materia";
            }
            return PartialView("Modal");
        }

    }
}