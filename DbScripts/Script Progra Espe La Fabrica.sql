/*
formato smalldatetime DD/MM/YYYY hh:mm:ss

use master
go
drop database LaFabricaDB;
*/


create database LaFabricaDB;
go
use LaFabricaDB;


create table Paises
(
	nombre varchar(40) primary key
)
/*
drop table Paises
select * from Paises
*/


create table Provincias
(
	 nombreProvincia varchar(40)
	,nombrePais varchar(40)

	,primary key(nombreProvincia, nombrePais)

	,foreign key(nombrePais) references Paises(nombre) 
)
/*
drop table Provincias
select * from Provincias
*/


create table Universidades
(
	 nombreUniversidad varchar(40)
	,nombrePais varchar(40)

	,primary key(nombreUniversidad, nombrePais)

	,foreign key(nombrePais) references Paises(nombre) 
)
/*
drop table Universidades
select * from Universidades
*/


create table Deportes
(
	 nombreDeporte varchar(40) primary key
)
/*
drop table Deportes
select * from Deportes
*/


create table Posiciones
(
	 nombreDeporte varchar(40)
	,idPosicion int
	,nombrePosicion varchar(40)

	,primary key(nombreDeporte, idPosicion)

	,foreign key(nombreDeporte) references Deportes(nombreDeporte) 
)
/*
drop table Posiciones
select * from Posiciones
*/


create table Entrenadores
(
	 nombre varchar(30) not null
	,apellido varchar(30) not null
	,correo varchar(30) primary key
	,fechaInscripcion date not null
	,password varchar(max) not null
	,activo bit not null
	,pais varchar(40) not null--FK
	,universidad varchar(40) not null--FK

	,foreign key(pais) references Paises(nombre)
	,foreign key(universidad, pais) references Universidades(nombreUniversidad, nombrePais)
)
/*
drop table Entrenadores
select * from Entrenadores
*/


create table Temporadas
(
	 nombreTemporada varchar(40) primary key
)
/*
drop table Temporadas
select * from Temporadas
*/


create table Equipos
(
	 nombreEquipo varchar(40) unique--PK
	,correoEntrenador varchar(30)--PK,FK
	,nombreTemporada varchar(40) not null

	,primary key(nombreEquipo, correoEntrenador)

	,foreign key(correoEntrenador) references Entrenadores(correo)
	,foreign key(nombreTemporada) references Temporadas(nombreTemporada)
)
/*
drop tanle Equipos
select * from Equipos
*/


create table Atletas
(
	 nombre varchar(30) not null
	,apellido varchar(30) not null
	,cedula int not null
	,provincia varchar(40) not null--FK
	,fechaNacimiento date not null
	,activo bit not null
	,correo1 varchar(30) primary key
	,correo2 varchar(30)
	,telefono varchar(10)
	,foto varchar(120)
	,fechaInscripcion date not null
	,pais varchar(40) not null--FK
	,universidad varchar(40) not null--FK
	,password varchar(max) not null
	,deporte varchar(40) not null
	,altura numeric(4,2) not null
	,peso numeric(5,2) not null
	,posicion int not null--FK
	,posicionSecundaria int --FK

	--,numeroPartidos int not null--calculado
	--,promedioCalificacionPartidos numeric(5,2) not null --calculado

	,notaXSport decimal(4,2) not null
	,nombreEquipo varchar(40)--FK
	,correoEntrenador varchar(30)--FK

	-- juegosTotal int not null
	-- juegosTotalGanados int not null
	-- juegosTotalPerdidos int not null
	-- juegosTotalEmpatados int not null
	-- totalGoles int not null
	-- totalAsistencias int not null
	-- totalBalonesRecuperados int not null
	-- balonesRecuperadosPorPartido int not null
	-- total pases
	-- % de pases ok
	-- total centros
	-- % centros ok
	-- total tarjetas amarillas
	-- total tarjetas rojas

	--		Solo portero		--
	-- total penales detenidos
	-- total remates salvados
	-- total goles recibidos!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
	-- % remates salvados

	,foreign key(provincia, pais) references Provincias(nombreProvincia, nombrePais)
	,foreign key(pais) references Paises(nombre)
	,foreign key(deporte, posicion) references Posiciones(nombreDeporte, idPosicion)
	,foreign key(deporte, posicionSecundaria) references Posiciones(nombreDeporte, idPosicion)
	,foreign key(nombreEquipo, correoEntrenador) references Equipos(nombreEquipo, correoEntrenador)
	,foreign key(correoEntrenador) references Entrenadores(correo)
)
/*
select * from Atletas
drop table Atletas
*/


