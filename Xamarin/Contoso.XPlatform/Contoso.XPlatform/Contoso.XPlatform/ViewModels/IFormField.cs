namespace Contoso.XPlatform.ViewModels
{
    public interface IFormField
    {
        string Name { get; set; }
        bool IsVisible { get; set; }
        object Value { get; set; }
        void Clear();
    }
}
