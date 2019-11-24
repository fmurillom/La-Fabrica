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
                with.Body("{\"idEntrenador\": \"aaaa\"}");
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
                with.Body("{\"nombre\":'TestInser',\"apellido\":'TestInser',\"cedula\":123456789,\"provincia\":'Cartago',\"fechaNacimiento\":'02/01/2000',\"correo1\":'TestInser@algo.com',\"correo2\":\"null\",\"telefono\":22223333,\"foto\":'FotoYo',\"pais\":'Costa Rica',\"universidad\":'Instituto Tecnologico de Costa Rica',\"password\":'password2',\"deporte\":'Futbol',\"altura\":1.7,\"peso\":60,\"posicion\":1,\"posicionSecundaria\":2}");
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
                with.Body("{\"nombre\": \"TestScout\", \"apellido\": \"TestScout\",\"correo\": \"TestScout@algo.com\",\"password\": \"456\",\"pais\": \"Costa Rica\",\"universidad\": \"Instituto Tecnologico de Costa Rica\"}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

    }
}