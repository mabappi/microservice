using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Infrastructure.Basics
{
    public static class Guard
    {
        [ContractAnnotation(@"name:null => halt")]
        [AssertionMethod]
        public static void AgainstNull(object @object, [NotNull] string name)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        [ContractAnnotation(@"name:null => halt")]
        [AssertionMethod]
        public static void AgainstNullOrEmpty(string @string, [NotNull] string name)
        {
            if (string.IsNullOrEmpty(@string))
            {
                throw new ArgumentNullException(name);
            }
        }

        [NotNull]
        public static T GuardAgainstNull<T>(this T @object, [NotNull] string name) where T : class
        {
            AgainstNull(@object, name);
            return @object;
        }

        [NotNull]
        public static string GuardAgainstNullOrEmpty(this string @string, [NotNull] string name)
        {
            AgainstNullOrEmpty(@string, name);
            return @string;
        }

        public static void GuardAgainstNullInList<T>(IEnumerable<T> list, [NotNull] string name) where T : class
        {
            var enumerable = list as T[] ?? list.ToArray();
            GuardAgainstNull(enumerable, nameof(list));
            if (enumerable.Any(x => x == null))
            {
                throw new ArgumentException(@"List contains null value.");
            }
        }
    }
}
