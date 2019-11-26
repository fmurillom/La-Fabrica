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
                sqlInjection("exec proc_registrarTrabajador @nombre = '" + nombreC +"', @apellido = '"+ apellidoC + "', @correo = '" + email + "', @password = '"+ password +"', @rol = " + rol);
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Trabajadores where correo = '" + email + "'", connection);
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
                sqlInjection("update Atletas set nombreEquipo = '" + nombreEquip + "' where correo1 = '" + jugador + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Atletas where correo1 = '" + jugador + "' and nombreEquipo = '" + nombreEquip + "'", connection);
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



        public static bool crearEquip(string nombreEquip, string idEnrenador, string temporada)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("insert into Equipos(nombreEquipo,correoEntrenador) values('"+ nombreEquip + "', '" + idEnrenador + "')");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Equipos where nombreEquipo = '" + nombreEquip + "'" , connection);
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

                if (ok)
                {
                    sqlInjection("insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('" + nombreEquip + "', '" + temporada + "')");
                    connection.Open();
                    command = new SqlCommand("select count(*) as exist from EquiposTemporadas where nombreEquipo = '" + nombreEquip + "' and nombreTemporada = '" + temporada + "'", connection);
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
                                            float pruebaHR, string correo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            int ultimoIdEntr = 0;

            try
            {
                connection.Open();
                command = new SqlCommand("select max(idEntrenamiento) as id from Entrenamientos where correoAtleta = '" + correo + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        ultimoIdEntr = int.Parse(sqlReader["id"].ToString());
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();

                ultimoIdEntr = ultimoIdEntr + 1;

                sqlInjection("insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(" + ultimoIdEntr + ",'" + correo + "'," +  calificacion + "," + tiempoDC + "," + tiempoDL + "," + salto + "," + tiempoPH + "," + pase + "," + pruebaHR + ")");
                connection.Open();
                string com = "select count(*) as exist from Entrenamientos where idEntrenamiento = " + ultimoIdEntr + " and correoAtleta = '" + correo + "'";
                command = new SqlCommand(com, connection);
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
                                            int tarjetasRoj, int penales, int rematesSalv, int golesRecib, string correo, string temporada, float calificacion)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            int ultimoIdEntr = 0;
            try
            {

                connection.Open();
                command = new SqlCommand("select max(idPartido) as id from Partidos where correoAtleta = '" + correo + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        ultimoIdEntr = int.Parse(sqlReader["id"].ToString());
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();

                ultimoIdEntr = ultimoIdEntr + 1;

                sqlInjection("insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(" + ultimoIdEntr + ",'" + correo + "','" + temporada + "'," + calificacion + "," + estadoP + "," + goles + "," + asistencias +"," + balonesR + "," + pases + "," + pasesExit + "," + centros + "," + centrosExit + "," + tarjetasAmar + "," + tarjetasRoj + "," + penales + "," + rematesSalv + "," + golesRecib +")" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Partidos where idPartido = " + ultimoIdEntr + " and correoAtleta = '" + correo + "'" , connection);
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
            string query = "select nombreTemporada from EquiposTemporadas where nombreEquipo = '" + nombreEquipo + "'";
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

        public static JObject login(string emailCuenta, string password)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            JObject login = new JObject();
            bool ok = false;
            try
            {
                connection.Open();
                command = new SqlCommand("exec proc_logInAtleta @correo = '" + emailCuenta + "', @password = '" + password + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["success"].ToString().Equals("1"))
                        {
                            login["success"] = 1;
                            login["rol"] = "Atl";
                            ok = true;
                        }
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();

                if (!ok)
                {
                    connection.Open();
                    command = new SqlCommand("exec proc_logInEntrenador @correo = '" + emailCuenta + "', @password = '" + password + "'", connection);
                    sqlReader = command.ExecuteReader();
                    try
                    {
                        while (sqlReader.Read())
                        {
                            if (sqlReader["success"].ToString().Equals("1"))
                            {
                                login["success"] = 1;
                                login["rol"] = "Ent";
                                ok = true;
                            }
                        }
                    }
                    catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                    sqlReader.Close();
                    command.Dispose();
                    connection.Close();
                }

                if (!ok)
                {
                    connection.Open();
                    command = new SqlCommand("exec proc_logInTrabajador @correo = '" + emailCuenta + "', @password = '" + password + "'", connection);
                    sqlReader = command.ExecuteReader();
                    try
                    {
                        while (sqlReader.Read())
                        {
                            if (sqlReader["success"].ToString().Equals("1"))
                            {
                                login["success"] = 1;
                                login["rol"] = "Sct";
                                ok = true;
                            }
                        }
                    }
                    catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                    sqlReader.Close();
                    command.Dispose();
                    connection.Close();

                    if (ok)
                    {
                        connection.Open();
                        command = new SqlCommand("select count(*) as exist from Trabajadores where rol = 0 and correo = '" + emailCuenta + "'", connection);
                        sqlReader = command.ExecuteReader();
                        try
                        {
                            while (sqlReader.Read())
                            {
                                if (sqlReader["exist"].ToString().Equals("1"))
                                {
                                    login["success"] = 1;
                                    login["rol"] = "Admn";
                                    ok = true;
                                }
                            }
                        }
                        catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                        sqlReader.Close();
                        command.Dispose();
                        connection.Close();
                    }

                }
                if (!ok)
                {
                    login["success"] = 0;
                }
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            return login;
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
            string query = "select correo1 from Atletas where nombreEquipo = '" + nombrEquipo + "'";
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
                        integrantes.Add(sqlReader["correo1"]);
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
                sqlInjection("update Atletas set activo = 0 where correo1 = '" + emailCuenta + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Atletas where correo1 = '" + emailCuenta + "' and activo = 0", connection);
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

        public static bool activarCuenta(string emailCuenta)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("update Atletas set activo = 1 where correo1 = '" + emailCuenta + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Atletas where correo1 = '" + emailCuenta + "' and activo = 1", connection);
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
            string query = "select * from atletas where correo1 = '" + emailCuenta + "'";
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
            string query = "select * from atletas where nombre = '" + nombreJ + "' and apellido = '" + apellidoJ + "'";
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
            string query = "select * from atletas where cedula = '" + idJugador + "'";
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

        public static bool asignarPP(string idAtleta, JArray ejecicios, int semana)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
  
            bool ok = false;


            sqlInjection("insert into Planes(semana,correoAtleta) values(" + semana + ",'" + idAtleta +"')");


            for (int i = 0; i < ejecicios.Count; i++)
            {
                try
                {
                    sqlInjection("insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(" + semana + ",'" + idAtleta + "'," + int.Parse(ejecicios[i]["dia"].ToString()) + "," + int.Parse(ejecicios[i]["idEjercicio"].ToString()) + "," + int.Parse(ejecicios[i]["cantidad"].ToString()) + ")");
                    ok = true;
                }
                catch (SqlException ex)
                {
                    SqlError err = ex.Errors[0];
                    Console.WriteLine("Codigo de error: " + err.Number);
                    Console.WriteLine("Descripcion: " + err.Message);
                }
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
                sqlInjection("insert into Temporadas(nombreTemporada) values ('" + nombreTemporada + "')");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Temporadas where nombreTemporada = '" + nombreTemporada + "'", connection);
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
                sqlInjection("insert into Ejercicios(nombreEjercicio) values('" + nombreEjercicio + "')" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Ejercicios where  nombreEjercicio= '" + nombreEjercicio + "'", connection);
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
                sqlInjection("insert into Universidades(nombreUniversidad,nombrePais) values('" + nombreUniversidad + "', '" + nombrePais + "')" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Universidades where nombreUniversidad = '" + nombreUniversidad + "'", connection);
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

        public static bool agregarPosicion(string nombrePosicion, string deporte)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            int ultimoidPosicion = 0;
            try
            {
                connection.Open();
                command = new SqlCommand("select max(idPosicion) as id from Posiciones", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        ultimoidPosicion = int.Parse(sqlReader["id"].ToString());
                    }
                }
                catch (Exception ex2) { Console.WriteLine(ex2.Message); }
                sqlReader.Close();
                command.Dispose();
                connection.Close();

                ultimoidPosicion = ultimoidPosicion + 1;

                sqlInjection("insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('"+ deporte + "'," + ultimoidPosicion + ", '"+ nombrePosicion + "')" );
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Posiciones where nombrePosicion = '" + nombrePosicion + "' and idPosicion = " + ultimoidPosicion, connection);
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
            string query = "select * from PlanesEjercicios as P join Ejercicios as E on P.idEjercicio = E.idEjercicio where correoAtleta = '" + emailJugador + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;

            JObject plan = new JObject();
            JArray ejercicios = new JArray();
            JObject ejercicio;

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
                        ejercicio["semana"] = int.Parse(sqlReader["semana"].ToString());
                        ejercicio["nombreEjercicio"] = sqlReader["nombreEjercicio"].ToString();
                        ejercicio["cantidad"] = int.Parse(sqlReader["cantidad"].ToString());
                        ejercicio["dia"] = int.Parse(sqlReader["dia"].ToString());
                        ejercicios.Add(ejercicio);
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
            plan["planes"] = ejercicios;
            return plan;
        }


        public static JObject getProvincias(string nombrePais)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Provincias where nombrePais = '" + nombrePais + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject provincias = new JObject();
            JArray prov = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        prov.Add(sqlReader["nombreProvincia"]);
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
            provincias["provincias"] = prov;
            return provincias;
        }

        public static JObject getDeportes()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Deportes";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject deportes = new JObject();
            JArray depor = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        depor.Add(sqlReader["nombreDeporte"]);
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
            deportes["deportes"] = depor;
            return deportes;
        }
    }




}