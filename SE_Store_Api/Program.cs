using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SE_Store_Api.Filters;
using SE_Store_Model.EF;
using SE_Store_Model.Mapper;
using SE_Store_Model.Repository;
using SE_Store_Model.Repository.Interface;
using SE_Store_Service;
using SE_Store_Service.Interface;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string corsOriginSpecific = "_myAllowSpecificOrigins";

builder.Services.AddCors(p => p.AddPolicy(corsOriginSpecific, builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));


// setting config
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build();

// log  config
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(configuration)
  .Enrich.WithCorrelationIdHeader("X-TraceId")
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddDbContext<StoreContext>(o => {
    o.UseMySQL(builder.Configuration.GetConnectionString("store"));
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();
builder.Services.AddScoped<IGcpService, GcpService>();
builder.Services.AddScoped<ITipoService, TipoService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IGrupoProductoService, GrupoProductoService>();
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IClasificacionTipoService, ClasificacionTipoService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<ISeguridadService, SeguridadService>();
builder.Services.AddScoped<IJwtService, JwtService> ();
builder.Services.AddScoped<IFuncionalidadService, FuncionalidadService>();


builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IDocumentoRepository, DocumentoRepository>();
builder.Services.AddScoped<IParametroRepository, ParametroRepository>();
builder.Services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();
builder.Services.AddScoped<ITipoRepository, TipoRepository>();
builder.Services.AddScoped<IGrupoProductoRepository, GrupoProductoRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IDireccionRepository, DireccionRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IClasificacionTipoRepository, ClasificacionTipoRepository>();
builder.Services.AddScoped<IOrdenProductoRepository, OrdenProductoRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddScoped<IFuncionalidadRepository, FuncionalidadRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Disable default response's  case format  JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});


builder.Services.AddAutoMapper(typeof(EstadoProfile));
builder.Services.AddAutoMapper(typeof(EntidadProfile));
builder.Services.AddAutoMapper(typeof(ClienteProfile));
builder.Services.AddAutoMapper(typeof(RegionProfile));
builder.Services.AddAutoMapper(typeof(DocumentoProfile));
builder.Services.AddAutoMapper(typeof(TipoDocumentoProfile));
builder.Services.AddAutoMapper(typeof(GrupoProductoProfile));
builder.Services.AddAutoMapper(typeof(ProductoProfile));
builder.Services.AddAutoMapper(typeof(TipoProfile));
builder.Services.AddAutoMapper(typeof(ProvinciaProfile));
builder.Services.AddAutoMapper(typeof(DistritoProfile));
builder.Services.AddAutoMapper(typeof(OrdenProfile));
builder.Services.AddAutoMapper(typeof(OrdenProductoProfile));
builder.Services.AddAutoMapper(typeof(UsuarioProfile));
builder.Services.AddAutoMapper(typeof(RolProfile));
builder.Services.AddAutoMapper(typeof(FuncionalidadProfile));
builder.Services.AddAutoMapper(typeof(ModuloProfile));

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JWT.AUDIENCE.TOKEN"],
        ValidIssuer = builder.Configuration["JWT.ISSUER.TOKEN"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT.SECRET.KEY"]))
    };
});

// Configuration Serilog TraceId
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("PropagateHeaders").AddHeaderPropagation();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("X-TraceId"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsOriginSpecific);

app.UseAuthorization();

app.MapControllers();

app.Run();

