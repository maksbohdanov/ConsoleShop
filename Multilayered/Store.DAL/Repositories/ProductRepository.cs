using System;
using System.Collections.Generic;
using Store.DAL.Entities;
using Store.DAL.Interfaces;
using Store.DAL.Context;
using System.Linq;

namespace Store.DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {          
        public IEnumerable<Product> GetAll()
        {
            return StoreContext.Products;
        }

        public Product GetById(string id)
        {
            return StoreContext.Products.Find(p =>p.Id == id);
        }

        public IEnumerable<Product> Find(Func<Product, bool> predicate)
        {
            return StoreContext.Products.Where(predicate).ToList();
        }


        public bool Create(Product entity)
        {
            StoreContext.Products.Add(entity);

            return StoreContext.Products.FirstOrDefault(p => p.Id == entity.Id) != null;
        }

        public bool Update(Product entity)
        {
            var toUpdate = StoreContext.Products.FirstOrDefault(p => p.Id == entity.Id);
            if (toUpdate is null)
                return false;

            toUpdate = entity;

            return StoreContext.Products.Contains(toUpdate);
        }

        public bool Delete(string id)
        {
            StoreContext.Products.RemoveAll(p => p.Id == id);

            return StoreContext.Products.FirstOrDefault(p => p.Id == id) == null;
        }     
    }
}
