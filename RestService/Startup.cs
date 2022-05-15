using BusinessLogicLayer;
using BusinessLogicLayer.LogicMain.Managers.Print;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Interfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Print;
using BusinessLogicLayer.LogicMain.Presenters.Repositories.Classes.ConcreteDefinitions;
using DataAccessLayerCore;
using DataAccessLayerCore.DataAccessClasses.Repositories.ConcreteDefinitions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace RestService
{
	public class Startup
	{
		public Startup()
		{
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

			services.AddControllers();

			services.AddScoped<IDetailRelationRepositoryPresenter>(printPresenter =>
				new DetailRelationRepositoryPresenter(
					new DetailRelationRepositoryManager(
						new DetailRelationRepository(
							new DbContextOptionsBuilder<MainContext>().
								UseSqlServer(new ConnectInfoDataAccess("localhost", "Kuznetsov", "msroot", "msroot", 1433).ConnectionString).Options))));

			services.AddScoped<IDetailRelationRepositoryManager>(detailRelationRepositoryManager =>
				new DetailRelationRepositoryManager(
					new DetailRelationRepository(
						new DbContextOptionsBuilder<MainContext>().
							UseSqlServer(new ConnectInfoDataAccess("localhost", "Kuznetsov", "msroot", "msroot", 1433).ConnectionString).Options)));

			services.AddScoped<IPrintPresenter>(printPresenter =>
				new PrintPresenter(
					new PrintManager(
						new DetailRelationRepository(
							new DbContextOptionsBuilder<MainContext>().
								UseSqlServer(new ConnectInfoDataAccess("localhost", "Kuznetsov", "msroot", "msroot", 1433).ConnectionString).Options))));

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = true;

					options.TokenValidationParameters = new TokenValidationParameters
					{
						// Укзывает, будет ли валидироваться издатель при валидации токена.
						ValidateIssuer = true,
						// Строка, представляющая издателя.
						ValidIssuer = AuthOptions.ISSUER,

						// Будет ли валидироваться потребитель токена.
						ValidateAudience = true,
						// Установка потребителя токена.
						ValidAudience = AuthOptions.AUDIENCE,
						// Будет ли валидироваться время существования.
						ValidateLifetime = true,

						// Установка ключа безопасности.
						IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
						// Галидация ключа безопасности.
						ValidateIssuerSigningKey = true,
					};
				});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo()
				{
					Title = "Swagger XML Api Demo",
					Version = "v1"
				});
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Description = "In the next box, enter the request header to add JWT licenses token: bearer token",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
				// Set the comments path for the Swagger JSON and UI.
				string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => endpoints.MapControllers());

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tyazhmash api");
			});
		}
	}
}