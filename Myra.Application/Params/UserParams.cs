using LinqKit;
using Microsoft.EntityFrameworkCore;
using Myra.Domain.Models;
using System.Linq.Expressions;

namespace Myra.Application.Params
{
    public class UserParams
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; } = 10;

        public Expression<Func<User, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<User>();

            if (!string.IsNullOrEmpty(Name))
                predicate = predicate.And(u => EF.Functions.Like(u.Name, $"%{Name}%"));

            if (!string.IsNullOrEmpty(Username))
                predicate = predicate.And(u => EF.Functions.Like(u.UserName, $"%{Username}%"));

            if (!string.IsNullOrEmpty(Email))
                predicate = predicate.And(u => EF.Functions.Like(u.Email, $"%{Email}%"));

            return predicate.IsStarted ? predicate : null;
        }
    }
}