create table TiposLesiones
(
	 idTipoLesion int primary key
	,tipoLesion varchar(40) not null
)
/*
Tipo Lesion:
	Mayor
	Menor
	Leve

drop table TiposLesiones
select * from TiposLesiones
*/


create table Lesiones
(
	 correoAtleta varchar(30)
	,fechaInicio date not null
	,fechaFinal date not null
	,descripcion varchar(300) not null
	,idTipoLesion int not null--FK
	,lugar varchar(40)--Ubicacion de la lesion en el we

	,primary key(correoAtleta, lugar)

	,foreign key(idTipoLesion) references TiposLesiones(idTipoLesion) 
)
/*
drop table Lesiones
select * from Lesiones
*/


create table EstadosDePartido
(
	 idEstado int primary key
	,estado varchar(40) unique not null
)
/*
drop table Lesiones
select * from Lesiones
*/


create table Partidos
(
	 idPartido int--PK
	,correoAtleta varchar(30)--PK,FK
	,temporada varchar(40) not null--FK

	,idEstado int not null
	,cantidadGoles int not null
	,cantidadAsistencias int not null
	,balonesRecuperados int not null
	,cantidadPases int not null
	,cantidadPasesExitosos int not null
	,cantidadCentros int not null
	,cantidadCentrosExitosos int not null
	,cantidadTarjetasAmarillas int not null
	,cantidadTarjetasRojas int not null

	--Weas de porteror--
	,cantidadPenales int not null	
	,cantidadRematesSalvados int not null
	,cantidadGolesRecibidos int not null

	,primary key(idPartido, correoAtleta)

	,foreign key(correoAtleta) references Atletas(correo1)
	,foreign key(temporada) references Temporadas(nombreTemporada)
	,foreign key(idEstado) references EstadosDePartido(idEstado)
	
)
/*
estado:
	Gano
	Perdio
	Empate
*/
--drop table Partido
--select * from Partido


create table Roles
(
	 idRol int primary key
	,nombreRol varchar(40) not null
)
/*
rol:
	0 Administrador
	1 Scout

drop table Roles
select * from Roles
*/


create table Trabajadores
(
	 nombre varchar(30) not null
	,apellido varchar(30) not null
	,correo varchar(30) primary key
	,fechaInscripcion date not null
	,password varchar(max) not null
	,activo bit not null

	,rol int not null--FK

	foreign key(rol) references Roles(idRol)
)
/*
drop table Trabajador
select * from Trabajador
*/


create table Ejercicios
(
	 idEjercicio int identity(0,1) primary key
	,nombreEjercicio varchar(40) unique not null
)
--select * from Ejercicios
--drop table Ejercicios


create table Planes
(
	 semana int
		,check(semana > 0 and semana < 52)--PK--de [1, 52]
	,correoAtleta varchar(30)--PK,FK

	,primary key(semana, correoAtleta)

	,foreign key(correoAtleta) references Atletas(correo1)
)
/*
select * from Ejercicios
drop table Ejercicios
*/


create table PlanesEjercicios
(
	 semana int--PK,FK	
	,correoAtleta varchar(30)--PK,FK
	,idEjercicio int--PK,FK
	,cantidad int not null

	,primary key(semana, correoAtleta, idEjercicio)
	,foreign key(semana, correoAtleta) references Planes(semana, correoAtleta)
	,foreign key(idEjercicio) references Ejercicios(idEjercicio)
)
/*
select * from PlanesEjercicios
drop table PlanesEjercicios
*/


