namespace Enrollment.Forms.View
{
    public abstract class ViewBase
    {
        public string TypeFullName { get { return this.GetType().FullName; } }
    }
}
