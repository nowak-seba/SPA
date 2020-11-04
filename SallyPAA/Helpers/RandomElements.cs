using System;
using System.Collections.Generic;
using System.Linq;

namespace SallyPAA.Helpers
{
    public static class RandomElements
    {
        public static List<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount)
        {
            return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
        }
    }
}
