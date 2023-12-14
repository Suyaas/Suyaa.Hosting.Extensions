using Suyaa;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using Suyaa.Logs.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEfCoreHosting.DbWorkInterceptors
{
    /// <summary>
    /// 小写下划线命名方式
    /// </summary>
    public sealed class DemoDbWorkInterceptor : IDbWorkInterceptor
    {
        private readonly ILogger _logger;

        public DemoDbWorkInterceptor(
            ILogger logger
            )
        {
            _logger = logger;
        }

        public DbCommand? DbCommandCreating(DbCommand? command)
        {
            //throw new NotImplementedException();
            _logger.Info($"{nameof(DbCommandCreating)}");
            return command;
        }

        public DbCommand DbCommandExecuting(DbCommand command)
        {
            //throw new NotImplementedException();
            _logger.Info($"{nameof(DbCommandExecuting)} - {command.CommandText}");
            return command;
        }
    }
}
