using Microsoft.AspNetCore.Components;

namespace MoneyMap.Components.Pages
{
	 public partial class Settings
	{
		[CascadingParameter]
		private Action<string> AppBarTitle { get; set; }

		private const string title_account = "Account";
		private const string currency_change = "Currency";
		protected sealed override void OnInitialized()
		{
			AppBarTitle.Invoke("Settings");

			navigationmanger.NavigateTo("/edit_account");

			

		}

	}
}
