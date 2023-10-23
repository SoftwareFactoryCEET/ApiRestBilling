<!DOCTYPE html>
<html>

<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <!-- <title>Welcome file</title>-->
  <link rel="stylesheet" href="https://stackedit.io/style.css" />
</head>

<body class="stackedit">
  <div class="stackedit__html"><h3 id="instalar-paquetes-necesarios-para-trabajar-con-entityframework">Instalar paquetes necesarios para trabajar con EntityFramework</h3>
<pre><code>a) Microsoft.EntityFrameworkCore
b) Microsoft.EntityFrameworkCore.SqlServer
c) Microsoft.EntityFrameworkCore.Tools
d) Microsoft.EntityFrameworkCore.Design
</code></pre>
<h3 id="cadena-de-conexión">Cadena de conexión</h3>
<pre><code>a) "ConnectionStrings:DefaultConnection" "server=.; database=BillingAPIDB; Integrated Security=true; TrustServerCertificate=true;MultipleActiveResultSets=true"
</code></pre>
<h3 id="crear-un-secreto-para-la-cadena-de-conexión-a-la-db">Crear un secreto para la cadena de conexión a la DB</h3>
<pre><code>a) dotnet user-secrets init
b) dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=.; database=BillingAPIDB; Integrated Security=true; TrustServerCertificate=true;MultipleActiveResultSets=true"
</code></pre>
<h3 id="registrar-la-cadena-de-conexión">Registrar la cadena de conexión</h3>
<h4 id="class-program">Class Program:</h4>
<pre><code>1) Cadena de Conexión:
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

2) Servicio: 
builder.Services.AddDbContext&lt;ApplicationDbContext&gt;(options =&gt;
options.UseSqlServer(connectionString));
</code></pre>
<h3 id="crear-clases-modelo">Crear clases modelo</h3>
<pre><code>a) Crear las clases modelo
b) Validar los modelos
</code></pre>
<h3 id="applicationdbcontext-mapear-las-clases-modelo.">ApplicationDbContext (Mapear las clases modelo).</h3>
<pre><code>public DbSet&lt;Supplier&gt; Suppliers { get; set; }
public DbSet&lt;Customer&gt; Customers { get; set; }
public DbSet&lt;Order&gt; Orders { get; set; }
public DbSet&lt;OrderItem&gt; OrderItems { get; set; }
public DbSet&lt;Product&gt; Products { get; set; }
</code></pre>
<h3 id="migraciones-entity-framework-command-line">Migraciones Entity Framework (Command Line)</h3>
<pre><code>a) Add-Migration initial-models-wihtoutvalidatios
</code></pre>
<h3 id="actualizar-la-base-de-datos-con-entity-framework-command-line">Actualizar la Base de Datos con Entity Framework (Command Line)</h3>
<pre><code>a) Update-Database -Migration initial-models-wihtoutvalidatios
</code></pre>
<h3 id="crear-un-controlador-para-una-api-rest">Crear un controlador para una API REST</h3>
<pre><code>a) GET
b) POST
c) PUT
d) PACH
e) DELETE
</code></pre>
<h3 id="probar-api-rest-ambiente-local">Probar API REST (Ambiente local)</h3>
<pre><code>a) Swagger
</code></pre>
<h3 id="publicar-api-rest-en-ms-azure-cloud">Publicar API REST en MS Azure (Cloud)</h3>
<pre><code>	a) Grupo de recursos.
	b) Servidor de bases de datos MS SQL
	c) Base de Datos MS SQL.
	d) App Services.
	e) Telemetría.
</code></pre>
</div>
</body>

</html>