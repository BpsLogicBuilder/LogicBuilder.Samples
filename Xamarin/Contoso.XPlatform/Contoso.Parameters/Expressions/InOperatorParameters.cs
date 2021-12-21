namespace Contoso.Parameters.Expressions
{
    public class InOperatorParameters : IExpressionParameter
    {
		public InOperatorParameters()
		{
		}

		public InOperatorParameters(IExpressionParameter itemToFind, IExpressionParameter listToSearch)
		{
			ItemToFind = itemToFind;
			ListToSearch = listToSearch;
		}

		public IExpressionParameter ItemToFind { get; set; }
		public IExpressionParameter ListToSearch { get; set; }
    }
}