## ![Predictor](https://img.shields.io/badge/Predictor-011681?style=for-the-badge&logo=formula1&logoColor=white) <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/logo_app/logo_predictor_nav.png" alt="f1" width="50" style="vertical-align:middle;"/> 
 

### Stack:

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET](https://img.shields.io/badge/ASP.NET-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6DB33F?style=for-the-badge&logo=nuget&logoColor=white)
![ML.NET](https://img.shields.io/badge/ML.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Razor](https://img.shields.io/badge/Razor-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)


## 1. Clonar el Repositorio

```bash
git clone https://github.com/JulianKer/PredictorTP.git
```
Luego abrirlo en Visual Studio.

## 2. Requerimientos:
* Vistual Studio
* C#
* SQLServer

Proyecto: PredictorTP (MVC):
* Microsoft.EntityFrameworkCore.Design
* Microsoft.ML

Proyecto: PredictorTP.Entidades:
* Microsoft.AspNetCore.Mvc.Core
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.ML

Proyecto: PredictorTP.Servicios:
* BCrypt.Net-Next
* MailKit
* Whisper.net
* Whisper.net.Runtime


## 3. Crear la Base de Datos en SQLServer:

Utilizar el script de extensión .sql ubicado en:
```bash
PredictorTP/wwwroot/PredictorBDD.sql
```

## 4. Ejecutar el comando Scaffold:

Ejecutar el comando reemplazando el Server y seleccionando como proyecto predeterminado a "PredictorTP.Entidades":

```bash
Scaffold-DbContext "Server=[REEMPLAZAR];Database=PredictorBDD;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir EF
```

## 5. Ejecutar el proyecto.
<div style="border-left: 4px solid #4CAF50; background: #e7f5e1; padding: 10px; margin: 10px 0;">
  <strong>💡 Tip:</strong> Nosotros lo ejecutamos por http y no por https.
</div>

_____________________________________________________________________



# Preview:

![Rol](https://img.shields.io/badge/Rol-000000?style=for-the-badge&logo=account&logoColor=white)
![Usuario](https://img.shields.io/badge/Usuario-011681?style=for-the-badge&logo=account&logoColor=white)
<img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/logo_app/logo_predictor_nav.png" alt="f1" width="50" style="vertical-align:middle;"/> 


| Sección       | Imagen       |
|-----------------|-----------------|
| Registro | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/1.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Creación de usuario | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/2.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Validación usuario no verificado | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/3.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Verificacción vía Mail | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/4.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Inicio | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/5.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Predicción de Idioma | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/6.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Predicción de Polaridad | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/7.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Predicción de Sentimiento | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/8.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Proceso de imagen (detección de rostros, edad, género y emoción) | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/9.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Perfil | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/10.png" alt="predictor" width="700" style="vertical-align:middle;"/>|

_________________________________________________________________


![Rol](https://img.shields.io/badge/Rol-000000?style=for-the-badge&logo=account&logoColor=white)
![Administrador](https://img.shields.io/badge/Administrador-011681?style=for-the-badge&logo=account&logoColor=white)
<img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/logo_app/logo_predictor_nav.png" alt="f1" width="50" style="vertical-align:middle;"/> 

| Sección       | Imagen       |
|-----------------|-----------------|
| Inicio | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/11_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Gráficos de Auditoría 1 | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/12_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Gráficos de Auditoría 2 | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/13_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Gestión de Usuarios (cambio de Rol, bloquear, desbloquear y eliminar historial de imgs) | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/14_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Entrenamiento de modelos | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/15_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Entrenamiento modelo Idiomas | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/16_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Entrenamiento modelo Polaridad | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/17_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|
| Entrenamiento modelo Sentimientos | <img src="https://github.com/JulianKer/PredictorTP/blob/master/PredictorTP/wwwroot/img/preview/18_admin.png" alt="predictor" width="700" style="vertical-align:middle;"/>|


______________________________________________

![Readme by:](https://img.shields.io/badge/Readme%20by:-011681?style=for-the-badge&logo=account&logoColor=white)
[![JulianKer](https://img.shields.io/badge/JulianKer-000000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/JulianKer)
