using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result Add(ML.AlumnoMateria alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGeronimoControlEscolarEntities context = new DL.RGeronimoControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaAdd(
                        alumno.Alumno.IdAlumno,
                        alumno.Materia.IdMateria);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al insertar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdAlumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGeronimoControlEscolarEntities context = new DL.RGeronimoControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaDelete(IdAlumnoMateria);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio u problema al eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetMateriasAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGeronimoControlEscolarEntities context = new DL.RGeronimoControlEscolarEntities())
                {
                    var query = context.GetMateriasAsignadasByIdAlumno(IdAlumno).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Alumno = new ML.Alumno();
                            alumnoMateria.Materia = new ML.Materia();

                            alumnoMateria.IdAlumnoMateria = item.IdAlumnoMateria;
                            alumnoMateria.Alumno.IdAlumno = item.IdAlumno;
                            alumnoMateria.Materia.IdMateria = item.IdMateria.Value;

                            alumnoMateria.Alumno.Nombre = item.Alumno;
                            alumnoMateria.Materia.Nombre = item.Materia;
                            alumnoMateria.Materia.Costo = item.Costo.Value;

                            result.Objects.Add(alumnoMateria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al mostrar los datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetMateriasNoAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RGeronimoControlEscolarEntities context = new DL.RGeronimoControlEscolarEntities())
                {
                    result.Objects = new List<object>();
                    var query = context.GetMateriasNoAsignadasByIdAlumno(IdAlumno).ToList();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.AlumnoMateria materia = new ML.AlumnoMateria();
                            materia.Materia = new ML.Materia();
                            materia.Materia.IdMateria = obj.IdMateria;
                            materia.Materia.Nombre = obj.Nombre;
                            materia.Materia.Costo = obj.Costo.Value;

                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Lista vacia";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
