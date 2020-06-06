using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace NetSpider.WebApi.Services
{
    public static class AutoMapperHelper
    {
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(NetSpider.Domain.Ctrip.CtripMapper));
            return services;
        }
    }
}
