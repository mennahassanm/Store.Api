using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence
{
    public class SpecificationEvaluator
    {
        // Generate Query 
        public static IQueryable<TEntity> GetQuary<TEntity , TKey>(
            IQueryable<TEntity> inputQuery ,
            ISpecifications<TEntity , TKey> spec
            )
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            if ( spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBY is not null)
                query = query.OrderBy(spec.OrderBY);
            else if (spec.OrderBYDescending is not null)
                query = query.OrderByDescending(spec.OrderBYDescending);

            if (spec.IsPagination)
                query = query.Skip(spec.Skip).Take(spec.Take);


            query = spec.IncludeExpressions.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            
            return query;
        }
    }
}
