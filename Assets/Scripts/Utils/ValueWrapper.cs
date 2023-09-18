using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace TFR

{
    /// <summary>
    /// 绕过struct、value type按值传递，以及async、迭代器不允许ref out in造成的限制，允许按引用传进任意函数里
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueWrapper<T> where T : struct
    {
        public T Value { get; set; }
        public ValueWrapper(T value) { this.Value = value; }
        public static implicit operator T(ValueWrapper<T> wrapper)
        {
            if (wrapper == null)
            {
                return default(T);
            }
            return wrapper.Value;
        }
    }
}
