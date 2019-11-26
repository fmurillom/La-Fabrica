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
	,idPosicion int --identity(0,1)
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
	 nombreEquipo varchar(40)--PK
	,correoEntrenador varchar(30) not null--FK

	,primary key(nombreEquipo)

	,foreign key(correoEntrenador) references Entrenadores(correo)
)
/*
drop table Equipos
select * from Equipos
*/


create table EquiposTemporadas
(
	 nombreEquipo varchar(40)--PK
	,nombreTemporada varchar(40)--PK,FK

	,primary key(nombreEquipo, nombreTemporada)

	,foreign key(nombreTemporada) references Temporadas(nombreTemporada)
)
/*
drop table Equipos
select * from Equipos
*/


create table Atletas
(
	 nombre varchar(30) not null
	,apellido varchar(30) not null
	,cedula int unique not null
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
	,altura numeric(5,2) not null
	,peso numeric(5,2) not null
	,posicion int not null--FK
	,posicionSecundaria int --FK

	--,numeroPartidos int not null--calculado
	--,promedioCalificacionPartidos numeric(5,2) not null --calculado

	,notaXSport decimal(4,2) not null
	,nombreEquipo varchar(40)--FK

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
	,foreign key(nombreEquipo) references Equipos(nombreEquipo)
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
	,calificacionPartido numeric(5,2) not null
	,idEstado int not null
	,cantidadGoles int not null
	,cantidadAsistencias int not null
	,balonesRecuperados int not null
	,cantidadPasesFallidos int not null
	,cantidadPasesExitosos int not null
	,cantidadCentrosFallidos int not null
	,cantidadCentrosExitosos int not null
	,cantidadTarjetasAmarillas int not null
	,cantidadTarjetasRojas int not null

	--Weas de portero--
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
	,dia int not null
		,check(dia > 0 and dia < 8)
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


create procedure proc_logInAtleta
	 @correo varchar(30)
	,@password varchar(8)
AS
BEGIN
	if (select password from Atletas where correo1 = @correo) = (SELECT CONVERT(varchar(max), HASHBYTES ('SHA2_512', @password) ,2))
	begin
		select 1 as Success
	end
	else
	begin		
		select 0 as Success
	end
END
--drop procedure proc_logInAtleta


go


create procedure proc_logInEntrenador
	 @correo varchar(30)
	,@password varchar(8)
AS
BEGIN
	if (select password from Entrenadores where correo = @correo) = (SELECT CONVERT(varchar(max), HASHBYTES ('SHA2_512', @password) ,2))
	begin
		select 1 as Success
	end
	else
	begin		
		select 0 as Success
	end
END
--drop procedure proc_logInEntrenador


go


create procedure proc_logInTrabajador
	 @correo varchar(30)
	,@password varchar(8)
AS
BEGIN
	if (select password from Trabajadores where correo = @correo) = (SELECT CONVERT(varchar(max), HASHBYTES ('SHA2_512', @password) ,2))
	begin
		select 1 as Success
	end
	else
	begin		
		select 0 as Success
	end
END
--drop procedure proc_logInTrabajador


--SELECT CONVERT(varchar(max), HASHBYTES ('SHA2_512', 'Doom') ,2)


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
	,@altura numeric(5,2)
	,@peso numeric(5,2)
	,@posicion int
	,@posicionSecundaria int
AS
BEGIN
	insert into Atletas(nombre,apellido,cedula,provincia,fechaNacimiento,activo,correo1,correo2,telefono,foto,fechaInscripcion,pais,universidad,password,deporte,altura,peso,posicion,posicionSecundaria,notaXSport,nombreEquipo)
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


go


create procedure proc_getCantEntrenamientos
	 @correoAtleta varchar(30)
AS
BEGIN
	select count(*) as cantEntrenamientos from Entrenamientos where correoAtleta = @correoAtleta
	--select count(*) as cantEntrenamientos from Entrenamientos where correoAtleta = 'correo4@algo.com'
END
--drop procedure proc_getAVGcalificacionEntrenamientos


go


create procedure proc_AVGcalificacionEntrenamientos
	 @correoAtleta varchar(30)
AS
BEGIN
	select convert(numeric(5,2), (select avg(calificacionEntrenamiento) from Entrenamientos where correoAtleta = @correoAtleta))
END
--drop procedure proc_getAVGcalificacionEntrenamientos


go


create procedure proc_getCantPartidos
	 @correoAtleta varchar(30)
AS
BEGIN
	select count(*) as cantPartidos from Partidos where correoAtleta = @correoAtleta
	--select count(*) as cantPartidos from Partidos where correoAtleta = 'correo3@algo.com'
END
--drop procedure proc_getCantPartidos


go


create procedure proc_AVGcalificacionPartidos
	 @correoAtleta varchar(30)
AS
BEGIN
	select convert(numeric(5,2), (select avg(calificacionPartido) from Partidos where correoAtleta = @correoAtleta)) as promedioCalificacionPartidos
	--select convert(numeric(5,2), (select avg(calificacionPartido) from Partidos where correoAtleta = 'correo1@algo.com')) as promedioCalificacionPartidos
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_AVGtiempoPruebaDistanciaCorta
	 @correoAtleta varchar(30)
AS
BEGIN
	select convert(numeric(4,2), (select avg(tiempoPruebaDistanciaCorta) from Entrenamientos where correoAtleta = @correoAtleta)) as promedioTiempoPruebaDistanciaCorta
	--select convert(numeric(5,2), (select avg(tiempoPruebaDistanciaCorta) from Entrenamientos where correoAtleta = 'correo4@algo.com')) as promedioTiempoPruebaDistanciaCorta
	
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_AVGtiempoPruebaDistanciaLarga
	 @correoAtleta varchar(30)
AS
BEGIN
	select convert(numeric(4,2), (select avg(tiempoPruebaDistanciaLarga) from Entrenamientos where correoAtleta = @correoAtleta)) as promedioTiempoPruebaDistanciaLarga
	--select convert(numeric(5,2), (select avg(tiempoPruebaDistanciaLarga) from Entrenamientos where correoAtleta = 'correo4@algo.com')) as promedioTiempoPruebaDistanciaLarga
	
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_bestoTiempoPruebaDistanciaCorta
	 @correoAtleta varchar(30)
AS
BEGIN	
	select min(tiempoPruebaDistanciaCorta) as bestoTiempoPruebaDistanciaCorta from Entrenamientos where correoAtleta = @correoAtleta
	--select min(tiempoPruebaDistanciaCorta) as bestoTiempoPruebaDistanciaCorta from Entrenamientos where correoAtleta = 'correo4@algo.com'
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_bestoTiempoPruebaDistanciaLarga
	 @correoAtleta varchar(30)
AS
BEGIN	
	select min(tiempoPruebaDistanciaLarga) as bestoTiempoPruebaDistanciaLarga from Entrenamientos where correoAtleta = @correoAtleta
	--select min(tiempoPruebaDistanciaCorta) as bestoTiempoPruebaDistanciaLarga from Entrenamientos where correoAtleta = 'correo4@algo.com'
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_AVGsalto
	 @correoAtleta varchar(30)
AS
BEGIN
	select convert(numeric(4,2), (select avg(salto) from Entrenamientos where correoAtleta = @correoAtleta)) as promedioSalto
	--select convert(numeric(5,2), (select avg(salto) from Entrenamientos where correoAtleta = 'correo4@algo.com')) as promedioSalto
	
END
--drop procedure proc_AVGcalificacionPartidos



go


create procedure proc_bestoSalto
	 @correoAtleta varchar(30)
AS
BEGIN
	select max(salto) as bestoSalto from Entrenamientos where correoAtleta = @correoAtleta
	--select max(salto) as bestoSalto from Entrenamientos where correoAtleta = 'correo4@algo.com'
	
	
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_AVGtiempoPruebaHabilidad
	 @correoAtleta varchar(30)
AS
BEGIN
	select convert(numeric(4,2), (select avg(tiempoPruebaHabilidad) from Entrenamientos where correoAtleta = @correoAtleta)) as promedioTiempoPruebaHabilidad
	--select convert(numeric(5,2), (select avg(tiempoPruebaHabilidad) from Entrenamientos where correoAtleta = 'correo4@algo.com')) as promedioTiempoPruebaHabilidad
	
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_bestoTiempoPruebaHabilidad
	 @correoAtleta varchar(30)
AS
BEGIN
	select min(tiempoPruebaHabilidad) as bestoTiempoPruebaHabilidad from Entrenamientos where correoAtleta = @correoAtleta
	--select max(tiempoPruebaHabilidad) as bestoTiempoPruebaHabilidad from Entrenamientos where correoAtleta = 'correo4@algo.com'
END
--drop procedure proc_AVGcalificacionPartidos


go


create procedure proc_getCantJuegosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select count(*) as cantJuegosPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select count(*) as cantJuegosPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getCantJuegosGanadosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select count(*) as cantJuegosGanadosPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada and idEstado = 2
	--select count(*) as cantJuegosGanadosPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1' and idEstado = 0
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getCantJuegosPerdidosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select count(*) as cantJuegosPerdidosPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada and idEstado = 0
	--select count(*) as cantJuegosPerdidosPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1' and idEstado = 0
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getCantJuegosEmpatadosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select count(*) as cantJuegosEmpatadosPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada and idEstado = 1
	--select count(*) as cantJuegosEmpatadosPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1' and idEstado = 0
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getCantGolesPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select sum(cantidadGoles) as cantGolesPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select sum(cantidadGoles) as cantGolesPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
	
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getCantAsistenciasPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select sum(cantidadAsistencias) as cantAsistenciasPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select sum(cantidadAsistencias) as cantAsistenciasPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
	
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getBalonesRecuperadosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select sum(balonesRecuperados) as cantBalonesRecuperadosPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select sum(balonesRecuperados) as cantBalonesRecuperadosPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
	
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getAVGbalonesRecuperadosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select avg(balonesRecuperados) as promedioBalonesRecuperadosPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select avg(balonesRecuperados) as promedioBalonesRecuperadosPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
	
END
--drop procedure proc_getCantPartidos


go



create procedure proc_getTotalPasesTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select (select sum(cantidadPasesFallidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada) + (select sum(cantidadPasesExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada) as totalPasesPorTemporada
	--select (select sum(cantidadPasesFallidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1') + (select sum(cantidadPasesExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1') as totalPasesPorTemporada
END
--drop procedure proc_getTotalPasesTemporada


go


create procedure proc_getPorcentajePasesExitososPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	IF	(select sum(cantidadPasesFallidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
		+
		(select sum(cantidadPasesExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
		<>
		0
	begin
		select
		convert
		(
			 numeric(5,2)
			,(
				convert(numeric(5,2), (select sum(cantidadPasesExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada))
				*
				100
				/
				convert
				(
					numeric(5,2),
					(
						(select sum(cantidadPasesFallidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
						+
						(select sum(cantidadPasesExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
					)
				)
			)
		)
		as porcentajePasesExitososPorTemporada
	end
	ELSE
	begin
		select 0.00 as porcentajePasesExitososPorTemporada
	end
	/*
	select
		convert
		(
			 numeric(5,2)
			,(
				convert(numeric(5,2), (select sum(cantidadPasesExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'))
				*
				100
				/
				convert
				(
					numeric(5,2),
					(
						(select sum(cantidadPasesFallidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
						+
						(select sum(cantidadPasesExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
					)
				)
			)
		)
		as porcentajePasesExitososPorTemporada
	*/
	--select (select sum(cantidadPasesFallidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1') + (select sum(cantidadPasesExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
END
--drop procedure proc_getPorcentajePasesExitososPorTemporada


go


create procedure proc_getTotalCentrosTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select (select sum(cantidadCentrosFallidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada) + (select sum(cantidadCentrosExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada) as totalCentrosPorTemporada
	--select (select sum(cantidadCentrosFallidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1') + (select sum(cantidadCentrosExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1') as totalCentrosPorTemporada
END
--drop procedure proc_getTotalPasesTemporada


go


create procedure proc_getPorcentajeCentrosExitososPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	IF	(select sum(cantidadCentrosFallidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
		+
		(select sum(cantidadCentrosExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
		<>
		0
	begin
		select
		convert
		(
			 numeric(5,2)
			,(
				convert(numeric(5,2), (select sum(cantidadCentrosExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada))
				*
				100
				/
				convert
				(
					numeric(5,2),
					(
						(select sum(cantidadCentrosFallidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
						+
						(select sum(cantidadCentrosExitosos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
					)
				)
			)
		)
		as porcentajeCentrosExitososPorTemporada

	end
	ELSE
	begin
		select 0.00 as porcentajeCentrosExitososPorTemporada
	end
	/*
	select
		convert
		(
			 numeric(5,2)
			,(
				convert(numeric(5,2), (select sum(cantidadCentrosExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'))
				*
				100
				/
				convert
				(
					numeric(5,2),
					(
						(select sum(cantidadCentrosFallidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
						+
						(select sum(cantidadCentrosExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
					)
				)
			)
		)
		as porcentajeCentrosExitososPorTemporada
	*/
	--select (select sum(cantidadPasesFallidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1') + (select sum(cantidadPasesExitosos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
END
--drop procedure proc_getPorcentajePasesExitososPorTemporada


go


create procedure proc_getCantTarjetasAmarillasPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select sum(cantidadTarjetasAmarillas) as cantidadTarjetasAmarillas from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select sum(cantidadTarjetasAmarillas) as cantidadTarjetasAmarillas from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
	
END
--drop procedure proc_getCantPartidos


go


create procedure proc_getCantTarjetasRojasPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select sum(cantidadTarjetasRojas) as cantTarjetasRojasPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select sum(cantidadTarjetasRojas) as cantTarjetasRojasPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
END
--drop procedure proc_getCantTarjetasRojasPorTemporada


go


create procedure proc_getCantPenalesPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select sum(cantidadPenales) as cantPenalesPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select sum(cantidadTarjetasRojas) as cantPenalesPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
END
--drop procedure proc_getCantPenalesPorTemporada


go


create procedure proc_getCantRematesSalvadosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	select sum(cantidadRematesSalvados) as cantRematesSalvadosPorTemporada from Partidos where correoAtleta = @correoAtleta and temporada = @temporada
	--select sum(cantidadRematesSalvados) as cantRematesSalvadosPorTemporada from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'
END
--drop procedure proc_getCantRematesSalvadosPorTemporada


go


create procedure proc_getPorcentajeRematesSalvadosPorTemporada
	 @correoAtleta varchar(30)
	,@temporada varchar(40)
AS
BEGIN
	IF	(select sum(cantidadGolesRecibidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
		+
		(select sum(cantidadRematesSalvados) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
		<>
		0
	begin
		select
		convert
		(
			 numeric(5,2)
			,(
				convert(numeric(5,2), (select sum(cantidadRematesSalvados) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada))
				*
				100
				/
				convert
				(
					numeric(5,2),
					(
						(select sum(cantidadGolesRecibidos) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
						+
						(select sum(cantidadRematesSalvados) from Partidos where correoAtleta = @correoAtleta and temporada = @temporada)
					)
				)
			)
		)
		as porcentajeRematesSalvadosPorTemporada
	end
	ELSE
	begin
		select 0.00 as porcentajeRematesSalvadosPorTemporada
	end
/*
	IF	(select sum(cantidadGolesRecibidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
		+
		(select sum(cantidadRematesSalvados) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
		<>
		0
	begin
		select
		convert
		(
			 numeric(5,2)
			,(
				convert(numeric(5,2), (select sum(cantidadRematesSalvados) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1'))
				*
				100
				/
				convert
				(
					numeric(5,2),
					(
						(select sum(cantidadGolesRecibidos) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
						+
						(select sum(cantidadRematesSalvados) from Partidos where correoAtleta = 'correo1@algo.com' and temporada = 'Temporada 1')
					)
				)
			)
		)
		as porcentajeRematesSalvadosPorTemporada
	end
	ELSE
	begin
		select 0.00 as porcentajeRematesSalvadosPorTemporada
	end
*/
END
--drop procedure proc_getPorcentajeRematesSalvadosPorTemporada







/*
	IF
		<>
		0
	begin
	end
	ELSE
	begin
		select 0.00 as XXX
	end
*/