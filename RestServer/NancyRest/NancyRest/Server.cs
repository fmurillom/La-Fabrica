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
    public class Server : NancyModule
    {
        public Server() : base("/")
        {

            /*
            this.After.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            });*/

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



            Get("/estadosPart", _ => {
                string response = SQLManager.getEstadosPart().ToString();
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

            Get("/estadoLesion", _ => {
                string response = SQLManager.getEstadoLesion().ToString();
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

                string idEntrenador = data["idEntrenador"].ToString();

                string response = SQLManager.getEquipos(idEntrenador).ToString();
                Console.WriteLine("Response:\n" + response);

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/equiposTemporadas", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string idEntrenador = data["correo"].ToString();

                string response = SQLManager.getEquiposTemporadas(idEntrenador).ToString();
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

                bool response = SQLManager.insertAtleta(nombreC, cedula, apellidoC, provincia, email1, email2, telefonoM, foto, pais, universidad, password, deporte, altura, peso, fechaNacimiento, posicion, posicionSecundaria);
                Console.WriteLine("Response:\n" + response);

                return response;
                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });


                string universidad = data["universidad"].ToString();
                

                string response = SQLManager.getAtletasUniversidad(universidad).ToString();

                Console.WriteLine("Response:\n" + response);
               
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };
            });

            Post("/crearLesion", x =>
            {
                Console.WriteLine("post: /crearEnt");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string correo = data["correo"].ToString();
                string fechaInicio = data["fechaInicio"].ToString();
                string fechaFinal = data["fechaFinal"].ToString();
                int gravedad = int.Parse(data["gravedad"].ToString());
                string descripcion = data["descripcion"].ToString();

                bool response = SQLManager.insertLesion(correo, fechaInicio, fechaFinal, gravedad, descripcion);

                return response;

                Console.WriteLine("Response:\n" + response);
                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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

                bool response = SQLManager.insertEntr(nombreC, apellidoC, email, pais, universidad, password);

                return response;

                Console.WriteLine("Response:\n" + response);
                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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


                bool response = SQLManager.evaluarAtlEntr(calificacion, tiempoDC, tiempoDL, salto, tiempoPH, pase, pruebaHR, correoAtleta);
                Console.WriteLine("Response:\n" + response);

                return response;

                /*

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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


                int estadoP = int.Parse(data["estadoP"].ToString());
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

                bool response = SQLManager.evaluarAtlPart(estadoP, goles, asistencias, balonesR, pasesExit, pases, centros, centrosExit, tarjetasAmar, tarjetasRoj,
                                                                penales, rematesSalv, golesRecib, correo, temporada, calificacion);
                Console.WriteLine("Response:\n" + response);

                return response;
                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);


               

                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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

                bool response = SQLManager.insertTrab(nombreC, apellidoC, email, password, 1);
                Console.WriteLine("Response:\n" + response);

                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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

                bool response = SQLManager.insertTrab(nombreC, apellidoC, email, password, 0);

                Console.WriteLine("Response:\n" + response);

                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });


            Post("/insertEquip", x =>
            {
                Console.WriteLine("post: /crearAdmin");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreEquip = data["nombreEquipo"].ToString();
                string jugador = data["jugador"].ToString();

                bool response = SQLManager.insertEquip(nombreEquip, jugador);
                Console.WriteLine("Response:\n" + response);

                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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

                bool response = SQLManager.crearEquip(nombreEquip, idEntrenador, temporada);
                Console.WriteLine("Response:\n" + response);

                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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

                bool response = SQLManager.desactivarCuenta(emailCuenta);
                Console.WriteLine("Response:\n" + response);

                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/activarCuenta", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string emailCuenta = data["correo"].ToString();

                bool response = SQLManager.activarCuenta(emailCuenta);
                Console.WriteLine("Response:\n" + response);

                return response;
                /*

                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
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

            Post("/planesEmail", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string emailCuenta = data["correo"].ToString();

                string response = SQLManager.getPlanesAtleta(emailCuenta).ToString();
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

                JArray filtros = data["filtros"] as JArray;

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

                bool response = SQLManager.crearTemporada(nombreTemporada);
                Console.WriteLine("Response:\n" + response);

                return response;
                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });


            Post("/nuevoEjercicio", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreEjercicio = data["nombreEjercicio"].ToString();

                bool response = SQLManager.agregarEjercicio(nombreEjercicio);
                Console.WriteLine("Response:\n" + response);

                return response;
                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/nuevaUniversidad", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreUniversidad = data["nombreUniversidad"].ToString();

                string nombrePais = data["nombrePais"].ToString();

                bool response = SQLManager.agregarUniversidad(nombreUniversidad, nombrePais);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });


            Post("/delUniversidad", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombreUniversidad = data["nombreUniversidad"].ToString();

                string nombrePais = data["nombrePais"].ToString();

                bool response = SQLManager.deleteUniversidad(nombreUniversidad, nombrePais);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/nuevaPosicion", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombrePosicion = data["nombrePosicion"].ToString();

                string deporte = data["deporte"].ToString();

                bool response = SQLManager.agregarPosicion(nombrePosicion, deporte);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/delPosicion", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string nombrePosicion = data["nombrePosicion"].ToString();

                string deporte = data["deporte"].ToString();

                bool response = SQLManager.eliminarPosicion(nombrePosicion, deporte);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/nuevoPais", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string pais = data["pais"].ToString();


                bool response = SQLManager.agregarPais(pais);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/nuevoDeporte", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string deporte = data["deporte"].ToString();

                bool response = SQLManager.agregarDeporte(deporte);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/delDeporte", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string deporte = data["deporte"].ToString();

                bool response = SQLManager.eliminarDeporte(deporte);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });


            Post("/nuevoIdioma", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string idioma = data["idioma"].ToString();

                bool response = SQLManager.agregarIdioma(idioma);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/eliminarIdioma", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string idioma = data["idioma"].ToString();

                bool response = SQLManager.eliminarIdioma(idioma);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });


            Post("/delPais", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string pais = data["pais"].ToString();

                bool response = SQLManager.eliminarPais(pais);
                Console.WriteLine("Response:\n" + response);
                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });

            Post("/asignarPP", x =>
            {
                Console.WriteLine("post: /universidades");
                string json = this.Request.Body.AsString();
                JObject data = JObject.Parse(json);
                Console.WriteLine("Request:\n" + data);

                string idAtleta = data["idAtleta"].ToString();

                int semana = int.Parse(data["semana"].ToString());

                JArray ejecicios = data["ejercicios"] as JArray;

                bool response = SQLManager.asignarPP(idAtleta, ejecicios, semana);
                Console.WriteLine("Response:\n" + response);

                return response;

                /*
                var jsonBytes = Encoding.UTF8.GetBytes(response);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = s => s.Write(jsonBytes, 0, jsonBytes.Length)
                };*/
            });
        }
    }
}