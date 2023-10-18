using System;
using System.Collections.Generic;
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
                using (DL.RGeronimoControlEscolarEntities context = new DL.RGeronimoControlEscolarEntities())
                {
                    var query = context.UsuarioAdd(usuario.Nombre, usuario.Password);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un problema al insertar el usuario";
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
                using (DL.RGeronimoControlEscolarEntities context = new DL.RGeronimoControlEscolarEntities())
                {
                    var query = context.UsuarioGetByNombrePassword(usuario.Nombre, usuario.Password).FirstOrDefault();
                    if (query != null)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no registrado";
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
