using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = context.CreateCommand();

                    cmd.CommandText = "AlumnoAdd";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", alumno.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", alumno.ApellidoMaterno);

                    context.Open();

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
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

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = context.CreateCommand();

                    cmd.CommandText = "AlumnoUpdate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@Nombre", alumno.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", alumno.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", alumno.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@IdAlumno", alumno.IdAlumno);

                    context.Open();

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al actualizar";
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

        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = context.CreateCommand();

                    cmd.CommandText = "AlumnoDelete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@IdAlumno", IdAlumno);

                    context.Open();

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al eliminar";
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = context.CreateCommand();

                    cmd.CommandText = "AlumnoGetAll";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in table.Rows)
                        {
                            ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = int.Parse(row[0].ToString());
                            alumno.Nombre = row[1].ToString();
                            alumno.ApellidoPaterno = row[2].ToString();
                            alumno.ApellidoMaterno = row[3].ToString();

                            result.Objects.Add(alumno);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
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

        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "AlumnoGetById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@IdAlumno", IdAlumno);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        result.Object = new Object();
                        DataRow row = table.Rows[0];

                        ML.Alumno alumno = new ML.Alumno();

                        alumno.IdAlumno = int.Parse(row[0].ToString());
                        alumno.Nombre = row[1].ToString();
                        alumno.ApellidoPaterno = row[2].ToString();
                        alumno.ApellidoMaterno = row[3].ToString();

                        result.Object = alumno;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el alumno";
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
