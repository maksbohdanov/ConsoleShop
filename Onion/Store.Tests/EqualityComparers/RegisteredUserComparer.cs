using System;
using Store.Domain.Contracts.DTO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Store.Tests.EqualityComparers
{
    internal class RegisteredUserComparer : IEqualityComparer<RegisteredUserDto>
    {
        public bool Equals([AllowNull] RegisteredUserDto x, [AllowNull] RegisteredUserDto y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                x.FirstName == y.FirstName &&
                x.LastName == y.LastName &&
                x.UserType == y.UserType &&
                x.CredentialsId == y.CredentialsId &&
                Enumerable.SequenceEqual(x.Orders, y.Orders, new OrderComparer());
        }

        public int GetHashCode([DisallowNull] RegisteredUserDto obj)
        {
            return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName, obj.UserType, obj.CredentialsId, obj.Orders);
        }
    }
}
