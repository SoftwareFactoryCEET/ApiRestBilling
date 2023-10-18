<!DOCTYPE html>
<html>

<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
<!--  <title>Welcome file</title> -->
  <link rel="stylesheet" href="https://stackedit.io/style.css" />
</head>

<body class="stackedit">
  <div class="stackedit__html"><h3 id="instalar-paquetes-necesarios-para-trabajar-con-entityframework">Instalar paquetes necesarios para trabajar con EntityFramework</h3>
<pre><code>a) Microsoft.EntityFrameworkCore
b) Microsoft.EntityFrameworkCore.SqlServer
c) Microsoft.EntityFrameworkCore.Tools
d) Microsoft.EntityFrameworkCore.Design
</code></pre>
<h3 id="crear-un-secreto-para-la-cadena-de-conexión-a-la-db">Crear un secreto para la cadena de conexión a la DB</h3>
<pre><code>a) dotnet user-secrets init
b) dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=.; database=BillingAPIDB; Integrated Security=true; TrustServerCertificate=true;MultipleActiveResultSets=true"
</code></pre>
<h3 id="cadena-de-conexión">Cadena de conexión</h3>
<pre><code>a) "ConnectionStrings:DefaultConnection" "server=.; database=BillingAPIDB; Integrated Security=true; TrustServerCertificate=true;MultipleActiveResultSets=true"
</code></pre>
<h3 id="configurar-la-cadena-de-conexión">Configurar la cadena de conexión</h3>
<p>Class Program:</p>
<pre><code>a) Cadena de Conexión
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

b) Servicio 
builder.Services.AddDbContext&lt;ApplicationDbContext&gt;(options =&gt;
options.UseSqlServer(connectionString));

C) ApplicationDbContext (Mapeamos las clases modelo).
</code></pre>
<h3 id="creo-los-modelos">Creo los modelos</h3>
<pre><code>a) Validar los modelos
</code></pre>
<h3 id="migraciones-entity-framework-command-line">Migraciones Entity Framework (Command Line)</h3>
<pre><code>a) Add-Migration initial-models-wihtoutvalidatios
</code></pre>
<h3 id="actualizamos-la-base-de-datos-con-entity-framework-command-line">Actualizamos la Base de Datos con Entity Framework (Command Line)</h3>
<pre><code>a) Update-Database -Migration initial-models-wihtoutvalidatios
</code></pre>
<h3 id="crear-un-controlador-para-una-api-rest">Crear un controlador para una API REST</h3>
<h3 id="probar-api-rest">Probar API REST</h3>
<h3 id="publicar-api-rest-en-ms-azure">Publicar API REST en MS Azure</h3>
<h3 id="comandos-secrets">Comandos (Secrets):</h3>
<pre><code>a) dotnet user-secrets init
b)	dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=.; database=BillingAPIDB; Integrated Security=true; TrustServerCertificate=true;MultipleActiveResultSets=true"
</code></pre>
</div>
</body>

</html>
