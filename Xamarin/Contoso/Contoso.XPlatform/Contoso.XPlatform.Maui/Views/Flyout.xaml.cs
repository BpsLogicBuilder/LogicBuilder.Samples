using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Views;

public partial class Flyout : ContentPage
{
    public CollectionView ListView;

    public Flyout()
	{
		InitializeComponent();
        Visual = VisualMarker.Default;
        ListView = MenuItemsListView;
    }
}