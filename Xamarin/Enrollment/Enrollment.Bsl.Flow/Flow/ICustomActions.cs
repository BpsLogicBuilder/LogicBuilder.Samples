using LogicBuilder.Attributes;

namespace Enrollment.Bsl.Flow
{
    public interface ICustomActions
    {
        [AlsoKnownAs("WriteToLog")]
        void WriteToLog(string message);
    }
}
