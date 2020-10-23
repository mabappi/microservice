using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Basics;
using JetBrains.Annotations;

namespace Infrastructure.Exceptions
{
    public sealed class DomainException : Exception
    {
        public DomainException([NotNull] string message) : this(message, new List<DomainError>())
        {
        }

        public DomainException([NotNull] string message, [NotNull] IList<DomainError> errors) : base(message)
        {
            Errors = errors.GuardAgainstNull(nameof(errors));
        }

        [NotNull]
        public IList<DomainError> Errors { get; }

        public string ToUserSummary()
        {
            var summary = Message;
            if (Errors.Any())
            {
                summary = $"{Message} {Environment.NewLine} {string.Join(Environment.NewLine, Errors.Select(x => x.Message))}";
            }
            return summary;
        }

        public override string ToString() =>
            $"{base.ToString()} {Environment.NewLine} {string.Join(Environment.NewLine, Errors.Select(x => x.Message))}";
    }
}