create table Entrenamientos
(
	 idEntrenamiento int
	,correoAtleta varchar(30)
	,calificacionEntrenamiento numeric(5,2) not null
	,tiempoPruebaDistanciaCorta numeric(4,2) not null
	,tiempoPruebaDistanciaLarga numeric(4,2) not null
	,salto numeric(4,2) not null
	,tiempoPruebaHabilidad numeric(4,2) not null
	,pruebaFisicaPace numeric(4,2) not null
	,pruebaFisicaHR numeric(4,2) not null

	,primary key(idEntrenamiento, correoAtleta)
	
	,foreign key(correoAtleta) references Atletas(correo1)
)
/*
select * from Entrenamientos
drop table Entrenamientos
*/





/*
drop table PlanesEjercicios
drop table Planes
drop table Ejercicios
drop table Trabajador
drop table Roles
drop table Entrenadores
drop table AtletasEquipos
drop table Equipos
drop table Partidos
drop table Temporadas
drop table Lesiones
drop table TiposLesiones
drop table Atletas
drop table Posiciones
drop table Deportes
drop table Universidades
drop table Provincias
drop table Paises
*/


--EncryptByPassPhrase ('FraseClave','TextoAEncriptar')


go


create procedure proc_registrarAtleta
	 @nombre varchar(30)
	,@apellido varchar(30)
	,@cedula int
	,@provincia varchar(40)
	,@fechaNacimiento date
	,@correo1 varchar(30)
	,@correo2 varchar(30)
	,@telefono varchar(10)
	,@foto varchar(120)
	,@pais varchar(40)
	,@universidad varchar(40)
	,@password varchar(8)
	,@deporte varchar(40)
	,@altura numeric(4,2)
	,@peso numeric(5,2)
	,@posicion int
	,@posicionSecundaria int
AS
BEGIN
	insert into Atletas(nombre,apellido,cedula,provincia,fechaNacimiento,activo,correo1,correo2,telefono,foto,fechaInscripcion,pais,universidad,password,deporte,altura,peso,posicion,posicionSecundaria,notaXSport,nombreEquipo,correoEntrenador)
		values
		(
			 @nombre
			,@apellido
			,@cedula
			,@provincia
			,@fechaNacimiento
			,1
			,@correo1
			,@correo2
			,@telefono
			,@foto
			,CONVERT(VARCHAR(10), getdate(), 103)
			,@pais
			,@universidad
			,CONVERT(varchar(max), HASHBYTES ('SHA2_512', @password) ,2)
			,@deporte
			,@altura
			,@peso
			,@posicion
			,@posicionSecundaria
			,0
			,null
			,null
		)
END
--drop procedure proc_registrarAtleta


go


create procedure proc_registrarEntrenador
	 @nombre varchar(30)
	,@apellido varchar(30)
	,@correo varchar(30)
	,@password varchar(max)
	,@pais varchar(40)
	,@universidad varchar(40)
AS
BEGIN
	insert into Entrenadores(nombre,apellido,correo,fechaInscripcion,password,activo,pais,universidad)
		values
		(
			 @nombre
			,@apellido
			,@correo
			,CONVERT(VARCHAR(10), getdate(), 103)
			,CONVERT(varchar(max), HASHBYTES ('SHA2_512', @password) ,2)
			,1
			,@pais
			,@universidad
		)
END
--drop procedure proc_registrarEntrenador


go


create procedure proc_registrarTrabajador
	 @nombre varchar(30)
	,@apellido varchar(30)
	,@correo varchar(30)
	,@password varchar(max)
	,@rol int
AS
BEGIN
	insert into Trabajadores(nombre,apellido,correo,fechaInscripcion,password,activo,rol)
		values
		(
			 @nombre
			,@apellido
			,@correo
			,CONVERT(VARCHAR(10), getdate(), 103)
			,CONVERT(varchar(max), HASHBYTES ('SHA2_512', @password) ,2)
			,1
			,@rol
		)
