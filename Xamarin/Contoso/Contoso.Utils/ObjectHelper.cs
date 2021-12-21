namespace Contoso.Utils
{
    public static class ObjectHelper
    {
        public static object Null = null;

        public static bool IsNull(object anyObject) => anyObject == null;
    }
}
