using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Reflection;

namespace MathsCore.LinearAlgebra
{
    public static class VectorisableObjectExtensions
    {
        public static Vector<string, TField> ToVector<T, TField>(this T t)
        {
            var result = new Vector<string, TField>();
            t.EachProperty(
                prop =>
                {
                    if (!prop.HasAttribute("VectorComponent")) return;
                    if (prop.PropertyType == typeof(TField))
                        result[prop.Name] = t.GetPropertyValue(prop.Name);
                    if (prop.PropertyType != typeof(IEnumerable<TField>)) return;
                    var enumerable = ((IEnumerable<TField>)t.GetPropertyValue(prop.Name)).ToList();
                    enumerable.EachIndex(i => result[prop.Name + "_" + i] = enumerable[i]);
                });
            return result;
        }
    }
}