using MudBlazor;

namespace MoneyMap.Components.Pages.Settings_Page
{
	partial class Currency
	{

		private string _CURRENCY_CODE { get; set; } = "USD";

		private string[] _ALL_CURRENCY = { "USD", "NPR", "EUR" };
		private Dictionary<string, string> cur = new Dictionary<string, string>
		{
			{"USD","$"},
			{"NPR","रु"},
			{"EUR","€"},
		};
		public async Task Save()
		{

			foreach (KeyValuePair<string, string> code in cur)
			{
				if (code.Key == _CURRENCY_CODE)
				{
					data._CURRENCY_TYPE(code.Value);
					Snackbar.Add("Currency Successfully Changed To " + code.Value, Severity.Success);
					
				}
			}



		}


	}
}
