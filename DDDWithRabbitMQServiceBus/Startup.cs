using DDDWithRabbitMQServiceBus.EventBus;
using DDDWithRabbitMQServiceBus.EventBus.Abstract;
using DDDWithRabbitMQServiceBus.EventBusRabbitMQ;
using DDDWithRabbitMQServiceBus.Events;
using DDDWithRabbitMQServiceBus.Events.TestEventHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace DDDWithRabbitMQServiceBus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = Configuration["EventBusConnection"]
                };

                return new DefaultRabbitMqPersistentConnection(factory);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
            {
                var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<EventBus.IEventBusSubscriptionsManager>();
                return new EventBusRabbitMq(rabbitMqPersistentConnection, eventBusSubcriptionsManager);
            });
            services.AddSingleton<EventBus.IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<TestEventHandler>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            ConfigureEventBus(app);
        }

        public void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<TestEvent, TestEventHandler>();
        }
    }
}