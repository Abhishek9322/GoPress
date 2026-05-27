using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace GoPress.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection service)
        {
            service.AddMediatR(
                 Assembly.GetExecutingAssembly());

            //fluent validation here after if neede 



            // AutoMapper if needed here 

            return service;
        }
    }
}
