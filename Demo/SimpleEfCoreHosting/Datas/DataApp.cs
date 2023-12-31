﻿using Microsoft.EntityFrameworkCore;
using SimpleEfCoreHosting.Datas.Dto;
using SimpleEfCoreHosting.Entities;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.App.Services;
using Suyaa.Hosting.AutoMapper.Dependency;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Jwt.Configures;
using Suyaa.Logs.Dependency;

namespace SimpleEfCoreHosting.Datas
{
    /// <summary>
    /// 数据
    /// </summary>
    public sealed class DataApp : DomainServiceApp
    {
        private readonly IDependencyManager _dependencyManager;
        private readonly IObjectMapper _objectMapper;
        private readonly ILogger _logger;

        /// <summary>
        /// 数据
        /// </summary>
        /// <param name="dependencyManager"></param>
        public DataApp(
            IDependencyManager dependencyManager,
            IObjectMapper objectMapper,
            ILogger logger
            )
        {
            _dependencyManager = dependencyManager;
            _objectMapper = objectMapper;
            _logger = logger;
        }

        public async Task<string> GetJwtConfig()
        {
            var jwtConfig = _dependencyManager.ResolveRequired<IOptionConfig<JwtConfig>>();
            return await Task.FromResult(jwtConfig.CurrentValue.TokenKey);
        }

        public async Task<List<TestOutput>> GetTests()
        {
            var testRepository = _dependencyManager.ResolveRequired<IRepository<Test, string>>();
            var datas = await testRepository.Query().ToListAsync();
            return _objectMapper.Map<List<TestOutput>>(datas);
        }

        public async Task<Test> GetTest()
        {
            //var dbFactory = _dependencyManager.ResolveRequired<IDbFactory>();
            //var dbContextFactory = _dependencyManager.ResolveRequired<IDbContextFactory>();
            //var workNew = _dependencyManager.ResolveRequired<IDbWork>();
            var test = new Test() { Content = nameof(DataApp) };
            var testRepository = _dependencyManager.ResolveRequired<IRepository<Test, string>>();
            await testRepository.InsertAsync(test);
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //    foreach (var assembly in assemblies)
            //    {
            //        if (assembly.ManifestModule.Name != "Suyaa.Data.PostgreSQL.dll") continue;
            //        try
            //        {
            //            var types = assembly.GetTypes();
            //            foreach (var type in types)
            //            {
            //                if (type.FullName == "Suyaa.Data.PostgreSQL.Providers.PostgreSqlProvider")
            //                {
            //                    var obj = sy.Assembly.Create(type);
            //                }
            //            }
            //        }
            //        catch (Exception ex1)
            //        {
            //            _logger.Error(ex1.ToString());
            //        }
            //    }
            //    //var obj = sy.Assembly.Create("Suyaa.Data.PostgreSQL.Providers.PostgreSqlProvider");
            //    _logger.Error(ex.ToString());
            //}
            return await Task.FromResult(test);
        }

        public async Task Update()
        {
            //var dbFactory = _dependencyManager.ResolveRequired<IDbFactory>();
            //var dbContextFactory = _dependencyManager.ResolveRequired<IDbContextFactory>();
            //var workNew = _dependencyManager.ResolveRequired<IDbWork>();
            var test = new Test() { Content = nameof(DataApp) };
            var testRepository = _dependencyManager.ResolveRequired<IRepository<Test, string>>();
            await testRepository.UpdateAsync(test, d => d.Content, d => d.Id == "018c583bf26900000100000000f6d4b4");
        }
    }
}
