using Infrastructure.Basics;
using Moq;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tests.Basics
{
    public abstract class BuilderBase<T> where T : class
    {
        private readonly IDictionary<string, object> _mockContainer;

        protected BuilderBase()
        {
            _mockContainer = new Dictionary<string, object>();
        }

        public T Build()
        {
            return BuildInternal();
        }

        protected abstract T BuildInternal();

        [NotNull]
        public TInput Get<TInput>() where TInput : class
        {
            var mock = GetMock<TInput>();
            return mock.Object;
        }

        public Mock<TInput> GetMock<TInput>() where TInput : class
        {
            var key = typeof(TInput).FullName;
            _mockContainer.TryGetValue(key, out object dependency);

            if (dependency == null)
            {
                var mock = new Mock<TInput>();
                WithMock(mock);
                return mock;
            }

            return (Mock<TInput>)dependency;
        }

        [NotNull]
        public BuilderBase<T> WithMock<TDependency>([NotNull] Mock<TDependency> mock) where TDependency : class
        {
            Guard.AgainstNull(mock, nameof(mock));
            var key = typeof(TDependency).FullName;

            if (!_mockContainer.ContainsKey(key))
            {
                _mockContainer.Add(key, mock);
            }
            return this;
        }
    }
}
