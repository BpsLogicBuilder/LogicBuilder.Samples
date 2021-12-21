using System;

namespace CreateCommonView
{
    class Program
    {
        static void Main(string[] args)
        {
            WriterForEnums.Write();
            CreateViewClassesFromParametersClasses.Write();
        }
    }

    internal struct Constants
    {
        internal const string BASEPATH = @"C:\.github\BlaiseD\LogicBuilder.Samples\.NetCore\Contoso";
    }
}
