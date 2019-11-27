//iniciador de la aplicación y el controlador de angularjs para el empleado en movil 
var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope, $http) {
    $scope.url = "http://tabaslogin.azurewebsites.net/";
    $scope.paises = "";
    $scope.deportes = "";
    $scope.posiciones = "";
    $scope.desactivar = true;
    $scope.semanas = "";
    $http.get("http://localhost:50239/paises").then(function (response) {
        $scope.paises = response.data;
    })
    $http.get("http://localhost:50239/deportes").then(function (response) {
        $scope.deportes = response.data;
    })
    $http({
        method: 'POST',
        url:"http://localhost:50239/posiciones",
        data: {"nombreDeporte": "Futbol"}
    }).then(function successCallback(response){
        $scope.posiciones = response.data;
        console.log(response);
    }, function errorCallback(response){
    });



    //Metodo de get paises
    $scope.getPaises = function(){
        document.location.href="CrearCuentaA.html";
    }

    $scope.selectPais = function(){
        $scope.nombrepais = {"nombrePais":$scope.atleta.pais};
        $http({
            method: 'POST',
            url:"http://localhost:50239/provincias",
            data: $scope.nombrepais
        }).then(function successCallback(response){
            $scope.provincias = response.data;
        }, function errorCallback(response){
        });
        $http({
            method: 'POST',
            url:"http://localhost:50239/universidades",
            data: $scope.nombrepais
        }).then(function successCallback(response){
            $scope.universidades = response.data;
        }, function errorCallback(response){
        });
    }

    $scope.selectPaisU = function(){
        $scope.nombrepais = {"nombrePais":$scope.universidad.nombrePais};
        $http({
            method: 'POST',
            url:"http://localhost:50239/universidades",
            data: $scope.nombrepais
        }).then(function successCallback(response){
            $scope.universidades = response.data;
        }, function errorCallback(response){
        });
    }

    $scope.selectPaisE = function(){
        $scope.nombrepais = {"nombrePais":$scope.entrenador.pais};
        $http({
            method: 'POST',
            url:"http://localhost:50239/provincias",
            data: $scope.nombrepais
        }).then(function successCallback(response){
            $scope.provincias = response.data;
        }, function errorCallback(response){
        });
        $http({
            method: 'POST',
            url:"http://localhost:50239/universidades",
            data: $scope.nombrepais
        }).then(function successCallback(response){
            $scope.universidades = response.data;
        }, function errorCallback(response){
        });
    }

    $scope.getPosiciones = function(){
        $http({
            method: 'POST',
            url:"http://localhost:50239/posiciones",
            data: {"nombreDeporte": $scope.atleta.deporte}
        }).then(function successCallback(response){
            $scope.posiciones = response.data;
            console.log(response);
        }, function errorCallback(response){
        });
    }

    $scope.registrar = function(atleta){
        $scope.atleta = atleta;
        $scope.atleta.fechaNacimiento = $scope.atleta.fechaN.getDate()+"/"+($scope.atleta.fechaN.getMonth()+1)+"/"+$scope.atleta.fechaN.getFullYear();
        $scope.atleta.posicion = $scope.atleta.posicion.idPosicion;
        $scope.atleta.posicionSecundaria = $scope.atleta.posicionSecundaria.idPosicion;
        if($scope.atleta.correo2==null){
            $scope.atleta.correo2="";
        }
        console.log($scope.atleta.fechaN);
        $scope.atleta.foto = "Foto";
        $http({
            method: 'POST',
            url:"http://localhost:50239/crearAtl",
            data: $scope.atleta
        }).then(function successCallback(response){
            localStorage.setItem("id",$scope.atleta.correo)
            document.location.href = "HomePageLA.html";
        }, function errorCallback(response){
        });
    }

    $scope.crearEnt = function(entrenador){
        $scope.entrenador = entrenador;
        $http({
            method: 'POST',
            url:"http://localhost:50239/crearEnt",
            data: $scope.entrenador
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.crearS = function(scout){
        $scope.scout = scout;
        $http({
            method: 'POST',
            url:"http://localhost:50239/crearScout",
            data: $scope.scout
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.crearAd = function(admin){
        $scope.admin = admin;
        $http({
            method: 'POST',
            url:"http://localhost:50239/crearAdmin",
            data: $scope.admin
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.getPlan = function(){
        $scope.id = {"correo": localStorage.getItem("id"),"semana":$scope.semana};
        $http({
            method: 'POST',
            url:"http://localhost:50239/planesEmailSem",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.plan = response.data;
        }, function errorCallback(response){
        });
    }


    $scope.ingresar = function(client){
        $scope.client = client;
        $http({
            method: 'POST',
            url:"http://localhost:50239/login",
            data: $scope.client
        }).then(function successCallback(response){
            $scope.rol = response.data;
            localStorage.setItem("id",$scope.client.correo)
            $scope.verificarRol(response.data.rol);
        }, function errorCallback(response){
        });

        $scope.verificarRol = function(rol){
            if($scope.rol.rol == "Atl"){
                $scope.id = {"correo": localStorage.getItem("id")};
                $http({
                    method: 'POST',
                    url:"http://localhost:50239/semanasPlanes",
                    data: $scope.id
                }).then(function successCallback(response){
                    $scope.semanas = response.data;
                    localStorage.setItem("semanas",$scope.semanas.semanas);
                    document.location.href = "HomePageLA.html";
                }, function errorCallback(response){
                });
            }else if($scope.rol.rol == "Ent"){
                document.location.href = "HomePageLE.html";
            }else if($scope.rol.rol == "Sct"){
                document.location.href = "HomePageLS.html";
            }else if($scope.rol.rol =="Admn"){
                document.location.href = "HomePageLAd.html";
            }else{
                alert("Usuario no encontrado");
            }
        }
    }

    $scope.getSemanas = function(){
        $scope.id = {"correo": localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/semanasPlanes",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.semanas = response.data;
            //document.location.href = "PlanPersonalizado.html";
        }, function errorCallback(response){
        });
    }

    $scope.getClientData = function(){
            $scope.id = {"correo": localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/atletacorreo",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.client = response.data;
            //document.location.href = "PlanPersonalizado.html";
        }, function errorCallback(response){
        });
    }

    $scope.getEquipos = function(){
        $scope.id = {"correo": localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/equipos",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.equipos = response.data;
        }, function errorCallback(response){
        });
    }

    $scope.getEquiposxTemporada = function(){
        $scope.id = {"correo": localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/equiposTemporadas",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.equipos = response.data;
        }, function errorCallback(response){
        });
    }

    $scope.planpag = function(equipo){
        localStorage.setItem("equipo",equipo);
        document.location.href = "CrearPlanP.html"
    }

    $scope.getAtletas = function(){
        $scope.id = {"nombreEquipo": localStorage.getItem("equipo")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/jugadoresEquip",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.atletas = response.data;
        }, function errorCallback(response){
        });
    }
    $scope.getEjercicios = function(){
        $http.get("http://localhost:50239/ejercicios").then(function (response) {
            $scope.ejercicios = response.data;
        })
    }

    $scope.siguiente = function(x){
        localStorage.setItem("correoA",x.correo);
        document.location.href = "SigPP.html";
    }


    $scope.guardar = function(plan){
        $scope.plan = plan;
        $scope.plan.correo = localStorage.getItem("correoA");
        $scope.plan.idEjercicio = $scope.plan.idEjercicio.idEjercicio;
        $http({
            method: 'POST',
            url:"http://localhost:50239/asignarPP",
            data: $scope.plan
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.crearEVA = function(equipo){
        localStorage.setItem("equipo",equipo);
        document.location.href = "crearEVA.html";
    }

    $scope.crearEVAP = function(equipo,temporada){
        localStorage.setItem("equipo",equipo);
        localStorage.setItem("temporada",temporada);
        document.location.href = "crearEVAP.html";
    }

    $scope.crearEvaluacionA = function(atleta){
        localStorage.setItem("correo",atleta.correo);
        document.location.href = "CrearEvaluacionA.html";
    }

    $scope.crearEvaluacionAP = function(atleta){
        localStorage.setItem("correo",atleta.correo);
        document.location.href = "CrearEvaluacionAP.html";
    }

    $scope.agregarEv = function(evaluacion){
        $scope.evaluacion = evaluacion;
        $scope.evaluacion.correo = localStorage.getItem("correo");
        $http({
            method: 'POST',
            url:"http://localhost:50239/evaluarAtlEntr",
            data: $scope.evaluacion
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.agregarEvP = function(evaluacion){
        $scope.evaluacion = evaluacion;
        $scope.evaluacion.correo = localStorage.getItem("correo");
        $scope.evaluacion.temporada = localStorage.getItem("temporada");
        $http({
            method: 'POST',
            url:"http://localhost:50239/evaluarAtlPart",
            data: $scope.evaluacion
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.agregarEq = function(equipo){
        $scope.equipo = equipo;
        localStorage.setItem("equipo",$scope.equipo.nombreEquipo);
        $scope.equipo.correoEntrenador = localStorage.getItem("id");
        $http({
            method: 'POST',
            url:"http://localhost:50239/crearEquip",
            data: $scope.equipo
        }).then(function successCallback(response){
            document.location.href = "CrearEquipoA.html"
        }, function errorCallback(response){
        });
    }

    $scope.getTemporadas = function(){
        $http.get("http://localhost:50239/temporadas").then(function (response) {
            $scope.temporadas = response.data;
        })
    }

    $scope.entrenadorInfo = function(){
        $scope.id = {"correo":localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/infoEntrenador",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.client = response.data;
        }, function errorCallback(response){
        });
    }

    $scope.getAtletasUni = function(){
        $scope.id = {"correo":localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/infoEntrenador",
            data: $scope.id
        }).then(function successCallback(response){
            $scope.universidad = response.data;
            $scope.universidad = {"universidad":$scope.universidad.universidad};
            $http({
                method: 'POST',
                url:"http://localhost:50239/atletasXuniversidad",
                data: $scope.universidad
            }).then(function successCallback(response){
                $scope.atletas = response.data;
            }, function errorCallback(response){
            });
        }, function errorCallback(response){
        });
    }

    $scope.agregarAtletasEquipo = function(atleta){
        $scope.id = localStorage.getItem("equipo");
        $scope.correo = atleta.correo;
        $scope.data = {"nombreEquipo":$scope.id,"correo":$scope.correo};
        $http({
            method: 'POST',
            url:"http://localhost:50239/insertEquip",
            data: $scope.data
        }).then(function successCallback(response){
        }, function errorCallback(response){
        })
    }

    $scope.getEquipoCreado = function(){
        $scope.nombre = {"nombreEquipo": localStorage.getItem("equipo")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/jugadoresEquip",
            data: $scope.nombre
        }).then(function successCallback(response){
            $scope.atletas = response.data;
        }, function errorCallback(response){
        })
    }

    $scope.lesionE = function(equipo){
        localStorage.setItem("equipo",equipo);
        document.location.href = "CrearLesionA.html";
    }

    $scope.crearLesionA = function(atleta){
        localStorage.setItem("atleta",atleta);
        document.location.href = "CrearLesionAT.html";
    }

    $scope.agregarLesion = function(lesion){
        $scope.lesion = lesion;
        $scope.lesion.gravedad = $scope.lesion.gravedad.idTipoLesion;
        $scope.lesion.fechaInicio = $scope.lesion.fechaInicio.getDate()+"/"+($scope.lesion.fechaInicio.getMonth()+1)+"/"+$scope.lesion.fechaInicio.getFullYear();
        $scope.lesion.fechaFinal = $scope.lesion.fechaFinal.getDate()+"/"+($scope.lesion.fechaFinal.getMonth()+1)+"/"+$scope.lesion.fechaFinal.getFullYear();
        $scope.lesion.correo = localStorage.getItem("correo");
        $http({
            method: 'POST',
            url:"http://localhost:50239/crearLesion",
            data: $scope.lesion
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.getLesiones = function(){
        $http.get("http://localhost:50239/estadoLesion").then(function (response) {
            $scope.lesiones = response.data;
        })
    }

    $scope.Eliminar = function(){
        $scope.agregar = false;
        $scope.eliminar = true;
    }

    $scope.Agregar = function(){
        $scope.eliminar = false;
        $scope.agregar = true;
    }

    $scope.eliminarU = function(universidad){
        $scope.universidad = universidad;
        $http({
            method: 'POST',
            url:"http://localhost:50239/delUniversidad",
            data: $scope.universidad
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.AgregarU = function(universidad){
        $scope.universidad = universidad;
        $http({
            method: 'POST',
            url:"http://localhost:50239/nuevaUniversidad",
            data: $scope.universidad
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.AgregarP = function(pais){
        $scope.pais = pais;
        $http({
            method: 'POST',
            url:"http://localhost:50239/nuevoPais",
            data: $scope.pais
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }
    $scope.eliminarP = function(pais){
        $scope.pais = pais;
        $http({
            method: 'POST',
            url:"http://localhost:50239/delPais",
            data: $scope.pais
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }
    $scope.AgregarPo = function(posicion){
        $scope.posicion = posicion;
        $http({
            method: 'POST',
            url:"http://localhost:50239/nuevaPosicion",
            data: $scope.posicion
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }
    $scope.eliminarPo = function(posicion){
        $scope.posicion = posicion;
        $scope.posicion.nombrePosicion = $scope.posicion.nombrePosicion.nombrePosicion;
        $http({
            method: 'POST',
            url:"http://localhost:50239/delPosicion",
            data: $scope.posicion
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }
    
    $scope.AgregarD = function(deporte){
        $scope.deporte = deporte;
        $http({
            method: 'POST',
            url:"http://localhost:50239/nuevoDeporte",
            data: $scope.deporte
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }
    $scope.eliminarD = function(posicion){
        $scope.posicion = posicion;
        $http({
            method: 'POST',
            url:"http://localhost:50239/delDeporte",
            data: $scope.posicion
        }).then(function successCallback(response){
        }, function errorCallback(response){
        });
    }

    $scope.filtrar = function(filtros){
        if(filtros.nombrePais != null && filtros.nombreUniversidad!=null){
            $scope.filtro = {"filtros":[{"filtro":"pais","valor":filtros.nombrePais},{"filtro":"universidad","valor":filtros.nombreUniversidad},{"filtro":"peso","valor":filtros.peso},{"filtro":"altura","valor":filtros.altura},{"filtro":"posicionSecundaria","valor":filtros.nombrePosicion.nombrePosicion}]};
            $scope.filtrarAux($scope.filtro);
        }else if(filtros.nombrePais!=null){
            $scope.filtro = {"filtros":[{"filtro":"pais","valor":filtros.nombrePais}]}
            $scope.filtrarAux($scope.filtro);
        }else if(filtros.nombreUniversidad!=null){
            $scope.filtro = {"filtros":[{"filtro":"universidad","valor":filtros.nombreUniversidad}]}
            $scope.filtrarAux($scope.filtro);
        }else if(filtros.altura!=null){
            $scope.filtro = {"filtros":[{"filtro":"altura","valor":filtros.altura}]}
            $scope.filtrarAux($scope.filtro);
        }else if(filtros.peso!=null){
            $scope.filtro = {"filtros":[{"peso":"universidad","valor":filtros.peso}]}
            $scope.filtrarAux($scope.filtro);
        }else if(filtros.nombrePosicion!=null){
            $scope.filtro = {"filtros":[{"filtro":"posicionSecundaria","valor":filtros.nombrePosicion}]}
            $scope.filtrarAux($scope.filtro);
        }else{
            $scope.filtro = {"filtros":[{"filtro":"xsport","valor":filtros.xsport}]}
            $scope.filtrarAux($scope.filtro);
        }
    }

    $scope.filtrarAux = function(filtro){
        $scope.filtro = filtro;
        $http({
            method: 'POST',
            url:"http://localhost:50239/reporteRango",
            data: $scope.filtro
        }).then(function successCallback(response){
            $scope.atletas = response.data;
        }, function errorCallback(response){
        });
    }

    $scope.desactivar = function(){
        $scope.client = {"correo":localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/desactivarCuenta",
            data: $scope.client
        }).then(function successCallback(response){
            $scope.activar = true;
            $scope.desactivar=false;
        }, function errorCallback(response){
        });
    }

    $scope.activar = function(){
        $scope.client = {"correo":localStorage.getItem("id")};
        $http({
            method: 'POST',
            url:"http://localhost:50239/activarCuenta",
            data: $scope.client
        }).then(function successCallback(response){
            $scope.activar = false;
            $scope.desactivar=true;
        }, function errorCallback(response){
        });
    }

});