using Microsoft.Extensions.DependencyInjection;
using FylingDonkeyFSTask.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FylingDonkeyFSTask.Business.Services;
using FylingDonkeyFSTask.DataAccess.Concreate;
using FylingDonkeyFSTask.DataAccess.Interfaces;
using FylingDonkeyFSTask.Business.Concreate;

namespace FylingDonkeyFSTask.Business.DependencyResolver
{
    public static class MicrosoftIOC
    {
        public static void AddCustomDependencies(this IServiceCollection services)
        {
            services.AddDbContext<FylingDonkeyFSTaskContext>();

            //Generic Classlar Dep. Eklendi
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
        }

    }
}
