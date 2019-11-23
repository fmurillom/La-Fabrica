using System;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace NancyRest
{
    public class SQLManager
    {
        private static string connectionString = "Data Source=Chislap;Initial Catalog=LaFabricaDB;Integrated Security=True";

        public static void sqlInjection(string injection)
        {
            SqlConnection connection = new SqlConnection("Data Source=Chislap;Initial Catalog=LaFabricaDB;Integrated Security=True");//
            SqlCommand command;
            SqlDataReader sqlReader;
            try
            {
                connection.Open();
                command = new SqlCommand("SET DATEFORMAT dmy;", connection);
                sqlReader = command.ExecuteReader();
                sqlReader.Close();

                command = new SqlCommand(injection, connection);
                sqlReader = command.ExecuteReader();

                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
        }


        public static bool insertAtleta(string nombreC, int cedula, string apellidoC, string provincia, string email1, string email2, int telefonoM, string foto, string pais,
                                        string universidad, string password, string deporte, float altura, float peso, string fechaNacimiento, int posicion, int posicionSecundaria)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            Console.WriteLine("EXEC proc_registrarAtleta @nombre = '" + nombreC + "',@apellido = '" + apellidoC + "', @cedula = " + cedula + ", @provincia = '" + provincia + "', @fechaNacimiento = '" + fechaNacimiento + "', @correo1 = '" + email1 + "', @correo2 = '" + email2 + "', @telefono =" + telefonoM + ", @foto = '" + foto + "', @pais = '" + pais + "', @universidad = '" + universidad + "', @password = '" + password + "', @deporte = '" + deporte + "', @altura =" + altura + ", @peso =" + peso + ", @posicion =" + posicion + ", @posicionSecundaria =" + posicionSecundaria);
            try
            {
                sqlInjection("EXEC proc_registrarAtleta @nombre = '" + nombreC + "',@apellido = '" + apellidoC + "', @cedula = " + cedula + ", @provincia = '" + provincia + "', @fechaNacimiento = '" + fechaNacimiento + "', @correo1 = '"+ email1 + "', @correo2 = '" + email2 + "', @telefono =" + telefonoM + ", @foto = '" + foto + "', @pais = '" + pais + "', @universidad = '" + universidad + "', @password = '" + password + "', @deporte = '" + deporte + "', @altura =" + altura + ", @peso =" + peso + ", @posicion =" + posicion + ", @posicionSecundaria =" + posicionSecundaria);
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Atletas where correo1 = '" + email1 + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }

        public static bool insertEntr(string nombreC, string apellidoC, string email, string pais, string universidad, string password)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("EXEC proc_registrarEntrenador @nombre = '" + nombreC + "', @apellido = '" + apellidoC + "', @correo = '" + email + "', @password = '"+ password + "', @pais = '" + pais + "', @universidad = '" + universidad + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Entrenadores where correo = '" + email + "'" , connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }


        public static bool insertTrab(string nombreC, string apellidoC, string email, string password, int rol)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }


        public static bool insertEquip(string nombreEquip, string jugador)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }



        public static bool crearEquip(string nombreEquip)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '" , connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }


        public static bool evaluarAtlEntr(float calificacion, float tiempoDC, float tiempoDL, float salto, float tiempoPH, float pase,
                                            float pruebaHR, float altura)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }


        public static bool evaluarAtlPart(string estadoP, int goles, int asistencias, int balonesR, int pasesExit, int pases, int centros, int centrosExit, int tarjetasAmar,
                                            int tarjetasRoj, int penales, int rematesSalv, int golesRecib)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '" , connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }

        public static JObject getPaises()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Paises";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject paises = new JObject();
            JArray pais = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        pais.Add(sqlReader["nombre"].ToString());
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            paises["pais"] = pais;
            return paises;
        }

        public static JObject getPocisiones(string nombreDeporte)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Posiciones where nombreDeporte = '" + nombreDeporte + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject posiciones = new JObject();
            JArray posicion = new JArray();
            JObject pos;

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        pos = new JObject();
                        pos["nombrePosicion"] = sqlReader["nombrePosicion"].ToString();
                        pos["idPosicion"] = int.Parse(sqlReader["idPosicion"].ToString());
                        posicion.Add(pos);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            posiciones["pos"] = posicion;
            return posiciones;
        }


        public static JObject getEquipos(string idEntrenador)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Equipos where correoEntrenador = '" + idEntrenador + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject equipos = new JObject();
            JArray equip = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        equip.Add(sqlReader["nombreEquipo"]);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            equipos["equipos"] = equip;
            return equipos;
        }

        public static JObject getTemporadasEquipo(string nombreEquipo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = ";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject temporada = new JObject();
            JArray temporadas = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        temporadas.Add(sqlReader["nombreTemporada"].ToString());
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            temporada["nombreTemporada"] = temporadas;
            return temporada;
        }

        public static JObject getUniversidades(string nombrepais)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Universidades where nombrePais = '" + nombrepais + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject universidades = new JObject();
            JArray unis = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        unis.Add(sqlReader["nombreUniversidad"]);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            universidades["uni"] = unis;
            return universidades;
        }



        public static JObject getMiembrosEquipo(string nombrEquipo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = ";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject equipo = new JObject();
            JArray integrantes  = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        integrantes.Add(sqlReader["correoAtleta"]);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            equipo["integrantes"] = integrantes;
            return equipo;
        }

        public static JObject getEjercicios()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Ejercicios";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject ejercicios = new JObject();
            JObject ejercicio;
            JArray ejer = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        ejercicio = new JObject();
                        ejercicio["idEjercicio"] = sqlReader["idEjercicio"].ToString();
                        ejercicio["nombreEjercicio"] = sqlReader["nombreEjercicio"].ToString();
                        ejer.Add(ejercicio);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            ejercicios["ejercicios"] = ejer;
            return ejercicios;
        }



        public static bool desactivarCuenta(string emailCuenta)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '" , connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }

        public static JObject getInfoAtleta(string emailCuenta)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = ";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject atleta = new JObject();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        atleta["nombre"] = sqlReader["nombre"].ToString();
                        atleta["cedula"] = int.Parse(sqlReader["cedula"].ToString());
                        atleta["apellido"] = sqlReader["apellido"].ToString();
                        atleta["provincia"] = sqlReader["provincia"].ToString();
                        atleta["fechaNacimiento"] = sqlReader["fechaNacimiento"].ToString();
                        atleta["activo"] = sqlReader["activo"].ToString();
                        atleta["correo1"] = sqlReader["correo1"].ToString();
                        atleta["correo2"] = sqlReader["correo2"].ToString();
                        atleta["telefono"] = sqlReader["telefono"].ToString();
                        atleta["foto"] = sqlReader["foto"].ToString();
                        atleta["fechaInscripcion"] = sqlReader["fechaInscripcion"].ToString();
                        atleta["pais"] = sqlReader["pais"].ToString();
                        atleta["universidad"] = sqlReader["universidad"].ToString();
                        atleta["password"] = sqlReader["password"].ToString();
                        atleta["deporte"] = sqlReader["deporte"].ToString();
                        atleta["altura"] = float.Parse(sqlReader["altura"].ToString());
                        atleta["peso"] = float.Parse(sqlReader["peso"].ToString());
                        atleta["posicion"] = sqlReader["posicion"].ToString();
                        atleta["posicionSecundaria"] = sqlReader["posicionSecundaria"].ToString();
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return atleta;
        }


        public static JObject getJugadorNombre(string nombreJ, string apellidoJ)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = ";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject atleta = new JObject();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        atleta["nombre"] = sqlReader["nombre"].ToString();
                        atleta["cedula"] = int.Parse(sqlReader["cedula"].ToString());
                        atleta["apellido"] = sqlReader["apellido"].ToString();
                        atleta["provincia"] = sqlReader["provincia"].ToString();
                        atleta["fechaNacimiento"] = sqlReader["fechaNacimiento"].ToString();
                        atleta["activo"] = sqlReader["activo"].ToString();
                        atleta["correo1"] = sqlReader["correo1"].ToString();
                        atleta["correo2"] = sqlReader["correo2"].ToString();
                        atleta["telefono"] = sqlReader["telefono"].ToString();
                        atleta["foto"] = sqlReader["foto"].ToString();
                        atleta["fechaInscripcion"] = sqlReader["fechaInscripcion"].ToString();
                        atleta["pais"] = sqlReader["pais"].ToString();
                        atleta["universidad"] = sqlReader["universidad"].ToString();
                        atleta["password"] = sqlReader["password"].ToString();
                        atleta["deporte"] = sqlReader["deporte"].ToString();
                        atleta["altura"] = float.Parse(sqlReader["altura"].ToString());
                        atleta["peso"] = float.Parse(sqlReader["peso"].ToString());
                        atleta["posicion"] = sqlReader["posicion"].ToString();
                        atleta["posicionSecundaria"] = sqlReader["posicionSecundaria"].ToString();
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return atleta;
        }

        public static JObject getJugadorID(int idJugador)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = ";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject atleta = new JObject();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        atleta["nombre"] = sqlReader["nombre"].ToString();
                        atleta["cedula"] = int.Parse(sqlReader["cedula"].ToString());
                        atleta["apellido"] = sqlReader["apellido"].ToString();
                        atleta["provincia"] = sqlReader["provincia"].ToString();
                        atleta["fechaNacimiento"] = sqlReader["fechaNacimiento"].ToString();
                        atleta["activo"] = sqlReader["activo"].ToString();
                        atleta["correo1"] = sqlReader["correo1"].ToString();
                        atleta["correo2"] = sqlReader["correo2"].ToString();
                        atleta["telefono"] = sqlReader["telefono"].ToString();
                        atleta["foto"] = sqlReader["foto"].ToString();
                        atleta["fechaInscripcion"] = sqlReader["fechaInscripcion"].ToString();
                        atleta["pais"] = sqlReader["pais"].ToString();
                        atleta["universidad"] = sqlReader["universidad"].ToString();
                        atleta["password"] = sqlReader["password"].ToString();
                        atleta["deporte"] = sqlReader["deporte"].ToString();
                        atleta["altura"] = float.Parse(sqlReader["altura"].ToString());
                        atleta["peso"] = float.Parse(sqlReader["peso"].ToString());
                        atleta["posicion"] = sqlReader["posicion"].ToString();
                        atleta["posicionSecundaria"] = sqlReader["posicionSecundaria"].ToString();
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return atleta;
        }

        public static JObject getReporteAtleta(JArray filtros)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = ";
            SqlCommand command;
            SqlDataReader sqlReader;

            JObject atleta;
            JObject atletas = new JObject();
            JArray atletasAux = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        atleta = new JObject();
                        atleta["nombre"] = sqlReader["nombre"].ToString();
                        atleta["cedula"] = int.Parse(sqlReader["cedula"].ToString());
                        atleta["apellido"] = sqlReader["apellido"].ToString();
                        atleta["provincia"] = sqlReader["provincia"].ToString();
                        atleta["fechaNacimiento"] = sqlReader["fechaNacimiento"].ToString();
                        atleta["activo"] = sqlReader["activo"].ToString();
                        atleta["correo1"] = sqlReader["correo1"].ToString();
                        atleta["correo2"] = sqlReader["correo2"].ToString();
                        atleta["telefono"] = sqlReader["telefono"].ToString();
                        atleta["foto"] = sqlReader["foto"].ToString();
                        atleta["fechaInscripcion"] = sqlReader["fechaInscripcion"].ToString();
                        atleta["pais"] = sqlReader["pais"].ToString();
                        atleta["universidad"] = sqlReader["universidad"].ToString();
                        atleta["password"] = sqlReader["password"].ToString();
                        atleta["deporte"] = sqlReader["deporte"].ToString();
                        atleta["altura"] = float.Parse(sqlReader["altura"].ToString());
                        atleta["peso"] = float.Parse(sqlReader["peso"].ToString());
                        atleta["posicion"] = sqlReader["posicion"].ToString();
                        atleta["posicionSecundaria"] = sqlReader["posicionSecundaria"].ToString();
                        atletasAux.Add(atleta);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            atletas["atletas"] = atletasAux;
            return atletas;
        }

        public static JObject getAdmins()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select nombre, apellido, correo from Trabajadores where rol = 0";
