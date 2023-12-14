using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Factories
{
    /// <summary>
    /// 主机数据库作业拦截器工厂
    /// </summary>
    public class HostDbWorkInterceptorFactory : DbWorkInterceptorFactory
    {
        /// <summary>
        /// 主机数据库作业拦截器工厂
        /// </summary>
        /// <param name="dependencyManager"></param>
        public HostDbWorkInterceptorFactory(IDependencyManager dependencyManager) : base(dependencyManager.Resolves<IDbWorkInterceptor>())
        {
            var interceptors = dependencyManager.Resolves<IDbWorkInterceptor>();
        }
    }
}
