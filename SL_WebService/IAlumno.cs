using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IAlumno" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAlumno
    {
        [OperationContract]
        SL_WebService.Result Add(ML.Alumno alumno);

        [OperationContract]
        SL_WebService.Result Update(ML.Alumno alumno);

        [OperationContract]
        SL_WebService.Result Delete(int IdAlumno);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        SL_WebService.Result GetAll();

        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        SL_WebService.Result GetById(int IdAlumno);
    }
}
