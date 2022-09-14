using System;
using System.Collections.Generic;
using Store.DAL.Users;
using Store.DAL.Interfaces;
using Store.DAL.Context;
using System.Linq;

namespace Store.DAL.Repositories
{
    public class UserRepository: IRepository<RegisteredUser>
    {
        public IEnumerable<RegisteredUser> GetAll()
        {
            return StoreContext.Users;
        }

        public RegisteredUser GetById(string id)
        {
            return StoreContext.Users.Find(p => p.Id == id);
        }

        public IEnumerable<RegisteredUser> Find(Func<RegisteredUser, bool> predicate)
        {
            return StoreContext.Users.Where(predicate).ToList();
        }


        public bool Create(RegisteredUser entity)
        {
            StoreContext.Users.Add(entity);

            return StoreContext.Users.FirstOrDefault(p => p.Id == entity.Id) != null;
        }

        public bool Update(RegisteredUser entity)
        {
            var toUpdate = StoreContext.Users.FirstOrDefault(p => p.Id == entity.Id);
            if (toUpdate is null)
                return false;

            toUpdate = entity;

            return StoreContext.Users.Contains(toUpdate);
        }

        public bool Delete(string id)
        {
            StoreContext.Users.RemoveAll(p => p.Id == id);

            return StoreContext.Users.FirstOrDefault(p => p.Id == id) == null;
        }
    }
}
