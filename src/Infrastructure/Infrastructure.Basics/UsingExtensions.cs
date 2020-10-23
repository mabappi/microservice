using System;

namespace Infrastructure.Basics
{
    public static class UsingExtensions
    {
        public static TResponse Using<T, TResponse>(this T caller, Func<IDisposable> disposable, Func<TResponse> action)
            where T : class
        {
            using (disposable())
            {
                return action();
            }
        }

        public static void Using<T>(this T caller, Func<IDisposable> disposable, Action action)
            where T : class
        {
            using (disposable())
            {
                action();
            }
        }
    }
}
