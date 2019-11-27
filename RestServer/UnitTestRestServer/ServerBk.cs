using Nancy;
using Nancy.Extensions;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;

namespace NancyRest
{
    public class ServerBK : NancyModule
    {
        public ServerBK() : base("/")
        {

            
            this.After.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            });

            Get("/paises", _ => {
                string response = SQLManager.getPaises().ToString();
                Console.WriteLine("Response:\n" + response);
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Get("/ejercicios", _ => {
                string response = SQLManager.getEjercicios().ToString();
                Console.WriteLine("Response:\n" + response);
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Get("/admins", _ => {
                string response = SQLManager.getAdmins().ToString();
                Console.WriteLine("Response:\n" + response);
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Get("/deportes", _ => {
                string response = SQLManager.getDeportes().ToString();
                Console.WriteLine("Response:\n" + response);
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Get("/", _ => {
                return "Za Warudo from Server!";
            });


            Post("/posiciones", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreDeporte = data["nombreDeporte"].ToString();

                string response = SQLManager.getPocisiones(nombreDeporte).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/provincias", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreProvincia = data["nombrePais"].ToString();

                string response = SQLManager.getProvincias(nombreProvincia).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/equipos", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string idEntrenador = data["correo"].ToString();

                string response = SQLManager.getEquipos(idEntrenador).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/login", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string idusuario = data["correo"].ToString();
                string password = data["password"].ToString();


                string response = SQLManager.login(idusuario, password).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/universidades", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombrePais = data["nombrePais"].ToString();

                string response = SQLManager.getUniversidades(nombrePais).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/crearAtl", x =>
            {
                Console.WriteLine("post: /crearAtl");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreC = data["nombre"].ToString();
                int cedula = int.Parse(data["cedula"].ToString());
                string apellidoC = data["apellido"].ToString();
                int carne = int.Parse(data["carne"].ToString());
                string provincia = data["provincia"].ToString();
                string email1 = data["correo1"].ToString();
                string email2 = data["correo2"].ToString();
                int telefonoM = int.Parse(data["telefono"].ToString());
                string foto = data["foto"].ToString();
                string pais = data["pais"].ToString();
                string universidad = data["universidad"].ToString();
                string password = data["password"].ToString();
                string deporte = data["deporte"].ToString();
                string fechaNacimiento = data["fechaNacimiento"].ToString();
                int posicion = int.Parse(data["posicion"].ToString());
                int posicionSecundaria = int.Parse(data["posicionSecundaria"].ToString());
                float altura = float.Parse(data["altura"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float peso = float.Parse(data["peso"].ToString(), CultureInfo.InvariantCulture.NumberFormat);

                string response = SQLManager.insertAtleta(nombreC, cedula, apellidoC, provincia, email1, email2, telefonoM, foto, pais, universidad, password, deporte, altura, peso, fechaNacimiento, posicion, posicionSecundaria, carne).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };

            });


            Post("/crearEnt", x =>
            {
                Console.WriteLine("post: /crearEnt");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreC = data["nombre"].ToString();
                string apellidoC = data["apellido"].ToString();
                string email = data["correo"].ToString();
                string pais = data["pais"].ToString();
                string universidad = data["universidad"].ToString();
                string password = data["password"].ToString();

                string response = SQLManager.insertEntr(nombreC, apellidoC, email, pais, universidad, password).ToString();


                Console.WriteLine("Response:\n" + response);
                
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/evaluarAtlEntr", x =>
            {
                Console.WriteLine("post: /crearEnt");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string correoAtleta = data["correo"].ToString();

                float calificacion = float.Parse(data["calificacion"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float tiempoDC = float.Parse(data["tiempoDC"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float tiempoDL = float.Parse(data["tiempoDL"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float salto = float.Parse(data["salto"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float tiempoPH = float.Parse(data["tiempoPH"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float pase = float.Parse(data["pase"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float pruebaHR = float.Parse(data["pruebaHR"].ToString(), CultureInfo.InvariantCulture.NumberFormat);


                string response = SQLManager.evaluarAtlEntr(calificacion, tiempoDC, tiempoDL, salto, tiempoPH, pase, pruebaHR, correoAtleta).ToString();
                Console.WriteLine("Response:\n" + response);                

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/evaluarAtlPart", x =>
            {
                Console.WriteLine("post: /crearEnt");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string correo = data["correo"].ToString();
                string temporada = data["temporada"].ToString();
                float calificacion = float.Parse(data["calificacion"].ToString(), CultureInfo.InvariantCulture.NumberFormat);


                string estadoP = data["estadoP"].ToString();
                int goles = int.Parse(data["goles"].ToString());
                int asistencias = int.Parse(data["asistencias"].ToString());
                int balonesR = int.Parse(data["balonesR"].ToString());
                int pasesExit = int.Parse(data["pasesExit"].ToString());
                int pases = int.Parse(data["pases"].ToString());
                int centros = int.Parse(data["centros"].ToString());
                int centrosExit = int.Parse(data["centrosExit"].ToString());
                int tarjetasAmar = int.Parse(data["tarjetasAmar"].ToString());
                int tarjetasRoj = int.Parse(data["tarjetasRoj"].ToString());
                int penales = int.Parse(data["penales"].ToString());
                int rematesSalv = int.Parse(data["rematesSalv"].ToString());
                int golesRecib = int.Parse(data["golesRecib"].ToString());

                string response = SQLManager.evaluarAtlPart(estadoP, goles, asistencias, balonesR, pasesExit, pases, centros, centrosExit, tarjetasAmar, tarjetasRoj,
                                                                penales, rematesSalv, golesRecib, correo, temporada, calificacion).ToString();
                Console.WriteLine("Response:\n" + response);
                
                var jsonBytes = Encoding.UTF8.GetBytes(response);

                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });




            Post("/crearScout", x =>
            {
                Console.WriteLine("post: /crearScout");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreC = data["nombre"].ToString();
                string apellidoC = data["apellido"].ToString();
                string email = data["correo"].ToString();
                string password = data["password"].ToString();

                string response = SQLManager.insertTrab(nombreC, apellidoC, email, password, 1).ToString();
                Console.WriteLine("Response:\n" + response);

            
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/crearAdmin", x =>
            {
                Console.WriteLine("post: /crearAdmin");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreC = data["nombre"].ToString();
                string apellidoC = data["apellido"].ToString();
                string email = data["correo"].ToString();
                string password = data["password"].ToString();

                string response = SQLManager.insertTrab(nombreC, apellidoC, email, password, 0).ToString();

                Console.WriteLine("Response:\n" + response);

               
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/insertEquip", x =>
            {
                Console.WriteLine("post: /crearAdmin");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreEquip = data["nombreEquipo"].ToString();
                string jugador = data["jugador"].ToString();

                string response = SQLManager.insertEquip(nombreEquip, jugador).ToString();
                Console.WriteLine("Response:\n" + response);

            
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/crearEquip", x =>
            {
                Console.WriteLine("post: /crearAdmin");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreEquip = data["nombreEquipo"].ToString();
                string idEntrenador = data["correoEntrenador"].ToString();
                string temporada = data["nombreTemporada"].ToString();

                string response = SQLManager.crearEquip(nombreEquip, idEntrenador, temporada).ToString();
                Console.WriteLine("Response:\n" + response);
                
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/jugadoresEquip", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombrEquipo = data["nombreEquipo"].ToString();

                string response = SQLManager.getMiembrosEquipo(nombrEquipo).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/buscarJugadorNombre", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreJ = data["nombre"].ToString();
                string apellidoJ = data["apellido"].ToString();

                string response = SQLManager.getJugadorNombre(nombreJ, apellidoJ).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/buscarJugadorID", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                int idJugador = int.Parse(data["cedula"].ToString());

                string response = SQLManager.getJugadorID(idJugador).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/desactivarCuenta", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string emailCuenta = data["correo"].ToString();

                string response = SQLManager.desactivarCuenta(emailCuenta).ToString();
                Console.WriteLine("Response:\n" + response);
               
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/activarCuenta", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string emailCuenta = data["correo"].ToString();

                string response = SQLManager.activarCuenta(emailCuenta).ToString();
                Console.WriteLine("Response:\n" + response);


                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/atletacorreo", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string emailCuenta = data["correo"].ToString();

                string response = SQLManager.getInfoAtleta(emailCuenta).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/planesEmailSem", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string emailCuenta = data["correo"].ToString();

                int semana = int.Parse(data["semana"].ToString());

                string response = SQLManager.getPlanesAtleta(emailCuenta, semana).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/semanasPlanes", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string emailCuenta = data["correo"].ToString();

                string response = SQLManager.getSemanasPAtleta(emailCuenta).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/reporteRango", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                JArray filtros = data["campos"] as JArray;

                string response = SQLManager.getReporteAtleta(filtros).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/temporadaXequipo", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreEquipo = data["nombreEquipo"].ToString();

                string response = SQLManager.getTemporadasEquipo(nombreEquipo).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/crearTemporada", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreTemporada = data["nombreTemporada"].ToString();

                string response = SQLManager.crearTemporada(nombreTemporada).ToString();
                Console.WriteLine("Response:\n" + response);
                
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });


            Post("/nuevoEjercicio", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreEjercicio = data["nombreEjercicio"].ToString();

                string response = SQLManager.agregarEjercicio(nombreEjercicio).ToString();
                Console.WriteLine("Response:\n" + response);

                
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/nuevaUniversidad", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreUniversidad = data["nombreUniversidad"].ToString();

                string nombrePais = data["nombrePais"].ToString();

                string response = SQLManager.agregarUniversidad(nombreUniversidad, nombrePais).ToString();
                Console.WriteLine("Response:\n" + response);

                
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/nuevaPosicion", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombrePosicion = data["nombrePosicion"].ToString();

                string deporte = data["deporte"].ToString();

                string response = SQLManager.agregarPosicion(nombrePosicion, deporte).ToString();
                Console.WriteLine("Response:\n" + response);
                return response;

                
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/asignarPP", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string idAtleta = data["correo"].ToString();

                int semana = int.Parse(data["semana"].ToString());

                int ejercico = int.Parse(data["idEjercicio"].ToString());

                int dia = int.Parse(data["dia"].ToString());

                int cantidad = int.Parse(data["cantidad"].ToString());

                string response = SQLManager.asignarPP(idAtleta, semana, ejercico, dia, cantidad).ToString();
                Console.WriteLine("Response:\n" + response);
              
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });
        }
    }
}