using System;
using System.Collections.Generic;

namespace Store.DAL.Interfaces
{
    public interface IRepository<BaseEntity> where BaseEntity: class
    {
        IEnumerable<BaseEntity> GetAll();

        BaseEntity GetById(string id);

        IEnumerable<BaseEntity> Find(Func<BaseEntity, Boolean> predicate);

        bool Create(BaseEntity entity);

        bool Update(BaseEntity entity);

        bool Delete(string id);
    }
}
