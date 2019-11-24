/*
use master


DBCC USEROPTIONS;
SET DATEFORMAT dmy;


SELECT CONVERT(varchar(max), HASHBYTES ('SHA2_512', 'clave') ,2)
*/


use LaFabricaDB
go


insert into Paises(nombre) values('Costa Rica')


insert into Provincias(nombreProvincia, nombrePais) values('San Jose', 'Costa Rica')
insert into Provincias(nombreProvincia, nombrePais) values('Alajuela', 'Costa Rica')
insert into Provincias(nombreProvincia, nombrePais) values('Cartago', 'Costa Rica')
insert into Provincias(nombreProvincia, nombrePais) values('Heredia', 'Costa Rica')
insert into Provincias(nombreProvincia, nombrePais) values('Guanacaste', 'Costa Rica')
insert into Provincias(nombreProvincia, nombrePais) values('Puntarenas', 'Costa Rica')
insert into Provincias(nombreProvincia, nombrePais) values('Limon', 'Costa Rica')


insert into Universidades(nombreUniversidad,nombrePais) values('Instituto Tecnologico de Costa Rica', 'Costa Rica')
insert into Universidades(nombreUniversidad,nombrePais) values('Universidad de Costa Rica', 'Costa Rica')
insert into Universidades(nombreUniversidad,nombrePais) values('Universidad Nacional', 'Costa Rica')


insert into Deportes(nombreDeporte) values('Futbol')


insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',0, 'Defensor central')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',1, 'Defensor lateral')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',2, 'Defensor libre por la banda')
--insert into Posiciones(nombreDeporte,nombrePosicion) values('Futbol', 'Defensor de Medio Campo')--En desuso

insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',3, 'Mediocampista defensivo')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',4, 'Mediocampista central')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',5, 'Mediocampista externo')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',6, 'Mediocampista ofensivo')

insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',7, 'Lateral volante')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',8, 'Volante de contencion')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',9, 'Volante de corte')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',10, 'Volante de salida')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',11, 'Volante externo')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',12, 'Volante mixto')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',13, 'Volante de enlace')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',14, 'Volante de creacion')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',15, 'Volante de llegada')

insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',16, 'Media punta')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',17, 'Segundo delantero')
--insert into Posiciones(nombreDeporte,nombrePosicion) values('Futbol', 'Centro punta')--en desuso
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',18, 'Puntero')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',19, 'Extremo')
insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',20, 'Delantero Centro')

insert into Posiciones(nombreDeporte,idPosicion,nombrePosicion) values('Futbol',21, 'Portero')


EXEC proc_registrarEntrenador
	 @nombre = 'Steve'
	,@apellido = 'Steve otra vez'
	,@correo = 'bbbb'
	,@password = '456'
	,@pais = 'Costa Rica'
	,@universidad = 'Instituto Tecnologico de Costa Rica'

EXEC proc_registrarEntrenador
	 @nombre = 'Sergio'
	,@apellido = 'Ramirez'
	,@correo = 'aaaa'
	,@password = '123'
	,@pais = 'Costa Rica'
	,@universidad = 'Universidad Nacional'

EXEC proc_registrarEntrenador
	 @nombre = 'Kawo'
	,@apellido = 'Ikari'
	,@correo = 'cccc'
	,@password = '789'
	,@pais = 'Costa Rica'
	,@universidad = 'Instituto Tecnologico de Costa Rica'


insert into Temporadas(nombreTemporada) values('Temporada 1')
insert into Temporadas(nombreTemporada) values('Temporada 2')
insert into Temporadas(nombreTemporada) values('Temporada 3')
insert into Temporadas(nombreTemporada) values('Temporada 4')
insert into Temporadas(nombreTemporada) values('Temporada 5')
insert into Temporadas(nombreTemporada) values('Temporada 6')
insert into Temporadas(nombreTemporada) values('Temporada 7')
insert into Temporadas(nombreTemporada) values('Temporada 8')
insert into Temporadas(nombreTemporada) values('Temporada 9')

insert into Equipos(nombreEquipo,correoEntrenador) values('Equipo 1', 'aaaa')
insert into Equipos(nombreEquipo,correoEntrenador) values('Equipo 2', 'aaaa')
insert into Equipos(nombreEquipo,correoEntrenador) values('Equipo 3', 'aaaa')
insert into Equipos(nombreEquipo,correoEntrenador) values('Equipo 4', 'bbbb')
insert into Equipos(nombreEquipo,correoEntrenador) values('Equipo 5', 'bbbb')
insert into Equipos(nombreEquipo,correoEntrenador) values('Equipo 6', 'cccc')
insert into Equipos(nombreEquipo,correoEntrenador) values('Equipo 7', 'cccc')


insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('Equipo 1','Temporada 1')
insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('Equipo 1','Temporada 2')
insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('Equipo 1','Temporada 3')
insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('Equipo 2','Temporada 2')
insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('Equipo 3','Temporada 1')
insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('Equipo 3','Temporada 2')
insert into EquiposTemporadas(nombreEquipo,nombreTemporada) values('Equipo 3','Temporada 3')



