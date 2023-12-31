﻿using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Multilingual.Helpers;
using Suyaa.Hosting.EfCore.Helpers;

namespace Suyaa.Hosting.UnitOfWork.EFCore.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加EFCore支持
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <returns></returns>
        public static IDependencyManager AddEfCoreUnitOfWork(this IDependencyManager dependencyManager)
        {
            // 注册程序集
            //dependencyManager.Include<DbSetFactory>();
            // 使用数据库
            dependencyManager.AddDbUnitOfWork();
            dependencyManager.AddEfCore();
            return dependencyManager;
        }
    }
}
