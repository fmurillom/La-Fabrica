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
        public void getTemporadasTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Get("/temporadas");

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void getEstadosPartidosTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Get("/estadosPart");

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
        public void getEstadoLesionTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Get("/estadoLesion");

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void getDeportesLesionTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Get("/deportes");

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
        public void postInfoEntrenadorTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/infoEntrenador", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestEntrenador@algo.com\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postProvinciasTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/provincias", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"nombrePais\": \"Costa Rica\"}");
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
                with.Body("{\"correo\": \"TestEntrenador@algo.com\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postEquiposTemporadasTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/equiposTemporadas", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestEntrenador@algo.com\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postLoginTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/login", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestEntrenador@algo.com\", \"password\": \"456\"}");
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
        public void postAtletasxUniversidadesTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/atletasXuniversidad", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"universidad\": \"Instituto Tecnologico de Costa Rica\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postCrearLesionTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/crearLesion", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\", \"fechaInicio\": \"12/12/2010\", \"fechaFinal\": \"13/12/2010\", \"gravedad\": 0, \"descripcion\": \"Se cayo\"}");
            });

            var res = response.Result.Body.AsString();

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
        public void postEvaluarAtlEntrenamientoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/evaluarAtlEntr", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\", \"calificacion\": 10 ,\"tiempoDC\": 10.1 ,\"tiempoDL\": 4.6,\"salto\": 10,\"tiempoPH\": 10, \"pase\": 10, \"pruebaHR\": 10.1}");
            });

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postEvaluarAtlPartidoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/evaluarAtlPart", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\", \"temporada\": \"TestTemporada\" ,\"calificacion\": 10.1 ,\"estadoP\": 0, \"goles\": 10,\"asistencias\": 10, \"balonesR\": 10, \"pasesExit\": 10, \"pases\": 10, \"centros\": 10, \"centrosExit\": 10, \"tarjetasAmar\": 2, \"tarjetasRoj\": 1, \"penales\": 12, \"rematesSalv\": 11, \"golesRecib\": 15}");
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

            Assert.Equal("True", response.Result.Body.AsString());
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

        [Fact]
        public void postGetSemanasPlanesTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/semanasPlanes", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"correo\": \"TestInser@algo.com\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }

        [Fact]
        public void postReporteRangoTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/reporteRango", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"filtros\": [{\"filtro\":\"pais\",\"valor\":\"Costa Rica\"}]}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("application/json", response.Result.ContentType);
        }
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
        public void postDeleteUniversidadTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/delUniversidad", with =>
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
        public void postDeletePosicionTest()
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
        public void postAgregarPaisTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/nuevoPais", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"pais\": \"TestPais\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postAgregarDeporteTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/nuevoDeporte", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"deporte\": \"TestDeporte\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postDeleteDeporteTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/delDeporte", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"deporte\": \"TestDeporte\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postAgregarIdiomaTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/nuevoIdioma", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"idioma\": \"TestIdioma\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }

        [Fact]
        public void postEliminarIdiomaTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/eliminarIdioma", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"idioma\": \"TestIdioma\"}");
            });

            var res = response.Result.Body.AsString();

            Assert.Equal("True", response.Result.Body.AsString());
        }


        [Fact]
        public void postEliminarPaisTest()
        {
            var browser = new Browser(with => with.Module(new Server()));

            var response = browser.Post("/delPais", with =>
            {
                with.Header("Content-Type", "application/json");
                with.Body("{\"pais\": \"TestPais\"}");
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