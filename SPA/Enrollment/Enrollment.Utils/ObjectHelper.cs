using LogicBuilder.Attributes;

namespace Enrollment.Utils
{
    public static class ObjectHelper
    {
        public static object Null = null;

        [AlsoKnownAs("ObjectIsNull")]
        public static bool IsNull(object anyObject) => anyObject == null;

        [AlsoKnownAs("StringIsNullOrEmpty")]
        public static bool StringIsNullOrEmpty(string value) => string.IsNullOrEmpty(value);
    }
}
