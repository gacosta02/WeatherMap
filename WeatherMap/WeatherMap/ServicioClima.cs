

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherMap.Models;

namespace WeatherMap
{
   public static class ServicioClima
    {
        static string Key = "33477f4e9b1c57a0af148c5e98a1a733";
     

        public static async Task<Clima> ConsultarClima(string ciudad)
        {
            var conexion = $"http://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid={Key}";

            using (var cliente = new HttpClient())
            {
                var peticion = await cliente.GetAsync(conexion);

                if (peticion != null)
                {
                    var json = peticion.Content.ReadAsStringAsync().Result;
                    var datos = (JContainer)JsonConvert.DeserializeObject(json);

                    if (datos["weather"] != null)
                    {
                        var clima = new Clima();
                        clima.Titulo = (string)datos["name"];
                        clima.Temperatura = ((float)datos["main"]["temp"] - 273.15).ToString("N2") + " °C";
                        clima.Viento = (string)datos["wind"]["speed"] + " mph";
                        clima.Humedad = (string)datos["main"]["humidity"] + " %";
                        clima.Visibilidad = (string)datos["weather"][0]["main"];

                        var fechaBase = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        var amanecer = fechaBase.AddSeconds((double)datos["sys"]["sunrise"]);
                        var ocaso = fechaBase.AddSeconds((double)datos["sys"]["sunset"]);
                        clima.Amanecer = amanecer.ToString() + " UTC";
                        clima.Ocaso = ocaso.ToString() + " UTC";
                        return clima;
                    }
                }
            }
            return default(Clima);
        }
    }
}
