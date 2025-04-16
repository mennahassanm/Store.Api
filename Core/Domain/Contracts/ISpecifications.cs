using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecifications <TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity , bool>>? Criteria { get; }
       List <Expression <Func<TEntity , object>>> IncludeExpressions { get; set; }

        Expression<Func<TEntity , Object>>? OrderBY { get; set; }
        Expression<Func<TEntity , Object>>? OrderBYDescending { get; set; }

        int Skip { get; set; }
        int Take { get; set; }
        bool IsPagination { get; set; }


    }
}
