using Nancy;
using Nancy.Testing;
using NancyRest;
using System;
using Xunit;

namespace UnitTestRestServer
{
    public class UnitTest1
    {
        [Fact]
        public void getPaisesTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Get("/paises");

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void getEjerciciosTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Get("/ejercicios");

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void getAdminsTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Get("/admins");

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postPosicionesTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/posiciones", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreDeporte\": \"Futbol\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postEquiposTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/equipos", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"aaaa\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postUniversidadesTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/universidades", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombrePais\": \"Costa Rica\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postCrearAtletaTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/crearAtl", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombre\":'TestInser',\"carne\": 2015000000, \"apellido\":'TestInser',\"cedula\":123456789,\"provincia\":'Cartago',\"fechaNacimiento\":'02/01/2000',\"correo1\":'TestInser@algo.com',\"correo2\":\"null\",\"telefono\":22223333,\"foto\":'FotoYo',\"pais\":'Costa Rica',\"universidad\":'Instituto Tecnologico de Costa Rica',\"password\":'password2',\"deporte\":'Futbol',\"altura\":1.7,\"peso\":60,\"posicion\":1,\"posicionSecundaria\":2}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postCrearEntrenadorTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/crearEnt", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombre\": \"TestEntrenador\", \"apellido\": \"TestEntrenador\",\"correo\": \"TestEntrenador@algo.com\",\"password\": \"456\",\"pais\": \"Costa Rica\",\"universidad\": \"Instituto Tecnologico de Costa Rica\"}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postCrearScoutTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/crearScout", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombre\": \"TestScout\", \"apellido\": \"TestScout\",\"correo\": \"TestScout@algo.com\",\"password\": \"456\"}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postCrearAdminTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/crearAdmin", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombre\": \"TestAdmin\", \"apellido\": \"TestAdmin\",\"correo\": \"TestAdmin@algo.com\",\"password\": \"456\"}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postCrearTemporadaTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/crearTemporada", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreTemporada\": \"TestTemporada\"}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }


        [Fact]
        public void postCrearEquipoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/crearEquip", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreEquipo\": \"TestEquipo\", \"correoEntrenador\": \"TestEntrenador@algo.com\", \"temporada\": \"TestTemporada\"}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postInsertarEnEquipoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/insertEquip", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreEquipo\": \"TestEquipo\", \"correo\": \"TestInser@algo.com\"}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postJugadoresenEquipoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/jugadoresEquip", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreEquipo\": \"TestEquipo\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postBuscarJugadorNombreTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/buscarJugadorNombre", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombre\": \"TestInser\", \"apellido\": \"TestInser\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postBuscarJugadorIdTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/buscarJugadorID", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"cedula\": 123456789}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postDesactivarCuentaTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/desactivarCuenta", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postActivarCuentaTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/activarCuenta", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True",response.Result.Body.AsString());
        }

        [Fact]
        public void postGetAtletaCorreoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/atletacorreo", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postGetPlanesCorreoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/planesEmailSem", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\", \"semana\": 1}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        //TODO Unit Test Reporte Rango

        [Fact]
        public void postGetTemporadasEquipoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/temporadaXequipo", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreEquipo\": \"TestEquipo\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postAgregarEjercicioTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/nuevoEjercicio", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreEjercicio\": \"TestEjercicio\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postAgregarUniversidadTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/nuevaUniversidad", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombreUniversidad\": \"TestUniversidad\", \"nombrePais\": \"Costa Rica\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postAgregarPosicionTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/nuevaPosicion", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombrePosicion\": \"TestPosicion\", \"deporte\": \"Futbol\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postAsignarPPTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/asignarPP", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\", \"semana\": 1, \"dia\": 1, \"idEjercicio\": 0, \"cantidad\": 10}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }


    }
}