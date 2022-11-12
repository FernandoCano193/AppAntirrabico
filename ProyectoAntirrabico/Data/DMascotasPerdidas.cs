using System;
using System.Collections.Generic;
using System.Text;
//LIBRERIAS NECESARIAS
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Conexion;
using Firebase.Database.Query;
using Firebase.Database;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using Firebase.Storage;

namespace ProyectoAntirrabico.Data
{
    public class DMascotasPerdidas
    {
        List<MMascotasPerdidas> MPerdidas = new List<MMascotasPerdidas>();
        string rutaFoto;
        string IDMascotaP;


        //Insercion de Mascotas Perdidas
        public async Task<string> InsertarMascotasPerdidas(MMascotasPerdidas parametros)
        {
            var data = await CConexion.firebase
                .Child("MascotasPerdidas")
                .PostAsync(new MMascotasPerdidas()
                {
                    Area = parametros.Area,
                    Colores = parametros.Colores,
                    Estado = parametros.Estado,
                    Edad = parametros.Edad,
                    Especie = parametros.Especie,
                    LinkFoto = parametros.LinkFoto,
                    Raza = parametros.Raza,
                    Sexo = parametros.Sexo,
                    IdMascotaPerdida = parametros.IdMascotaPerdida
                });

            IDMascotaP = data.Key;
            return IDMascotaP;
        }

        public async Task EliminarMascotasPerdidas(MMascotasPerdidas mascota)
        {
            var MascotaEliminar = (await CConexion.firebase
                .Child("MascotasPerdidas")
                .OnceAsync<MMascotasPerdidas>()).Where(a => a.Key == mascota.IdMascotaPerdida).FirstOrDefault();

            await CConexion.firebase
                .Child("MascotasPerdidas")
                .Child(MascotaEliminar.Key).DeleteAsync();

        }

        public async Task EliminarFoto(string nombre)
        {
            await new FirebaseStorage("appmascotas-a2b71.appspot.com")
                .Child("MascotasPerdidas")
                .Child(nombre).DeleteAsync();
        }

        public async Task EditarFoto(MMascotasPerdidas parametros)
        {
            var data = (await CConexion.firebase
                .Child("MascotasPerdidas")
                .OnceAsync<MMascotasPerdidas>()).Where(a => a.Key == parametros.IdMascotaPerdida).FirstOrDefault();

            await CConexion.firebase
                .Child("MascotasPerdidas")
                .Child(data.Key)
                .PutAsync(new MMascotasPerdidas()
                {
                    LinkFoto = parametros.LinkFoto,
                    Raza = parametros.Raza,
                    Estado = parametros.Estado,
                    Edad = parametros.Edad,
                    Colores = parametros.Colores,
                    Area = parametros.Area,
                    IdMascotaPerdida = data.Key,
                    Especie = parametros.Especie,
                    Sexo = parametros.Sexo
                });
        }

        //Consultas  Mascotas Perididas con un ObservableCollection
        public async Task<ObservableCollection<MMascotasPerdidas>> MostrarMP()
        {
            var data = await Task.Run(() => CConexion.firebase
            .Child("MascotasPerdidas")
            .AsObservable<MMascotasPerdidas>()
            .AsObservableCollection());

            return data;
        }

        //Consultas  Mascotas Perididas con una lista
        public async Task<List<MMascotasPerdidas>> MostrarMPList()
        {
            var data = await CConexion.firebase
                .Child("MascotasPerididas")
                .OrderByKey()
                .OnceAsync<MMascotasPerdidas>();
            foreach(var mascota in data)
            {
                MMascotasPerdidas parametros = new MMascotasPerdidas();
                parametros.IdMascotaPerdida = mascota.Key;
                parametros.Area = mascota.Object.Area;
                parametros.Colores = mascota.Object.Colores;
                parametros.Edad = mascota.Object.Edad;
                parametros.Especie = mascota.Object.Especie;
                parametros.LinkFoto = mascota.Object.LinkFoto;
                parametros.Raza = mascota.Object.Raza;
                parametros.Sexo = mascota.Object.Sexo;
                MPerdidas.Add(parametros);
            }

            return MPerdidas;
        }

        public async Task<string> SubirFotoStorage(Stream FotoStream,string IdMascota)
        {
            //SUBE LA IMAGEN AL ALMACENAMIENTO
            var FotoAlmacenada = await CConexion.storage.Child("MascotasPerdidas")
                .Child(IdMascota + ".jpg")
                .PutAsync(FotoStream);

            rutaFoto = FotoAlmacenada;
            return rutaFoto;
        }
    }
}
