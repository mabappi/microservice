using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Infrastructure.Basics
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null || action == null)
            {
                return;
            }

            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static void AddRange<T>([NotNull] this ICollection<T> collection, [NotNull] ICollection<T> itemsToAdd)
        {
            collection.GuardAgainstNull(nameof(collection));
            itemsToAdd.GuardAgainstNull(nameof(itemsToAdd));

            itemsToAdd.ForEach(collection.Add);
        }
    }
}
