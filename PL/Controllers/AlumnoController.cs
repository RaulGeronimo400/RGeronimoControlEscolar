using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        [HttpGet]
        public ActionResult GetAll()
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

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            if (IdAlumno == null)
            {
                return View(alumno);
            }
            else
            {
                //ML.Result result = BL.Alumno.GetById(IdAlumno.Value);

                ServiceAlumno.AlumnoClient service = new ServiceAlumno.AlumnoClient();
                var result = service.GetById(IdAlumno.Value);

                alumno.IdAlumno = ((ML.Alumno)result.Object).IdAlumno;
                alumno.Nombre = ((ML.Alumno)result.Object).Nombre;
                alumno.ApellidoPaterno = ((ML.Alumno)result.Object).ApellidoPaterno;
                alumno.ApellidoMaterno = ((ML.Alumno)result.Object).ApellidoMaterno;
                return View(alumno);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                if (alumno.IdAlumno == 0)
                {
                    //ML.Result result = BL.Alumno.Add(alumno);

                    ServiceAlumno.AlumnoClient service = new ServiceAlumno.AlumnoClient();
                    var result = service.Add(alumno);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha ingresado correctamente el alumno";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un problema al insertar el alumno";
                    }
                }
                else
                {
                    //ML.Result result = BL.Alumno.Update(alumno);

                    ServiceAlumno.AlumnoClient service = new ServiceAlumno.AlumnoClient();
                    var result = service.Update(alumno);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha actualizado correctamente el alumno";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un problema al actualizar el alumno";
                    }
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdAlumno)
        {
            //ML.Result result = BL.Alumno.Delete(IdAlumno);

            ServiceAlumno.AlumnoClient service = new ServiceAlumno.AlumnoClient();
            var result = service.Delete(IdAlumno);

            if (result.Correct)
            {
                ViewBag.Message = "Se ha eliminado correctamente el alumno";
            }
            else
            {
                ViewBag.Message = "Ocurrio un problema al eliminar el alumno";
            }
            return PartialView("Modal");
        }
    }
}