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
        internal const string BASEPATH = @"C:\BLoB\Samples\.NetCore\CheckMySymptoms";
    }
}
