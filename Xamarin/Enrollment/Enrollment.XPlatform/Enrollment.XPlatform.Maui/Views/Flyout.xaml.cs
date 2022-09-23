using Microsoft.Maui.Controls;

namespace Enrollment.XPlatform.Views;

public partial class Flyout : ContentPage
{
    public CollectionView ListView;

    public Flyout()
	{
		InitializeComponent();
        //Visual = VisualMarker.Default;
        ListView = MenuItemsListView;
    }
}