EXEC proc_registrarAtleta
	 @nombre = 'Atleta A'
	,@apellido = 'Apellido A'
	,@cedula = 111111111
	,@provincia = 'Cartago'
	,@fechaNacimiento = '01/01/2000'
	,@correo1 = 'correo1@algo.com'
	,@correo2 = null
	,@telefono = 11112222
	,@foto = 'Foto1'
	,@pais = 'Costa Rica'
	,@universidad = 'Instituto Tecnologico de Costa Rica'
	,@password = 'password1'
	,@deporte = 'Futbol'
	,@altura = 1.8
	,@peso = 70
	,@posicion = 0
	,@posicionSecundaria = 1

EXEC proc_registrarAtleta
	 @nombre = 'Atleta B'
	,@apellido = 'Apellido B'
	,@cedula = 111111112
	,@provincia = 'Cartago'
	,@fechaNacimiento = '02/01/2000'
	,@correo1 = 'correo2@algo.com'
	,@correo2 = null
	,@telefono = 22223333
	,@foto = 'Foto2'
	,@pais = 'Costa Rica'
	,@universidad = 'Instituto Tecnologico de Costa Rica'
	,@password = 'password2'
	,@deporte = 'Futbol'
	,@altura = 1.7
	,@peso = 60
	,@posicion = 1
	,@posicionSecundaria = 2

EXEC proc_registrarAtleta
	 @nombre = 'Atleta C'
	,@apellido = 'Apellido C'
	,@cedula = 111111113
	,@provincia = 'Cartago'
	,@fechaNacimiento = '03/01/2000'
	,@correo1 = 'correo3@algo.com'
	,@correo2 = null
	,@telefono = 33334444
	,@foto = 'Foto3'
	,@pais = 'Costa Rica'
	,@universidad = 'Instituto Tecnologico de Costa Rica'
	,@password = 'password3'
	,@deporte = 'Futbol'
	,@altura = 1.75
	,@peso = 75
	,@posicion = 2
	,@posicionSecundaria = 3

EXEC proc_registrarAtleta
	 @nombre = 'Atleta D'
	,@apellido = 'Apellido D'
	,@cedula = 111111114
	,@provincia = 'Cartago'
	,@fechaNacimiento = '04/01/2000'
	,@correo1 = 'correo4@algo.com'
	,@correo2 = null
	,@telefono = 44445555
	,@foto = 'Foto4'
	,@pais = 'Costa Rica'
	,@universidad = 'Instituto Tecnologico de Costa Rica'
	,@password = 'password4'
	,@deporte = 'Futbol'
	,@altura = 1.85
	,@peso = 90
	,@posicion = 3
	,@posicionSecundaria = 4


insert into TiposLesiones(idTipoLesion,tipoLesion) values(0,'Leve')
insert into TiposLesiones(idTipoLesion,tipoLesion) values(1,'medio fea')
insert into TiposLesiones(idTipoLesion,tipoLesion) values(2,'Fea')
insert into TiposLesiones(idTipoLesion,tipoLesion) values(3,'Muy fea')
insert into TiposLesiones(idTipoLesion,tipoLesion) values(4,'F')


insert into Lesiones(correoAtleta,fechaInicio,fechaFinal,descripcion,idTipoLesion,lugar) values('correo1@algo.com', '01/01/2001','01/01/2002','Explosion de Creeper',3,'En donde no ewe')
insert into Lesiones(correoAtleta,fechaInicio,fechaFinal,descripcion,idTipoLesion,lugar) values('correo1@algo.com', '01/01/2002','01/01/2003','DORIYA',0,'En casi todo')
insert into Lesiones(correoAtleta,fechaInicio,fechaFinal,descripcion,idTipoLesion,lugar) values('correo1@algo.com', '01/01/2003','01/01/2004','Masenko',0,'En el sitio')
insert into Lesiones(correoAtleta,fechaInicio,fechaFinal,descripcion,idTipoLesion,lugar) values('correo1@algo.com', '01/01/2004','01/01/2005','Hiya',4,'Ahi')


insert into EstadosDePartido(idEstado,estado) values(0,'Perdido')
insert into EstadosDePartido(idEstado,estado) values(1,'Empatado')
insert into EstadosDePartido(idEstado,estado) values(2,'Ganado')
insert into EstadosDePartido(idEstado,estado) values(3,'EZ')


insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(0,'correo1@algo.com','Temporada 1',60,2, 3,4,1,	10,7,6,5,	0,1,	0,0,0)
insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(1,'correo1@algo.com','Temporada 4',50,1, 1,5,2,	11,6,11,8,	1,0,	0,0,0)
insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(2,'correo2@algo.com','Temporada 5',40,2, 2,6,1,	12,5,12,3,	0,1,	0,0,0)
insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(3,'correo2@algo.com','Temporada 6',30,0, 0,5,2,	13,4,7,1,	1,0,	0,0,0)
insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(4,'correo3@algo.com','Temporada 7',20,2, 2,4,3,	14,3,45,9,	0,1,	0,0,0)

insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(5,'correo1@algo.com','Temporada 1',13,0, 1,5,3,	15,2,13,12,	1,1,	0,0,0)
insert into Partidos(idPartido,correoAtleta,temporada,calificacionPartido,idEstado,cantidadGoles,cantidadAsistencias,balonesRecuperados,cantidadPasesFallidos,cantidadPasesExitosos,cantidadCentrosFallidos,cantidadCentrosExitosos,cantidadTarjetasAmarillas,cantidadTarjetasRojas,cantidadPenales,cantidadRematesSalvados,cantidadGolesRecibidos) values(6,'correo1@algo.com','Temporada 2',13,2, 2,6,4,	16,1,90,45,	0,0,	0,0,0)


insert into Roles(idRol,nombreRol) values(0,'Administrador')
insert into Roles(idRol,nombreRol) values(1,'Scout')


exec proc_registrarTrabajador
	 @nombre = 'Admin 1'
	,@apellido = 'Apellido de Admin 1'
	,@correo = 'admin1@algo.com'
	,@password = 'passwordAdmin1'
	,@rol = 0

exec proc_registrarTrabajador
	 @nombre = 'Admin 2'
	,@apellido = 'Apellido de Admin 2'
	,@correo = 'admin2@algo.com'
	,@password = 'passwordAdmin2'
	,@rol = 0

exec proc_registrarTrabajador
	 @nombre = 'Scout 1'
	,@apellido = 'Apellido de Scout 1'
	,@correo = 'scout1@algo.com'
	,@password = 'passwordScout1'
	,@rol = 1

exec proc_registrarTrabajador
	 @nombre = 'Scout 2'
	,@apellido = 'Apellido de Scout 2'
	,@correo = 'scout2@algo.com'
	,@password = 'passwordScout2'
	,@rol = 1


insert into Ejercicios(nombreEjercicio) values('Tiro con Chanfle')
insert into Ejercicios(nombreEjercicio) values('Nombre de ejercicio comun de gimnasio 1')
insert into Ejercicios(nombreEjercicio) values('Nombre de ejercicio comun de gimnasio 2')
insert into Ejercicios(nombreEjercicio) values('Nombre de ejercicio comun de gimnasio 3')
insert into Ejercicios(nombreEjercicio) values('Nombre de ejercicio comun de gimnasio 4')
insert into Ejercicios(nombreEjercicio) values('Nombre de ejercicio comun de gimnasio 5')


insert into Planes(semana,correoAtleta) values(5,'correo1@algo.com')
insert into Planes(semana,correoAtleta) values(1,'correo1@algo.com')
insert into Planes(semana,correoAtleta) values(2,'correo2@algo.com')
insert into Planes(semana,correoAtleta) values(3,'correo2@algo.com')
insert into Planes(semana,correoAtleta) values(4,'correo2@algo.com')


insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(1,'correo1@algo.com',1,0,1000)
insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(1,'correo1@algo.com',2,1,100)
insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(2,'correo2@algo.com',3,2,10)
insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(3,'correo2@algo.com',4,1,1)
insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(3,'correo2@algo.com',5,2,10)
insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(4,'correo2@algo.com',6,3,100)
insert into PlanesEjercicios(semana,correoAtleta,dia,idEjercicio,cantidad) values(5,'correo1@algo.com',7,4,1000)


insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(0,'correo1@algo.com',50,90,91,2,8,15,23)
insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(1,'correo4@algo.com',89,45,90,6,9,89,90)
insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(2,'correo3@algo.com',50,90,91,2,8,15,23)
insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(3,'correo4@algo.com',50,90,91,2,8,15,23)
insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(4,'correo4@algo.com',50,90,91,2,8,15,23)
insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(5,'correo3@algo.com',50,90,91,2,8,15,23)

insert into Entrenamientos(idEntrenamiento,correoAtleta,calificacionEntrenamiento,tiempoPruebaDistanciaCorta,tiempoPruebaDistanciaLarga,salto,tiempoPruebaHabilidad,pruebaFisicaPace,pruebaFisicaHR) values(6,'correo4@algo.com',50,12,91,2,8,15,23)





/*
exec proc_AVGcalificacionEntrenamientos @correoAtleta = 'correo4@algo.com'


*/




/*
delete PlanesEjercicios


select * from Paises
select * from Provincias
select * from Universidades
select * from Deportes
select * from Posiciones
select * from Entrenadores
select * from Temporadas
select * from Equipos
select * from Atletas
select * from TiposLesiones
select * from Lesiones
select * from EstadosDePartido
select * from Partidos
select * from Roles
select * from Trabajadores
select * from Ejercicios
select * from Planes
select * from PlanesEjercicios
select * from Entrenamientos


delete Atletas
delete Universidades
*/




