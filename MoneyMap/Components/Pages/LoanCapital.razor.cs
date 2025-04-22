using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MoneyMap.Models;
using MudBlazor;
using ServiceStack;
using static MudBlazor.CategoryTypes;

namespace MoneyMap.Components.Pages
{
    public partial class LoanCapital
    {
		private string _SEARCHING = "";
		private const string debt_source = "debt_source";
		private const string debt_Amount = "debt_Amount";
		private const string debt_date = "debt_date";
		private const string debt_status = "debt_status";
		private bool sorted = false;
		IEnumerable<Debt_Model>? debt_get;
		[CascadingParameter]
        private Action<string> AppBarTitle { get; set; }
		private string? currency;
		

        protected sealed override void OnInitialized()
        {
			currency = data._CUR();
			AppBarTitle.Invoke("Loan Capital");
			debt_get = data.GetAllDebt();
		}

        private async Task Export()
        {
            await data.Export("Debt");
        }
        private async Task OpenFilter()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                {"Types","Loan"}
            };

            var dialog = await DialogService.ShowAsync<Dialog_Box.FilterDialog>("Filter", parameters ,options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                debt_get = (IEnumerable<Debt_Model>?)result.Data;
            }
            else
            {
                debt_get = data.GetAllDebt();
            }


        }
        private async Task OpenDialogAsync()
	{
		var options = new DialogOptions { CloseOnEscapeKey = true };
		
		var parameter = new DialogParameters<Dialog_Box.TempDialog>
        {
			{"Type", "Loan" }
	    };
		var dialog = await DialogService.ShowAsync<Dialog_Box.TempDialog>("Simple Dialog", parameter, options);

		var result = await dialog.Result;

		if (result.Canceled)
		{

			debt_get = data.GetAllDebt();

		}

	}

		private async Task Search()
		{
			
            await Task.Run(() =>
            {
				Snackbar.Add("Searched Item " + _SEARCHING, Severity.Success);
				debt_get = data.GetAllDebt().Where(x => (x._debtSource.ToLower().Contains(_SEARCHING.ToLower())));
			});
				
		}

		private async Task Sort(string title)
	{

			if (title == "debt_source")
			{
				if (!sorted)
				{
					debt_get = data.GetAllDebt().OrderBy(x => x._debtSource);
				
					sorted = true;
				}
				else
				{
                    debt_get = data.GetAllDebt().OrderByDescending(x => x._debtSource);
					sorted = false;
				}

			}
			else if (title == "debt_Amount")
			{
                if (!sorted)
                {
                    debt_get = data.GetAllDebt().OrderBy(x => x._debtAmount);

                    sorted = true;
                }
                else
                {
                    debt_get = data.GetAllDebt().OrderByDescending(x => x._debtAmount);
                    sorted = false;
                }

            }
			else if (title == "debt_date")
			{
                if (!sorted)
                {
                    debt_get = data.GetAllDebt().OrderBy(x => x._debtDate);

                    sorted = true;
                }
                else
                {
                    debt_get = data.GetAllDebt().OrderByDescending(x => x._debtDate);
                    sorted = false;
                }

            }
			else if (title == "debt_status")
			{
                if (!sorted)
                {
                    debt_get = data.GetAllDebt().OrderBy(x => x._debt_status);

                    sorted = true;
                }
                else
                {
                    debt_get = data.GetAllDebt().OrderByDescending(x => x._debt_status);
                    sorted = false;
                }


            }
		


		}

	}

}
