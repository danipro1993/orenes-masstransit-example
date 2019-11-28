using Autofac;
using Autofac.Extensions.DependencyInjection;
using Masstransit.Consumer.HostedServices;
using Masstransit.Consumer.Messages;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Masstransit.Consumer
{
    public class Startup
    {
        public ILifetimeScope ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<MassTransitService>();

            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);

            ConfigureMassTransit(builder);

            this.ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         
        }

        private void ConfigureMassTransit(ContainerBuilder builder)
        {

            builder.AddMassTransit(x =>
            {
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://127.0.0.1"), (configure) =>
                    {
                        configure.Username("guest");
                        configure.Password("guest");

                        configure.UseCluster(cluster =>
                        {
                            cluster.ClusterMembers = new[] { "localhost" };
                        });
                    });

                    cfg.ReceiveEndpoint(host, "queueName", ec =>
                    {
                        ec.Consumer<MessageConsumerHandler>(context);
                    });
                }));
               
            });

            builder.RegisterType<MessageConsumerHandler>().InstancePerLifetimeScope();

        }
    }
}
