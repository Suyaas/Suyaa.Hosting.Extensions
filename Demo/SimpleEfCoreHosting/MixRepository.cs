﻿using Suyaa.Data.Dependency;
using Suyaa.Data;
using System.Linq.Expressions;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa;
using Suyaa.Data.Providers;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.EFCore.Providers;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.DbWorks.Helpers;

namespace SimpleEfCoreHosting
{
    /// <summary>
    /// 混合数据仓库 增删改使用自研Orm引擎 查询使用EfCore
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public sealed class MixRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IDbEntity<TId>, new()
        where TId : notnull
    {
        #region 依赖注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IDbWorkManager _dbWorkManager;

        /// <summary>
        /// 混合数据仓库
        /// </summary>
        public MixRepository(
            IDependencyManager dependencyManager,
            IDbWorkManager dbWorkManager
            )
        {
            _dependencyManager = dependencyManager;
            _dbWorkManager = dbWorkManager;
        }

        #endregion

        // 获取当前作业
        private IDbWork GetDbWork()
        {
            var work = _dbWorkManager.GetCurrentWork();
            if (work is null) throw new NullException<IDbWork>();
            return work;
        }

        // 获取新增供应商
        private IDbInsertProvider<TEntity> GetDbInsertProvider()
        {
            var entityModelFactory = _dependencyManager.ResolveRequired<IEntityModelFactory>();
            var sqlRepository = GetDbWork().GetSqlRepository();
            DbInsertProvider<TEntity> provider = new DbInsertProvider<TEntity>(entityModelFactory, sqlRepository);
            return provider;
        }

        // 获取新增供应商
        private IDbDeleteProvider<TEntity> GetDbDeleteProvider()
        {
            var entityModelFactory = _dependencyManager.ResolveRequired<IEntityModelFactory>();
            var sqlRepository = _dependencyManager.ResolveRequired<ISqlRepository>();
            DbDeleteProvider<TEntity> provider = new DbDeleteProvider<TEntity>(entityModelFactory, sqlRepository);
            return provider;
        }

        // 获取新增供应商
        private IDbUpdateProvider<TEntity> GetDbUpdateProvider()
        {
            var entityModelFactory = _dependencyManager.ResolveRequired<IEntityModelFactory>();
            var sqlRepository = _dependencyManager.ResolveRequired<ISqlRepository>();
            DbUpdateProvider<TEntity> provider = new DbUpdateProvider<TEntity>(entityModelFactory, sqlRepository);
            return provider;
        }

        // 获取新增供应商
        private IDbQueryProvider<TEntity> GetDbQueryProvider()
        {
            EfCoreQueryProvider<TEntity> provider = new EfCoreQueryProvider<TEntity>();
            return provider;
        }

        #region 新增

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            // 获取当前作业
            var work = GetDbWork();
            if (entity is IHaveCreation haveCreation) haveCreation.CreationTime = DateTime.Now;
            GetDbInsertProvider().Insert(work, entity);
        }

        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            // 获取当前作业
            var work = GetDbWork();
            if (entity is IHaveCreation haveCreation) haveCreation.CreationTime = DateTime.Now;
            await GetDbInsertProvider().InsertAsync(work, entity);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            GetDbDeleteProvider().Delete(work, predicate);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            await GetDbDeleteProvider().DeleteAsync(work, predicate);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            if (entity is IHaveModification haveModification) haveModification.LastModificationTime = DateTime.Now;
            GetDbUpdateProvider().Update(work, entity, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            if (entity is IHaveModification haveModification) haveModification.LastModificationTime = DateTime.Now;
            GetDbUpdateProvider().Update(work, entity, selector, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            if (entity is IHaveModification haveModification) haveModification.LastModificationTime = DateTime.Now;
            await GetDbUpdateProvider().UpdateAsync(work, entity, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            if (entity is IHaveModification haveModification) haveModification.LastModificationTime = DateTime.Now;
            await GetDbUpdateProvider().UpdateAsync(work, entity, selector, predicate);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            // 获取当前作业
            var work = GetDbWork();
            return GetDbQueryProvider().Query(work);
        }

        #endregion
    }
}
