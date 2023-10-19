using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = context.CreateCommand();

                    cmd.CommandText = "UsuarioAdd";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@Usuario", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);

                    context.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al insertar al usuario";
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

        public static ML.Result GetByNombre(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "UsuarioGetByNombrePassword";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@Usuario", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    if(table.Rows.Count > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el usuario";
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
