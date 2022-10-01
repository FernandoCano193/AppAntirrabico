using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database; //INVOCANDO LAS LIBRERIAS DEL PAQUETE NuGet
using Firebase.Storage;

namespace ProyectoAntirrabico.Conexion
{
    public class CConexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://appmascotas-a2b71-default-rtdb.firebaseio.com/");
        
        //RUTA PARA EL ALMACENAMIENTO DE LAS FOTOS
        public static FirebaseStorage storage = new FirebaseStorage("appmascotas-a2b71.appspot.com");
    }
}
