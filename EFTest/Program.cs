using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace EFTest
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            //containerBuilder.
            _container = containerBuilder.Build();

        }
    }
}
