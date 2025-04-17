using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginationResponse<TEntity>
    {
        public PaginationResponse(int pageInex, int pageSize, int totalCount, IEnumerable<TEntity> data)
        {
            PageInex = pageInex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageInex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
