using FluentValidation;
using FluentValidation.AspNetCore;
using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Persistence.Data.Contexts;
using Hotel.Persistence.Executor;
using Hotel.Persistence.Repositories;
using Hotel.Presentation;
using Hotel.Presentation.Controllers;
using Hotel.Presentation.Mapper.Account;
using Hotel.Presentation.Mapper.Rooms;
using Hotel.Services.Interfaces;
using Hotel.Services.Mapper.Facilities;
using Hotel.Services.Mapper.Offers;
using Hotel.Services.Mapper.Rooms;
using Hotel.Services.Rooms;
using Hotel.Services.Services;
using Hotel_Reservation_System.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Hotel_Reservation_System.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            var connectionString = configuration["ConnectionStrings:DefaultConnection"]; //.GetConnectionString("DefaultConnection") ??
                //throw new InvalidOperationException("Connection String 'DefaultConnection' Not Found");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddControllers().AddApplicationPart(typeof(RoomController).Assembly);

            services.AddScoped<TransactionMiddleware>();
            services.AddScoped<GlobalErrorHandlerMiddleware>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IAsyncQueryExecutor, EfAsyncQueryExecutor>();
            services.AddScoped<IAccountServices, AccountServices>();

            services
                .AddSwaggerConfiguration()
                .AddMapperConfiguration()
                .AddFluentValidationConfiguration()
                .AddIdentityServices()
                .AddAuthenticationConfiguration(configuration);
           

            return services;
        }
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RoomDtoProfile).Assembly);
            services.AddAutoMapper(typeof(OfferDtoProfile).Assembly);
            services.AddAutoMapper(typeof(RoomViewModelProfile).Assembly);
            services.AddAutoMapper(typeof(FacilityDtoProfile).Assembly);
            services.AddAutoMapper(typeof(AccountViewModelProfile).Assembly);
            services.AddAutoMapper(typeof(CommonInfo).Assembly);
            return services;
        }
        public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders(); ;
            return services;
        }

        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

            services.AddAuthentication(opt => opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                 
                    };
                });
            services.AddAuthorization();
            return services;
        }
    }
}
