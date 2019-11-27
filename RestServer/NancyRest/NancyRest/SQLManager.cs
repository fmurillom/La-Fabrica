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
                                        string universidad, string password, string deporte, float altura, float peso, string fechaNacimiento, int posicion, int posicionSecundaria, int carne)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            Console.WriteLine("EXEC proc_registrarAtleta @nombre = '" + nombreC + "',@apellido = '" + apellidoC + "', @carne = " + carne + ", @cedula = " + cedula + ", @provincia = '" + provincia + "', @fechaNacimiento = '" + fechaNacimiento + "', @correo1 = '" + email1 + "', @correo2 = '" + email2 + "', @telefono =" + telefonoM + ", @foto = '" + foto + "', @pais = '" + pais + "', @universidad = '" + universidad + "', @password = '" + password + "', @deporte = '" + deporte + "', @altura =" + altura + ", @peso =" + peso + ", @posicion =" + posicion + ", @posicionSecundaria =" + posicionSecundaria);
            try
            {
                sqlInjection("EXEC proc_registrarAtleta @nombre = '" + nombreC + "',@apellido = '" + apellidoC + "', @carne = " + carne + ", @cedula = " + cedula + ", @provincia = '" + provincia + "', @fechaNacimiento = '" + fechaNacimiento + "', @correo1 = '"+ email1 + "', @correo2 = '" + email2 + "', @telefono =" + telefonoM + ", @foto = '" + foto + "', @pais = '" + pais + "', @universidad = '" + universidad + "', @password = '" + password + "', @deporte = '" + deporte + "', @altura =" + altura + ", @peso =" + peso + ", @posicion =" + posicion + ", @posicionSecundaria =" + posicionSecundaria);
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

        public static bool insertLesion(string correo, string fechaInicio, string fechaFinal, int gravedad, string descripcion)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("insert into Lesiones(correoAtleta,fechaInicio,fechaFinal,descripcion,idTipoLesion) values('" + correo + "', '" + fechaInicio + "','" + fechaFinal + "','" + descripcion +"'," + gravedad + ")");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Lesiones where correoAtleta = '" + correo + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (int.Parse(sqlReader["exist"].ToString()) >= 1)
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

        public static JObject getAtletasUniversidad(string universidad)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;

            JObject atleta = new JObject();
            JArray atletas = new JArray();
            JObject atl;

            try
            {
                connection.Open();
                command = new SqlCommand("select * from Atletas where universidad = '" + universidad + "' and nombreEquipo is null", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        atl = new JObject();
                        atl["nombre"] = sqlReader["nombre"].ToString();
                        atl["apellido"] = sqlReader["apellido"].ToString();
                        atl["correo"] = sqlReader["correo1"].ToString();
                        atletas.Add(atl);
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
            atleta["atletas"] = atletas;
            return atleta;
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


        public static bool evaluarAtlPart(int estadoP, int goles, int asistencias, int balonesR, int pasesExit, int pases, int centros, int centrosExit, int tarjetasAmar,
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

        public static JObject getTemporadas()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Temporadas";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject temporadas = new JObject();
            JArray temp = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        temp.Add(sqlReader["nombreTemporada"].ToString());
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
            temporadas["temporadas"] = temp;
            return temporadas;
        }

        public static JObject getEstadosPart()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from EstadosDePartido";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject estados = new JObject();
            JArray estad = new JArray();
            JObject estadAux;

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        estadAux = new JObject();
                        estadAux["idEstado"] = int.Parse(sqlReader["idEstado"].ToString());
                        estadAux["estado"] = sqlReader["estado"].ToString();
                        estad.Add(estadAux);

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
            estados["estados"] = estad;
            return estados;
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

        public static JObject getInfoEntrenador(string correo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Entrenadores where correo = '" + correo + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject entrenador = new JObject();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        entrenador["correo"] = sqlReader["correo"].ToString();
                        entrenador["nombre"] = sqlReader["nombre"].ToString();
                        entrenador["apellido"] = sqlReader["apellido"].ToString();
                        entrenador["fechaInscripcion"] = sqlReader["fechaInscripcion"].ToString();
                        entrenador["pais"] = sqlReader["pais"].ToString();
                        entrenador["universidad"] = sqlReader["universidad"].ToString();
                        if(sqlReader["activo"].ToString() == "True")
                        {
                            entrenador["activo"] = 1;
                        }
                        else
                        {
                            entrenador["activo"] = 0;

                        }
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
            return entrenador;
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
                        equip.Add(sqlReader["nombreEquipo"].ToString());
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

        public static JObject getEquiposTemporadas(string idEntrenador)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from Equipos where correoEntrenador = '" + idEntrenador + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject equipos = new JObject();
            JObject equipo;
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
                        equipo = new JObject();
                        equipo["temporadas"] = getTemporadasEquipo(sqlReader["nombreEquipo"].ToString())["nombreTemporada"];
                        equipo["nombre"] = sqlReader["nombreEquipo"].ToString();
                        equip.Add(equipo);

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
                        if (sqlReader["Success"].ToString().Equals("1"))
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
                            if (sqlReader["Success"].ToString().Equals("1"))
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
                            if (sqlReader["Success"].ToString().Equals("1"))
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
            string query = "select nombre, apellido, correo1 from Atletas where nombreEquipo = '" + nombrEquipo + "'";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;
            JObject equipo = new JObject();
            JObject inte;
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
                        inte = new JObject();
                        inte["nombre"] = sqlReader["nombre"].ToString();
                        inte["apellido"] = sqlReader["apellido"].ToString();
                        inte["correo"] = sqlReader["correo1"].ToString();
                        integrantes.Add(inte);
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
                sqlInjection("update Trabajadores set activo = 0 where correo = '" + emailCuenta + "'");
                sqlInjection("update Entrenadores set activo = 0 where correo = '" + emailCuenta + "'");

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
                sqlInjection("update Trabajadores set activo = 1 where correo = '" + emailCuenta + "'");
                sqlInjection("update Entrenadores set activo = 1 where correo = '" + emailCuenta + "'");

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
            string query = "select nombre,apellido,carne,cedula,provincia,fechaNacimiento,activo,correo1,correo2,telefono,foto,fechaInscripcion,pais,universidad,password,deporte,altura,peso,(select P.nombrePosicion from Posiciones as P where P.idPosicion = A.posicion) as posicion,(select P.nombrePosicion from Posiciones as P where P.idPosicion = A.posicionSecundaria) as posicionSecundaria ,notaXSport,nombreEquipo from Atletas as A join Posiciones as P on A.posicion = P.idPosicion where correo1 = '" + emailCuenta + "'";
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
                        atleta["carne"] = int.Parse(sqlReader["carne"].ToString());
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
            string query = "select * from Atletas as A join Posiciones as P on A.posicionSecundaria = P.idPosicion";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;

            JObject atleta;
            JObject atletas = new JObject();
            JArray atletasAux = new JArray();

            JObject atletaEscogido;

            JArray atletasFiltrados = new JArray();

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
                        atleta["correo1"] = sqlReader["correo1"].ToString();
                        atleta["pais"] = sqlReader["pais"].ToString();
                        atleta["universidad"] = sqlReader["universidad"].ToString();
                        atleta["altura"] = float.Parse(sqlReader["altura"].ToString());
                        atleta["peso"] = float.Parse(sqlReader["peso"].ToString());
                        atleta["posicion"] = int.Parse(sqlReader["posicion"].ToString());
                        atleta["posicionSecundaria"] = sqlReader["nombrePosicion"].ToString();
                        atleta["fechaNacimiento"] = sqlReader["fechaNacimiento"].ToString();
                        atleta["carne"] = int.Parse(sqlReader["carne"].ToString());
                        atleta["pais"] = sqlReader["pais"].ToString();
                        atletasAux.Add(atleta);
                    }
                    sqlReader.Close();
                    command.Dispose();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                connection.Close();

                if(filtros.Count > 1)
                {
                    for(int i = 0; i < atletasAux.Count; i++)
                    {
                        if(atletasAux[i]["pais"].ToString() == filtros[0]["valor"].ToString() && atletasAux[i]["universidad"].ToString() == filtros[1]["valor"].ToString() && float.Parse(atletasAux[i]["peso"].ToString()) > float.Parse(filtros[2]["valor"].ToString()) && float.Parse(atletasAux[i]["altura"].ToString()) > float.Parse(filtros[3]["valor"].ToString()) && atletasAux[i]["posicionSecundaria"].ToString() == filtros[4]["valor"].ToString())
                        {
                            atletasFiltrados.Add(atletasAux[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < atletasAux.Count; i++)
                    {
                        if (atletasAux[i]["pais"].ToString() == filtros[0]["valor"].ToString())
                        {
                            atletasFiltrados.Add(atletasAux[i]);
                        }
                    }
                }

                for(int i = 0; i < atletasFiltrados.Count(); i++)
                {
                    connection.Open();
                    query = "select dbo.func_AVGtiempoPruebaDistanciaCorta('" + atletasFiltrados[i]["correo1"].ToString() + "') as promedioDC ,dbo.func_AVGtiempoPruebaDistanciaLarga('" + atletasFiltrados[i]["correo1"].ToString() + "') as promedioDL,dbo.func_bestoTiempoPruebaDistanciaCorta('" + atletasFiltrados[i]["correo1"].ToString() + "') as mejorDC,dbo.func_bestoTiempoPruebaDistanciaLarga('" + atletasFiltrados[i]["correo1"].ToString() + "') as mejorDL,dbo.func_bestoSalto('" + atletasFiltrados[i]["correo1"].ToString() + "') as mejorSalto,dbo.func_AVGcalificacionPartidos('" + atletasFiltrados[i]["correo1"].ToString() + "') as promedioCP,dbo.func_AVGtiempoPruebaHabilidad('" + atletasFiltrados[i]["correo1"].ToString() + "') as promedioPH,dbo.func_getPorcentajePasesExitosos('" + atletasFiltrados[i]["correo1"].ToString() + "') as pPasesC,dbo.func_getPorcentajeCentrosExitosos('" + atletasFiltrados[i]["correo1"].ToString() + "') as pCentros,dbo.func_AVGcalificacionEntrenamientos('" + atletasFiltrados[i]["correo1"].ToString() + "') as promCalificacionEntrenamientos,dbo.func_getCantJuegosGanados('" + atletasFiltrados[i]["correo1"].ToString() + "') as cantG";
                    command = new SqlCommand(query, connection);
                    sqlReader = command.ExecuteReader();
                    float notaEntrenamientos;
                    float notaPartidos;
                    float tiempoDistanciaCorta;
                    float tiempoDistanciaLarga;
                    float saltos;
                    float centros;
                    float pases;
                    float victorias;
                    float nEporcent = 1;
                    float nPporcent = 1;
                    float tcPorcent = 1;
                    float tlPorcent = 1;
                    float saltosPorcent = 1;
                    float cnetrosPorcent = 1;
                    float pasesPorcent = 1;
                    float vicPorcent = 1;


                    try
                    {
                        while (sqlReader.Read())
                        {
                            atleta = atletasFiltrados[i] as JObject;
                            atleta["promedioDC"] = float.Parse(sqlReader["promedioDC"].ToString());
                            atleta["promedioDL"] = float.Parse(sqlReader["promedioDL"].ToString());
                            atleta["mejorDC"] = float.Parse(sqlReader["mejorDC"].ToString());
                            tiempoDistanciaCorta = float.Parse(sqlReader["mejorDC"].ToString());
                            atleta["mejorDL"] = float.Parse(sqlReader["mejorDL"].ToString());
                            tiempoDistanciaLarga = float.Parse(sqlReader["mejorDL"].ToString());
                            atleta["mejorSalto"] = float.Parse(sqlReader["mejorSalto"].ToString());
                            saltos = float.Parse(sqlReader["mejorSalto"].ToString());
                            atleta["promedioCP"] = float.Parse(sqlReader["promedioCP"].ToString());
                            notaPartidos = float.Parse(sqlReader["promedioCP"].ToString());
                            atleta["promedioPH"] = float.Parse(sqlReader["promedioPH"].ToString());
                            atleta["pPasesC"] = float.Parse(sqlReader["pPasesC"].ToString());
                            pases = float.Parse(sqlReader["pPasesC"].ToString());
                            atleta["pCentros"] = float.Parse(sqlReader["pCentros"].ToString());
                            centros = float.Parse(sqlReader["pCentros"].ToString());
                            atleta["promCalificacionEntrenamientos"] = float.Parse(sqlReader["promCalificacionEntrenamientos"].ToString());
                            notaEntrenamientos = float.Parse(sqlReader["promCalificacionEntrenamientos"].ToString());
                            atleta["cantG"] = float.Parse(sqlReader["cantG"].ToString());
                            victorias = float.Parse(sqlReader["cantG"].ToString());

                            nEporcent = notaEntrenamientos / 100 * 15;
                            nPporcent = notaPartidos / 100 * 15;

                            cnetrosPorcent = centros / 100 * 10;
                            pasesPorcent = pases / 100 * 10;
                            vicPorcent = victorias / 100 * 5;

                            if(13.20 < tiempoDistanciaCorta)
                            {
                                tcPorcent = 1;
                            }
                            else if (12.80 < tiempoDistanciaCorta && tiempoDistanciaCorta < 13)
                            {
                                tcPorcent = 2;
                            }
                            else if (12.60 < tiempoDistanciaCorta && tiempoDistanciaCorta < 12.80)
                            {
                                tcPorcent = 3;
                            }
                            else if (12.40 < tiempoDistanciaCorta && tiempoDistanciaCorta < 12.60)
                            {
                                tcPorcent = 4;
                            }
                            else if (12.20 < tiempoDistanciaCorta && tiempoDistanciaCorta < 12.40)
                            {
                                tcPorcent = 5;
                            }
                            else if (12.0 < tiempoDistanciaCorta && tiempoDistanciaCorta < 12.20)
                            {
                                tcPorcent = 6;
                            }
                            else if (11.75 < tiempoDistanciaCorta && tiempoDistanciaCorta < 12.0)
                            {
                                tcPorcent = 7;
                            }
                            else if (11.50 < tiempoDistanciaCorta && tiempoDistanciaCorta < 11.75)
                            {
                                tcPorcent = 8;
                            }
                            else if (11.25 < tiempoDistanciaCorta && tiempoDistanciaCorta < 11.50)
                            {
                                tcPorcent = 9;
                            }
                            else if (11.0 < tiempoDistanciaCorta && tiempoDistanciaCorta < 11.25)
                            {
                                tcPorcent = 10;
                            }
                            else if (10.75 < tiempoDistanciaCorta && tiempoDistanciaCorta < 11.0)
                            {
                                tcPorcent = 11;
                            }
                            else if (10.50 < tiempoDistanciaCorta && tiempoDistanciaCorta < 10.75)
                            {
                                tcPorcent = 12;
                            }
                            else if (10.25 < tiempoDistanciaCorta && tiempoDistanciaCorta < 10.50)
                            {
                                tcPorcent = 13;
                            }
                            else if (10.0 < tiempoDistanciaCorta && tiempoDistanciaCorta < 10.25)
                            {
                                tcPorcent = 14;
                            }
                            else if (tiempoDistanciaCorta <= 10.0)
                            {
                                tcPorcent = 15;
                            }


                            if (6 < tiempoDistanciaLarga)
                            {
                                tlPorcent = 1;
                            }
                            else if (5.40 < tiempoDistanciaLarga && tiempoDistanciaLarga < 5.50)
                            {
                                tlPorcent = 2;
                            }
                            else if (5.30 < tiempoDistanciaLarga && tiempoDistanciaLarga < 5.40)
                            {
                                tlPorcent = 3;
                            }
                            else if (5.20 < tiempoDistanciaCorta && tiempoDistanciaCorta < 5.30)
                            {
                                tlPorcent = 4;
                            }
                            else if (5.10 < tiempoDistanciaCorta && tiempoDistanciaCorta < 5.20)
                            {
                                tlPorcent = 5;
                            }
                            else if (5 < tiempoDistanciaCorta && tiempoDistanciaCorta < 5.10)
                            {
                                tlPorcent = 6;
                            }
                            else if (4.5 < tiempoDistanciaCorta && tiempoDistanciaCorta < 5.0)
                            {
                                tlPorcent = 7;
                            }
                            else if (4.4 < tiempoDistanciaCorta && tiempoDistanciaCorta < 4.5)
                            {
                                tlPorcent = 8;
                            }
                            else if (4.3 < tiempoDistanciaCorta && tiempoDistanciaCorta < 4.4)
                            {
                                tlPorcent = 9;
                            }
                            else if (4.2 < tiempoDistanciaCorta && tiempoDistanciaCorta < 4.3)
                            {
                                tlPorcent = 10;
                            }
                            else if (4.1 < tiempoDistanciaCorta && tiempoDistanciaCorta < 4.2)
                            {
                                tlPorcent = 11;
                            }
                            else if (4.0 < tiempoDistanciaCorta && tiempoDistanciaCorta < 4.1)
                            {
                                tlPorcent = 12;
                            }
                            else if (3.45 < tiempoDistanciaCorta && tiempoDistanciaCorta < 4.0)
                            {
                                tlPorcent = 13;
                            }
                            else if (3.30 < tiempoDistanciaCorta && tiempoDistanciaCorta < 3.45)
                            {
                                tlPorcent = 14;
                            }
                            else if (tiempoDistanciaCorta <= 3.30)
                            {
                                tlPorcent = 15;
                            }

                            if (saltos <= 25)
                            {
                                saltosPorcent = 1;
                            }
                            else if (25 < saltos && saltos < 30)
                            {
                                saltosPorcent = 2;
                            }
                            else if (30 < saltos && saltos < 32.5)
                            {
                                saltosPorcent = 3;
                            }
                            else if (32.5 < saltos && saltos < 35)
                            {
                                saltosPorcent = 4;
                            }
                            else if (35 < saltos && saltos < 37.5)
                            {
                                saltosPorcent = 5;
                            }
                            else if (37.5 < saltos && saltos < 40)
                            {
                                saltosPorcent = 6;
                            }
                            else if (40 < saltos && saltos < 45)
                            {
                                saltosPorcent = 7;
                            }
                            else if (50 < saltos && saltos < 55)
                            {
                                saltosPorcent = 8;
                            }
                            else if (55 < saltos && saltos < 60)
                            {
                                saltosPorcent = 9;
                            }
                            else if (60 < saltos && saltos < 65)
                            {
                                saltosPorcent = 10;
                            }
                            else if (65 < saltos && saltos < 70)
                            {
                                saltosPorcent = 11;
                            }
                            else if (70 < saltos && saltos < 72.5)
                            {
                                saltosPorcent = 12;
                            }
                            else if (72.5 < saltos && saltos < 75)
                            {
                                saltosPorcent = 13;
                            }
                            else if (75 <= saltos)
                            {
                                saltosPorcent = 15;
                            }

                            if(10 <= victorias)
                            {
                                vicPorcent = 1;
                            }
                            else if (20 < victorias  && victorias < 30)
                            {
                                vicPorcent = 2;
                            }
                            else if (30 < victorias && victorias < 40)
                            {
                                vicPorcent = 2;
                            }
                            else if (40 < victorias && victorias < 50)
                            {
                                vicPorcent = 3;
                            }
                            else if (50 <= victorias)
                            {
                                vicPorcent = 5;
                            }

                            atleta["xSport"] = nEporcent + nPporcent + tcPorcent + tlPorcent + saltosPorcent + cnetrosPorcent + pasesPorcent + vicPorcent;

                        }
                        sqlReader.Close();
                        command.Dispose();
                        connection.Close();

                        connection.Open();
                        query = "select * from posiciones where idPosicion = " + int.Parse(atletasFiltrados[i]["posicion"].ToString());
                        command = new SqlCommand(query, connection);
                        sqlReader = command.ExecuteReader();
                        try
                        {
                            while (sqlReader.Read())
                            {
                                atletasFiltrados[i]["posicion"] = sqlReader["nombrePosicion"].ToString();
                            }
                            sqlReader.Close();
                            command.Dispose();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        connection.Close();
                        



                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    connection.Close();



                }



            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                Console.WriteLine("Codigo de error: " + err.Number);
                Console.WriteLine("Descripcion: " + err.Message);
            }
            atletas["atletas"] = atletasFiltrados;
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


        public static JObject getEstadoLesion()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select * from TiposLesiones";
            ;
            SqlCommand command;
            SqlDataReader sqlReader;

            JObject tipos = new JObject();
            JObject tip;
            JArray tipLesiones = new JArray();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        tip = new JObject();
                        tip["idTipoLesion"] = sqlReader["idTipoLesion"].ToString();
                        tip["tipoLesion"] = sqlReader["tipoLesion"].ToString();
                        tipLesiones.Add(tip);
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
            tipos["tipos"] = tipLesiones;
            return tipos;
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

        public static bool asignarPP(string idAtleta, int semana, int ejercicio, int dia, int cantidad)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
  
            bool ok = false;


            sqlInjection("insert into Planes(semana,correoAtleta) values(" + semana + ",'" + idAtleta +"')");
            try
            {
                sqlInjection("insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(" + semana + ",'" + idAtleta + "'," + dia + "," + ejercicio + "," + cantidad + ")");
                ok = true;
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

        public static bool deleteUniversidad(string nombreUniversidad, string nombrePais)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("delete from Universidades where nombreUniversidad = '"+ nombreUniversidad + "' and nombrePais = '" + nombrePais + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Universidades where nombreUniversidad = '" + nombreUniversidad + "' and nombrePais= '" + nombrePais + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("0"))
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

        public static bool eliminarPosicion(string nombrePosicion, string deporte)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
                sqlInjection("delete from Posiciones where nombreDeporte = '" + deporte + "' and nombrePosicion = '" + nombrePosicion + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Posiciones where nombrePosicion = '" + nombrePosicion + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("0"))
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

        public static bool agregarPais(string nombrePais)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {
               
                sqlInjection("insert into Paises(nombre) values('" + nombrePais + "')");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Paises where nombre = '" + nombrePais + "'", connection);
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


        public static bool agregarDeporte(string deporte)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {

                sqlInjection("insert into Deportes(nombreDeporte) values('" + deporte + "')");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Deportes where nombreDeporte = '" + deporte + "'", connection);
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

        public static bool agregarIdioma(string idioma)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {

                sqlInjection("insert into Idiomas(idioma) values('" + idioma + "')");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Idiomas where idioma = '" + idioma + "'", connection);
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

        public static bool eliminarIdioma(string idioma)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {

                sqlInjection("delete from Idiomas where idioma = '" + idioma + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Idiomas where idioma = '" + idioma + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("0"))
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

        public static bool eliminarDeporte(string deporte)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {

                sqlInjection("delete from Deportes where nombreDeporte= '" + deporte + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Deportes where nombreDeporte = '" + deporte + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("0"))
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


        public static bool eliminarPais(string nombrePais)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            SqlDataReader sqlReader;
            bool ok = false;
            try
            {

                sqlInjection("delete from Paises where nombre = '" + nombrePais + "'");
                connection.Open();
                command = new SqlCommand("select count(*) as exist from Paises where nombre = '" + nombrePais + "'", connection);
                sqlReader = command.ExecuteReader();
                try
                {
                    while (sqlReader.Read())
                    {
                        if (sqlReader["exist"].ToString().Equals("0"))
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

        public static JObject getSemanasPAtleta(string emailJugador)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            //Query para obtener que semanas tiene planes
            string query = "select semana from PlanesEjercicios where correoAtleta = '" + emailJugador + "' group by semana";
            SqlCommand command;
            SqlDataReader sqlReader;

            string res = null;

            JObject plan = new JObject();
            JArray semanas = new JArray();
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
            plan["semanas"] = semanas;
            return plan;
        }

        public static JObject getPlanesAtleta(string emailJugador, int semana)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            //Query para obtener que semanas tiene planes
            string query = "select * from PlanesEjercicios as P join Ejercicios as E on P.idEjercicio = E.idEjercicio where correoAtleta = '" + emailJugador + "' and semana = " + semana;
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
                        ejercicio["nombreEjercicio"] = sqlReader["nombreEjercicio"].ToString();
                        ejercicio["cantidad"] = int.Parse(sqlReader["cantidad"].ToString());
                        int dia = int.Parse(sqlReader["dia"].ToString());
                        if(dia == 1)
                        {
                            ejercicio["dia"] = "Lunes";
                        }
                        if (dia == 2)
                        {
                            ejercicio["dia"] = "Martes";
                        }
                        if (dia == 3)
                        {
                            ejercicio["dia"] = "Miercoles";
                        }
                        if (dia == 4)
                        {
                            ejercicio["dia"] = "Jueves";
                        }
                        if (dia == 5)
                        {
                            ejercicio["dia"] = "Viernes";
                        }
                        if (dia == 6)
                        {
                            ejercicio["dia"] = "Sabado";
                        }
                        if (dia == 7)
                        {
                            ejercicio["dia"] = "Domingo";
                        }
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