END
--drop procedure proc_registrarTrabajador




/*
create procedure actualizarDatosTarjeta
	 @userName varchar(60)
	,@nombreTitular varchar(60)
	,@tarjeta varchar(60)
	,@fechaExp date
AS
BEGIN
	if @tarjeta is not null	
	begin
		if dbo.RegexMatch(@tarjeta, N'(?>^3[47][0-9]{13}$|^(6541|6556)[0-9]{12}$|^389[0-9]{11}$|^3(?:0[0-5]|[68][0-9])[0-9]{11}$|^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$|^63[7-9][0-9]{13}$|^(?:2131|1800|35\d{3})\d{11}$|^9[0-9]{15}$|^(6304|6706|6709|6771)[0-9]{12,15}$|^(5018|5020|5038|6304|6759|6761|6763)[0-9]{8,15}$|^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$|^(6334|6767)[0-9]{12}|(6334|6767)[0-9]{14}|(6334|6767)[0-9]{15}$|^(4903|4905|4911|4936|6333|6759)[0-9]{12}|(4903|4905|4911|4936|6333|6759)[0-9]{14}|(4903|4905|4911|4936|6333|6759)[0-9]{15}|564182[0-9]{10}|564182[0-9]{12}|564182[0-9]{13}|633110[0-9]{10}|633110[0-9]{12}|633110[0-9]{13}$|^(62[0-9]{14,17})$|^4[0-9]{12}(?:[0-9]{3})?$|^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$)') = 1
		begin
			update Usuarios
				set
					 nombreTitular = @nombreTitular
					,tarjeta = ENCRYPTBYPASSPHRASE('smas', @tarjeta)
					,fechaExp = @fechaExp
				where userName = @userName
		end
		else
			RAISERROR(N'Tarjeta no valida!', 15, 254)
	end
	else
	begin
		update Usuarios
			set
				 nombreTitular = @nombreTitular
				,tarjeta = @tarjeta
				,fechaExp = @fechaExp
			where userName = @userName
	end
END
--drop procedure actualizarDatosTarjeta

go

create procedure getPassword
	 @userName varchar(60)
AS
BEGIN
    select CONVERT(VARCHAR(60), DECRYPTBYPASSPHRASE('smas',password)) as password from Usuarios where userName = @userName
END

go

create procedure getUser
	 @userName varchar(60)
AS
BEGIN
    select
		 userName
		,CONVERT(VARCHAR(60), DECRYPTBYPASSPHRASE('smas',password)) as password
		,nombre,apellido1,apellido2,telefono,correo,nombreTitular
		,CONVERT(VARCHAR(60), DECRYPTBYPASSPHRASE('smas',tarjeta)) as tarjeta
		,fechaExp
	from Usuarios where userName = @userName
END
--

go

create procedure deleteUsuario
	 @userName varchar(60)
AS
BEGIN
    delete Estudiantes where userName = @userName
	delete Reservaciones where usuario = @userName
	delete Usuarios where userName = @userName
END
--drop procedure deleteUsuario
go

create procedure deleteUsuarios
AS
BEGIN
    delete Estudiantes
	delete Reservaciones
	delete Usuarios
END
--drop procedure deleteUsuarios

go

create procedure deleteVuelo
	 @id int
AS
BEGIN
    delete Escalas where id_vuelo = @id
	delete Vuelos where id = @id
END


go


create procedure deleteVuelos
as
BEGIN
    delete Escalas
	delete Vuelos
END
--drop procedure deleteVuelos

--select * from Aviones
--create trigger revisar


go


create procedure crearReservacion
	 @userName varchar(60)
	,@idVuelo int
AS
BEGIN
    insert into Reservaciones(usuario,idVuelo,fecha) values(@userName,@idVuelo,getDate())
END
--drop procedure crearReservacion


go


create procedure actualizarPeso
	 @userName varchar(60)
	,@idVuelo int
	,@peso int
AS
BEGIN
	if(@peso <= (select (select pesoMaximo from Vuelos join Aviones on Vuelos.avion = Aviones.id where Vuelos.id = @idVuelo)/(select count(asiento) as cantidadAsientos from Aviones join Asientos on Aviones.id = Asientos.idAvion join Vuelos on Vuelos.avion = Aviones.id where Vuelos.id = @idVuelo) as pesoMaximoPorPasajero))
	begin
		update Reservaciones
			set peso = @peso
		where usuario = @userName and idVuelo = @idVuelo
	end
	else
	begin
		RAISERROR(N'No se permite tanto peso!', 15, 253)
	end
END
--drop procedure actualizarPeso


go


create procedure actualizarAsiento
	 @userName varchar(60)
	,@idVuelo int
	,@asiento varchar(4)
AS
BEGIN
	if(@asiento in (select asiento from Asientos join Vuelos on Asientos.idAvion = Vuelos.avion where Vuelos.id = @idVuelo and asiento not in (select asiento from Reservaciones where asiento is not null)))
	begin
		update Reservaciones
			set asiento = @asiento
		where
			usuario = @userName
			and idVuelo = @idVuelo
	end
	else
	begin
		RAISERROR(N'El asiento ya esta ocupado!', 15, 252)
	end
END
--drop procedure actualizarAsiento


go


create procedure getAsientosOcupados
	@idVuelo int	
AS
BEGIN		
select
	asiento
from Asientos join Vuelos on Asientos.idAvion = Vuelos.avion
where
	Vuelos.id = @idVuelo
	and asiento in (select asiento from Reservaciones where asiento is not null)
END
--drop procedure getAsientosOcupados


go


create procedure getAsientosDesocupados
	@idVuelo int	
AS
BEGIN		
select
	asiento
from Asientos join Vuelos on Asientos.idAvion = Vuelos.avion
where
	Vuelos.id = @idVuelo
	and asiento not in (select asiento from Reservaciones where asiento is not null)
END
--drop procedure getAsientosDesocupados


go


create procedure getCantidadAsientos
	@idAvion int
AS
BEGIN
	select count(asiento) as cantidadAsientos from Asientos where idAvion = @idAvion
END


go


create trigger trgUpdateMillas
on Reservaciones
after insert
AS
BEGIN
	update Estudiantes set puntos = puntos+10 where userName = (select usuario FROM INSERTED)
END
--drop trigger trgUpdateMillas


go


create procedure resetPuntos
AS
BEGIN
	update Estudiantes set puntos = 0
END
--drop procedure trgUpdateMillas


go


create view VuelosView
as
select
	 id
	,nombre as inicio
	,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as fin
from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA
--drop view VuelosView
GO

--select * from VuelosView





--exec getAsientosOcupados @idVuelo = 0
--exec actualizarAsiento @userName = 'MajinLoop', @idVuelo = 0, @asiento = '2A'
--exec actualizarAsiento @userName = 'ollirum', @idVuelo = 0, @asiento = '2A'
--select * from Reservaciones
--select * from Vuelos
--update Reservaciones set asiento = null where usuario = 'MajinLoop' and idVuelo = 0
--update Reservaciones set asiento = null where usuario = 'ollirum' and idVuelo = 0
--exec getCantidadAsientos @idAvion = 4
--select Vuelos.id as idVuelo, nombre as sal, aeropuertoIni as iataSal, aeropuertoFin as iataDes, (select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as des, avion from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA join Aviones on Aviones.id = Vuelos.avion where Vuelos.id = 0

/*
--Cual es mas eficiente?
select
inicio,final
from
	(select id,nombre as inicio from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA) as A
	join
	(select id,nombre as final from Vuelos join Aeropuertos on Vuelos.aeropuertoFin = Aeropuertos.codigoIATA) as B
		on A.id = B.id
--vs

select
	 nombre as inicio
	,(select nombre from Aeropuertos where codigoIATA = aeropuertoFin) as final
from Vuelos join Aeropuertos on Vuelos.aeropuertoIni = Aeropuertos.codigoIATA
*/


*/