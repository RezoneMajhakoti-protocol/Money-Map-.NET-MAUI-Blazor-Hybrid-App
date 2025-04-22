namespace MoneyMap.Components.Pages
{
	public partial class LoginPage
	{

		protected sealed override void OnInitialized()
		{
			
			data.Init_Data();

		}
		void RegisterPage()
		{
			navigationmanger.NavigateTo("/Register");
		}

		void DashboardPage()
		{
			navigationmanger.NavigateTo("/dashboard");
		}
	}
}
