using System.Collections.Generic;
using CSharpExtensions.DesignPattern.Structural.Composite;
using MathsCore.Sets;

namespace MathsCore.DesignPattern.Structural.Composite
{
    public class CompositeSet<T> : IComposite<T>
    {
        public ICollection<IComposite<T>> Children { get; set; }
        public T Content { get; set; }

        public IComposite<T> Parent { get; set; }

        public IComposite<T> CreateComposite(ICollection<IComposite<T>> children)
        {
            return new CompositeSet<T> { Content = default(T), Children = children };
        }

        public CompositeSet()
        {
            Children = new Set<IComposite<T>>();
        }
    }
}
