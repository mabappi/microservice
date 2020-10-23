using Infrastructure.Basics;
using JetBrains.Annotations;

namespace Infrastructure.Exceptions
{
    public sealed class DomainError
    {
        public DomainError([NotNull] string message) => Message = message.GuardAgainstNullOrEmpty(nameof(message));

        public string Message { get; }

        public override string ToString() => Message;
    }
}
