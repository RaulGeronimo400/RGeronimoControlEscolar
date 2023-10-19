using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ILogin" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ILogin
    {
        [OperationContract]
        SL_WebService.Result SingIn(ML.Usuario usuario);

        [OperationContract]
        SL_WebService.Result Registrar(ML.Usuario usuario);
    }
}
