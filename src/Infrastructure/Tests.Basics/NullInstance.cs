namespace Tests.Basics
{
    public static class NullInstance
    {
        public static T GetNullInstance<T>() where T : class
        {
            return default(T);
        }
    }
}
