using System;
using Store.Domain.Contracts.DTO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Store.Tests.EqualityComparers
{
    internal class OrderComparer : IEqualityComparer<OrderDto>
    {
        public bool Equals([AllowNull] OrderDto x, [AllowNull] OrderDto y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;            

            return x.Id == y.Id &&
                x.Status == y.Status &&
                x.UserId == y.UserId &&
                Enumerable.SequenceEqual(x.Products, y.Products, new ProductComparer()); ;
        }

        public int GetHashCode([DisallowNull] OrderDto obj)
        {
            return HashCode.Combine(obj.Id, obj.Status, obj.Products);
        }
    }
}
