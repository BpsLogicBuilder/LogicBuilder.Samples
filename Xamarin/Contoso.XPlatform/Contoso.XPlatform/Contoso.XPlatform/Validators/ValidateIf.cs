namespace Contoso.XPlatform.Validators
{
    public class ValidateIf<T> : ConditionBase<T>
    {
        public IValidationRule Validator { get; set; }
    }
}
