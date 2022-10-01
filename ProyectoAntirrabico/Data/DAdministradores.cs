using System;
using System.Collections.Generic;
using System.Text;
//LIBRERIAS REQUERIDAS
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Conexion;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ProyectoAntirrabico.Data
{
    public class DAdministradores
    {
        string rutaFoto;
        string IDAdministrador;


        //Inserción de administradores
        public async Task<string> InsertarAdmin(MAdmistradores parametros)
        {
            var admin = await CConexion.firebase
                .Child("Administradores")
                .PostAsync(new MAdmistradores()
                {
                    Apellidos = parametros.Apellidos,
                    Nombres = parametros.Nombres,
                    Area = parametros.Area,
                    LinkFoto = parametros.LinkFoto,
                    Contraseña = parametros.Contraseña,
                    Correo = parametros.Correo
                });

            IDAdministrador = admin.Key;
            return IDAdministrador;
        }

        public async Task EditarFoto(MAdmistradores parametros)
        {
            var data = (await CConexion.firebase
                .Child("Administradores")
                .OnceAsync<MAdmistradores>()).Where(a => a.Key == parametros.IdAdministrador).FirstOrDefault();

            await CConexion.firebase
                .Child("Administradores")
                .Child(data.Key)
                .PutAsync(new MAdmistradores()
                {
                    Apellidos = parametros.Apellidos,
                    Area = parametros.Area,
                    Contraseña = parametros.Contraseña,
                    Correo = parametros.Correo,
                    LinkFoto = parametros.LinkFoto,
                    Nombres = parametros.Nombres
                });
        }

        public async Task<string> SubirFotoStorage(Stream FotoStream, string IdAdmin)
        {
            var FotoAlmacenada = await CConexion.storage.Child("Administradores")
                .Child(IdAdmin+".jpg")
                .PutAsync(FotoStream);

            rutaFoto = FotoAlmacenada;
            return rutaFoto;
        }
    }
}
