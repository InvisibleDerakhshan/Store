using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bugeto_Store.Common
{
    public static class Pagination
    {
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source,int page ,int PageSize , out int rowsCount)
        {
            rowsCount = source.Count();
            return source.Skip((page - 1) * PageSize).Take(PageSize);
        }
    }
}
