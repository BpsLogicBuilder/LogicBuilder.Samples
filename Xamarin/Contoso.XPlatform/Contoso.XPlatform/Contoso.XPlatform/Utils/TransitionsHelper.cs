using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    internal static class TransitionsHelper
    {
        public static async Task EntranceTransition(this View view, View transitionGrid, double offset)
        {
            await view.TranslateTo(offset, 0, 0);
            transitionGrid.IsVisible = false;
            await view.TranslateTo(0, 0, 1000, Easing.CubicOut);
        }
    }
}
