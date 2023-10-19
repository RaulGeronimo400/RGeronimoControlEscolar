using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Login" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Login.svc o Login.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Login : ILogin
    {
        public SL_WebService.Result SingIn(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetByNombre(usuario);
            SL_WebService.Result resultSL = new SL_WebService.Result();

            resultSL.Correct = result.Correct;
            resultSL.ErrorMessage = result.ErrorMessage;
            resultSL.Ex = result.Ex;
            resultSL.Object = result.Object;
            resultSL.Objects = result.Objects;

            return resultSL;
        }

        public SL_WebService.Result Registrar(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            SL_WebService.Result resultSL = new SL_WebService.Result();

            resultSL.Correct = result.Correct;
            resultSL.ErrorMessage = result.ErrorMessage;
            resultSL.Ex = result.Ex;
            resultSL.Object = result.Object;
            resultSL.Objects = result.Objects;

            return resultSL;
        }
    }
}
