using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_API.Controllers
{
    public class AlumnoMateriaController : ApiController
    {
        // GET: api/AlumnoMateria
        [HttpGet]
        [Route("api/materiasasignadas/{IdAlumno}")]
        public IHttpActionResult Get(int IdAlumno)
        {
            ML.AlumnoMateria materia = new ML.AlumnoMateria();
            ML.Result result = BL.AlumnoMateria.GetMateriasAsignadas(IdAlumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("api/materiasasignadas/{IdAlumnoMateria}")]
        public IHttpActionResult Delete(int IdAlumnoMateria)
        {
            ML.AlumnoMateria materia = new ML.AlumnoMateria();
            ML.Result result = BL.AlumnoMateria.Delete(IdAlumnoMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/materiasnoasignadas/{IdAlumno}")]
        public IHttpActionResult Get2(int IdAlumno)
        {
            ML.AlumnoMateria materia = new ML.AlumnoMateria();
            ML.Result result = BL.AlumnoMateria.GetMateriasNoAsignadas(IdAlumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/materiasasignadas")]
        public IHttpActionResult Add(ML.AlumnoMateria alumnoMateria)
        {
            ML.AlumnoMateria materia = new ML.AlumnoMateria();
            ML.Result result = BL.AlumnoMateria.Add(alumnoMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
