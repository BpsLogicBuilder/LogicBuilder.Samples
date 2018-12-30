using System;

namespace CreateCommonViewModelsForInputQuestions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            WriterForEnums.Write();
            CreateViewModelClassesFromParametersClasses.Write();
        }
    }
}
