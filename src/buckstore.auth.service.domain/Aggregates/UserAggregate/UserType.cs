using buckstore.auth.service.domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class UserType : Enumeration
    {
        public static UserType Admin = new UserType(1, nameof(Admin));
        public static UserType Employee = new UserType(2, nameof(Employee));
        public static UserType Customer = new UserType(3, nameof(Customer));

        public UserType(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<UserType> List() =>
             new[] { Admin, Employee, Customer };

        public static UserType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new Exception($"Possible values for UserType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static UserType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new Exception($"Possible values for UserType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
