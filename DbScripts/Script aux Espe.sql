/*
use master
*/


use LaFabricaDB
go



--select
	--promedio distancia corta
	--promedio distancia larga
	--mejor distancia corta
	--mejor distancia larga
	--mejor salto
	--promedio calificacion de partidos
	--promedio de prueba habilidad
	--promedio de pases exitosos
	--promedio de centros exitosos
	--promedio de calificacion entrenamientos
	--total de victorias

select
	 dbo.func_AVGtiempoPruebaDistanciaCorta('correo1@gmail.com') as promedioDC
	,dbo.func_AVGtiempoPruebaDistanciaLarga('correo1@gmail.com') as promedioDL
	,dbo.func_bestoTiempoPruebaDistanciaCorta('correo1@gmail.com') as mejorDC
	,dbo.func_bestoTiempoPruebaDistanciaLarga('correo1@gmail.com') as mejorDL
	,dbo.func_bestoSalto('correo1@gmail.com') as mejorSalto
	,dbo.func_AVGcalificacionPartidos('correo1@gmail.com') as promedioCP
	,dbo.func_AVGtiempoPruebaHabilidad('correo1@gmail.com') as promedioPH
	,dbo.func_getPorcentajePasesExitosos('correo1@gmail.com') as pPasesC
	,dbo.func_getPorcentajeCentrosExitosos('correo1@gmail.com') as pCentros
	,dbo.func_AVGcalificacionEntrenamientos('correo1@gmail.com') as promCalificacionEntrenamientos
	,dbo.func_getCantJuegosGanados('correo1@gmail.com') as cantG






