using System;
using System.Collections.Generic;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using Store.DAL.Context;
using System.Linq;

namespace Store.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {      
        public IEnumerable<Order> GetAll()
        {
            return StoreContext.Orders;
        }

        public Order GetById(string id)
        {
            return StoreContext.Orders.Find(p => p.Id == id);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return StoreContext.Orders.Where(predicate).ToList();
        }

        public bool Create(Order entity)
        {
            StoreContext.Orders.Add(entity);

            return StoreContext.Orders.FirstOrDefault(p => p.Id == entity.Id) != null;
        }

        public bool Update(Order entity)
        {
            var toUpdate = StoreContext.Orders.FirstOrDefault(p => p.Id == entity.Id);
            if (toUpdate is null)
                return false;

            toUpdate = entity;

            return StoreContext.Orders.Contains(toUpdate);
        }

        public bool Delete(string id)
        {
            StoreContext.Orders.RemoveAll(p => p.Id == id);

            return StoreContext.Orders.FirstOrDefault(p => p.Id == id) == null;
        }
    }
}
