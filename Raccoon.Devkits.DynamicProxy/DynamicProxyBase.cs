using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Raccoon.Devkits.DynamicProxy
{
    public abstract class DynamicProxyBase<TIService> : DispatchProxy where TIService : class
    {
        public TIService Target { get; set; } = null!;
        public Type ImplementationType { get; set; } = null!;
        public IServiceProvider ServiceProvider { get; set; } = null!;
    }
}
