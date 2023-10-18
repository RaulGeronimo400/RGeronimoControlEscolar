using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_API.Controllers
{
    public class MateriaController : ApiController
    {
        [HttpGet]
        [Route("api/materia")]
        public IHttpActionResult Get()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = BL.Materia.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET: api/Materia/5
        [HttpGet]
        [Route("api/materia/{IdMateria}")]
        public IHttpActionResult Get(int IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = BL.Materia.GetById(IdMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Materia
        [HttpPost]
        [Route("api/materia")]
        public IHttpActionResult Post([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.Add(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Materia/5
        [HttpPut]
        [Route("api/materia/{IdMateria}")]
        public IHttpActionResult Put(int IdMateria, [FromBody] ML.Materia materia)
        {
            materia.IdMateria = IdMateria;

            ML.Result result = BL.Materia.Update(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Materia/5
        [HttpDelete]
        [Route("api/materia/{IdMateria}")]
        public IHttpActionResult Delete(int IdMateria)
        {
            ML.Result result = BL.Materia.Delete(IdMateria);
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
