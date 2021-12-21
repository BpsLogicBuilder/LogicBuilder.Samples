using LogicBuilder.Attributes;

namespace Contoso.Bsl.Flow
{
    public interface ICustomActions
    {
        [AlsoKnownAs("WriteToLog")]
        void WriteToLog(string message);
    }
}
