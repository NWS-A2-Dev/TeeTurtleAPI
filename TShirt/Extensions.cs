using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Extensions
    {
        public static U Then<T, U>(this T obj, Func<T, U> fct)
        {
            return (fct(obj));
        }
    }
}
