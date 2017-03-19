using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CreditCuisse.VirtualCard.Repository;
using CreditCuisse.VirtualCard.Services;

namespace CreditCuisse.VirtualCard.Infrastructure
{
    public class DataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAccountService>().ImplementedBy<AccountService>().LifestylePerThread());
            container.Register(Component.For<ICardValidator>().ImplementedBy<CardValidator>().LifestylePerThread());
            container.Register(Component.For<IAccountRepository>().ImplementedBy<AccountRepository>().LifestylePerThread());

        }
    }
}