;
            SqlCommand command;
            SqlDataReader sqlReader;

            JObject admins = new JObject();
            JObject admin;
            JArray adminsArr = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        admin = new JObject();
                        admin["nombre"] = sqlReader["nombre"].ToString();
                        admin["apellido"] = sqlReader["apellido"].ToString();
                        admin["correo"] = sqlReader["correo"].ToString();
                        adminsArr.Add(admin);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            admins["admins"] = adminsArr;
            return admins;
        }



        public static JObject getVueloConEscalas(string idVuelo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = " + idVuelo;
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject vuelo = new JObject();
            JObject escala;
            JArray escalas = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        vuelo["id"] = sqlReader["id"].ToString();
                        vuelo["iataSal"] = sqlReader["iataSal"].ToString();
                        vuelo["sal"] = sqlReader["sal"].ToString();
                        vuelo["iataDes"] = sqlReader["iataDes"].ToString();
                        vuelo["des"] = sqlReader["des"].ToString();
                        vuelo["fecha"] = sqlReader["fecha"].ToString();
                    }
                    sqlReader.Close();
                    command.Dispose();


                    command = new SqlCommand("select Aeropuertos.codigoIATA, Aeropuertos.nombre from Vuelos join Escalas on Vuelos.id = Escalas.id_vuelo join Aeropuertos on Aeropuertos.codigoIATA = Escalas.codigoIATA where id = " + idVuelo, connection);
                    sqlReader = command.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        escala = new JObject();
                        escala["iata"] = sqlReader["codigoIATA"].ToString();
                        escala["nombre"] = sqlReader["nombre"].ToString();
                        escalas.Add(escala);
                    }
                    sqlReader.Close();
                    command.Dispose();

                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            vuelo["intermedio"] = escalas;
            return vuelo;
        }

        public static bool asignarPP(string idAtleta, JArray ejecicios, int semana, int dia)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '" , connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }


        public static bool crearTemporada(string nombreTemporada)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }


        public static bool agregarEjercicio(string nombreEjercicio)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }

        public static bool agregarUniversidad(string nombreUniversidad, string nombrePais)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }

        public static bool agregarPosicion(string nombrePosicion)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("exec crearReservacion @userName = '" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Reservaciones where usuario = '", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("1"))
                        {
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return ok;
        }

        public static JObject getPlanesAtleta(string emailJugador)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            //Query para obtener que semanas tiene planes
            string query = "select id,aeropuertoIni as iataSal,nombre as sal,aeropuertoFin as iataDes,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, fecha from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA where abierto = 1 and id = ";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            List<int> semanas = new List<int>();
            List<JObject> ejercicios = new List<JObject>();
            JObject ejerCant;
            JObject ejerSnd = new JObject();
            JArray arrayEjercicios = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        semanas.Add(int.Parse(sqlReader["semana"].ToString()));
                    }
                    sqlReader.Close();
                    command.Dispose();

                   for(int i = 0; i < semanas.Count; i++)
                    {
                        //Query Para obtener que ejercicios hacer en que semana
                        command = new SqlCommand("select Aeropuertos.codigoIATA, Aeropuertos.nombre from Vuelos join Escalas on Vuelos.id = Escalas.id_vuelo join Aeropuertos on Aeropuertos.codigoIATA = Escalas.codigoIATA where id = ", connection);
                        sqlReader = command.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            ejerCant = new JObject();
                            ejerCant["ejercicio"] = int.Parse(sqlReader["idEjercicio"].ToString());
                            ejerCant["cantidad"] = int.Parse(sqlReader["cantidad"].ToString());
                            ejercicios.Add(ejerCant);
                        }
                        sqlReader.Close();
                        command.Dispose();
                    }

                    for (int i = 0; i < ejercicios.Count; i++)
                    {
                        //Query Para obtener nombre de ejercicio atravez del id de ejercicio
                        command = new SqlCommand("select Aeropuertos.codigoIATA, Aeropuertos.nombre from Vuelos join Escalas on Vuelos.id = Escalas.id_vuelo join Aeropuertos on Aeropuertos.codigoIATA = Escalas.codigoIATA where id = ", connection);
                        sqlReader = command.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            ejerCant = ejercicios.ElementAt(i);
                            ejerCant["ejercicio"] = int.Parse(sqlReader["nombreEjercicio"].ToString());
                            ejercicios.Add(ejerCant);
                        }
                        sqlReader.Close();
                        command.Dispose();
                    }

                    ejercicios.ForEach(delegate(JObject item)
                    {
                        arrayEjercicios.Add(item);
                    });


                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            ejerSnd["ejercicios"] = arrayEjercicios;
            return ejerSnd;
        }
    }


}