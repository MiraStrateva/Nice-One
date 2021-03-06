﻿namespace NiceOne.Infrastructure
{
    using System;
    using System.Text;
    using GreenPipes;
    using Hangfire;
    using MassTransit;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using NiceOne.Messages;
    using Services.Identity;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebService<TDbContext>(this IServiceCollection services,
           IConfiguration configuration)
           where TDbContext : DbContext
        {
            services
                 .AddDatabase<TDbContext>(configuration)
                 .AddApplicationSettings(configuration)
                 .AddTokenAuthentication(configuration)
                 .AddHealth(configuration)
                 .AddControllers();

           return services;
        }

        public static IServiceCollection AddDatabase<TDbContext>(
           this IServiceCollection services,
           IConfiguration configuration)
           where TDbContext : DbContext
           => services
               .AddScoped<DbContext, TDbContext>()
               .AddDbContext<TDbContext>(options => options
                   .UseSqlServer(configuration.GetDefaultConnectionString(),
                   sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                           maxRetryCount: 10,
                           maxRetryDelay: TimeSpan.FromSeconds(30),
                           errorNumbersToAdd: null);
                   }));
  
        public static IServiceCollection AddApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)),
                    config => config.BindNonPublicProperties = true);

        public static IServiceCollection AddTokenAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            JwtBearerEvents events = null)
        {
            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    if (events != null)
                    {
                        bearer.Events = events;
                    }
                });

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }

        public static IServiceCollection AddMessaging(
            this IServiceCollection services,
            IConfiguration configuration,
            params Type[] consumers)
        {
            services
                .AddMassTransit(mt =>
                {
                    consumers.ForEach(consumer => mt.AddConsumer(consumer));
                    mt.AddBus(bus => Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host("rabbitmq", host =>
                        {
                            host.Username("rabbitmq");
                            host.Password("rabbitmq");
                        });

                        cfg.UseHealthCheck(bus);

                        consumers.ForEach(consumer => cfg
                            .ReceiveEndpoint(consumer.FullName, endpoint =>
                            {
                                endpoint.PrefetchCount = 7; 
                                endpoint.UseMessageRetry(x => x.Interval(7, 77));
                                endpoint.ConfigureConsumer(bus, consumer);
                            }));
                    }));
                })
                .AddMassTransitHostedService();

            services
                .AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetDefaultConnectionString()));

            services.AddHangfireServer();

            services.AddHostedService<MessagesHostedService>();

            return services;
        }

        public static IServiceCollection AddHealth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var healthChecks = services.AddHealthChecks();

            healthChecks
                .AddSqlServer(configuration.GetDefaultConnectionString());

            healthChecks
                .AddRabbitMQ(rabbitConnectionString: "amqp://rabbitmq:rabbitmq@rabbitmq/");

            return services;
        }
    }
}
