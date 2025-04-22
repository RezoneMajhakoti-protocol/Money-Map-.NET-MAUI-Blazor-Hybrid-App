using MudBlazor;

namespace MoneyMap.Components.Layout;

public partial class MainNavLayout
{

    private string _title;
    bool _drawerOpen = true;
    protected sealed override void OnInitialized()
    {
        data.Init_Data();
     
      
     

    }
        
    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    private void SetAppBarTitle(string title)
    {
        _title = title;
    }

    private void ResetAll()
    {
        data.Reset();
    }
}