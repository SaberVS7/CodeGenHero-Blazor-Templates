﻿namespace $safeprojectname$.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;
    using enums = $ext_safeprojectname$.Shared.Constants.Enums;

    public interface ICRUDOperation<EntityType> where EntityType : class
    {
        Task<IRepositoryActionResult<EntityType>> DeleteAsync(EntityType item);

        Task<EntityType> GetFirstOrDefaultAsync(EntityType item, enums.RelatedEntitiesType relatedEntitiesType);

        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;

        Task<IRepositoryActionResult<EntityType>> InsertAsync(EntityType item);

        Task<IRepositoryActionResult<EntityType>> UpdateAsync(EntityType item);
    }
}