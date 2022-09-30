namespace Enrollment.XPlatform.Constants
{
    /// <summary>
    /// The keys match the workflow variable's member name property.
    /// Typically for reference types the member name is typeof(T).FullName.
    /// For literals we more unique names to interact with the workflow from the code.
    /// </summary>
    public struct FlowDataCacheItemKeys
    {
        public const string Get_Selector_Success = nameof(Get_Selector_Success);
        public const string SearchText = nameof(SearchText);
        public const string SkipCount = nameof(SkipCount);
    }
}
