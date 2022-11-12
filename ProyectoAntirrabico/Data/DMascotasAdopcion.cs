using System;
using System.Collections.Generic;
using System.Text;
//LIBRERIAS NECESARIAS
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Conexion;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using System.Collections.ObjectModel;
using System.IO;
using Firebase.Storage;

namespace ProyectoAntirrabico.Data
{
    public class DMascotasAdopcion
    {
        string rutaFoto;
        string IDMascotaA;
        List<MMascotasAdopocion> MAdopcion = new List<MMascotasAdopocion>();

        //INSERTAR
        public async Task<string> InsertarMA(MMascotasAdopocion parametros)
        {
            var data = await CConexion.firebase
                .Child("MascotasAdopcion")
                .PostAsync(new MMascotasAdopocion()
                {
                    Area = parametros.Area,
                    Colores = parametros.Colores,
                    Nombre = parametros.Nombre,
                    Edad = parametros.Edad,
                    Especie = parametros.Especie,
                    LinkFoto = parametros.LinkFoto,
                    Raza = parametros.Raza,
                    Sexo = parametros.Sexo,
                    IdMascotaAdopcion = parametros.IdMascotaAdopcion
                });

            IDMascotaA = data.Key;
            return IDMascotaA;
        }

        public async Task EditarFoto(MMascotasAdopocion parametros)
        {
            var data = (await CConexion.firebase
                .Child("MascotasAdopcion")
                .OnceAsync<MMascotasAdopocion>()).Where(a => a.Key == parametros.IdMascotaAdopcion).FirstOrDefault();

            await CConexion.firebase
                .Child("MascotasAdopcion")
                .Child(data.Key)
                .PutAsync(new MMascotasAdopocion()
                {
                    LinkFoto = parametros.LinkFoto,
                    Edad = parametros.Edad,
                    Colores = parametros.Colores,
                    Area = parametros.Area,
                    IdMascotaAdopcion = data.Key,
                    Especie = parametros.Especie,
                    Nombre = parametros.Nombre,
                    Raza = parametros.Raza,
                    Sexo = parametros.Sexo
                });
        }

        //CONSULTAR
        public async Task<ObservableCollection<MMascotasAdopocion>> MostrarMA()
        {
            var data = await Task.Run(() => CConexion.firebase
            .Child("MascotasAdopcion")
            .AsObservable<MMascotasAdopocion>()
            .AsObservableCollection());

            return data;
        }

        public async Task<List<MMascotasAdopocion>> MostrarMAList()
        {
            var data = await CConexion.firebase
                .Child("MascotasAdopcion")
                .OrderByKey()
                .OnceAsync<MMascotasAdopocion>();

            foreach(var mascota in data)
            {
                MMascotasAdopocion parametros = new MMascotasAdopocion();
                parametros.IdMascotaAdopcion = mascota.Key;
                parametros.Area = mascota.Object.Area;
                parametros.Colores = mascota.Object.Colores;
                parametros.Edad = mascota.Object.Edad;
                parametros.Especie = mascota.Object.Especie;
                parametros.LinkFoto = mascota.Object.LinkFoto;
                parametros.Raza = mascota.Object.Raza;
                parametros.Sexo = mascota.Object.Sexo;
                parametros.Nombre = mascota.Object.Nombre;
                MAdopcion.Add(parametros);
            }

            return MAdopcion;
        }

        public async Task EliminarMascotasPerdidas(MMascotasAdopocion mascota)
        {
            var MascotaEliminar = (await CConexion.firebase
                .Child("MascotasAdopcion")
                .OnceAsync<MMascotasAdopocion>()).Where(a => a.Key == mascota.IdMascotaAdopcion).FirstOrDefault();

            await CConexion.firebase
                .Child("MascotasAdopcion")
                .Child(MascotaEliminar.Key).DeleteAsync();

        }

        public async Task EliminarFoto(string nombre)
        {
            await new FirebaseStorage("appmascotas-a2b71.appspot.com")
                .Child("MascotasAdopcion")
                .Child(nombre).DeleteAsync();
        }

        public async Task<string> SubirFotoStorage(Stream FotoStream, string IdMascota)
        {
            //SUBE LA IMAGEN AL ALMACENAMIENTO
            var FotoAlmacenada = await CConexion.storage.Child("MascotasAdopcion")
                .Child(IdMascota + ".jpg")
                .PutAsync(FotoStream);

            rutaFoto = FotoAlmacenada;
            return rutaFoto;
        }
    }

}
