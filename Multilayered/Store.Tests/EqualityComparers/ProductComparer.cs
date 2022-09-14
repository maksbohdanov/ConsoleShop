using Store.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Store.Tests.EqualityComparers
{
    internal class ProductComparer : IEqualityComparer<ProductDto>
    {
        public bool Equals([AllowNull] ProductDto x, [AllowNull] ProductDto y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.Name == y.Name &&
                x.Description == y.Description &&
                x.Cost == y.Cost;
        }

        public int GetHashCode([DisallowNull] ProductDto obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.Category, obj.Description, obj.Cost);
        }
    }
}
