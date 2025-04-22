using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MoneyMap.Models;
using MudBlazor;
using ServiceStack;
using static MudBlazor.CategoryTypes;


namespace MoneyMap.Components.Pages
{
    public partial class Transaction
    {
		private string _SEARCHING;
		private const string description_name = "Description";
		private const string tags_name = "Tags";
		private const string types_name = "Types";
		private const string amount_name = "Amount";
		private const string date_name = "Date";
		private bool sorted = false;
		IEnumerable<Transaction_Model>? transaction_get;

		[CascadingParameter]
        private Action<string> AppBarTitle { get; set; }

		private string? currency;
		

        protected sealed override void OnInitialized()
        {
		

            currency = data._CUR();
			AppBarTitle.Invoke("Transaction");
			transaction_get = data.GetTranscationAll();

		
        }

		private async Task Export()
		{
			await data.Export("Transaction");
	
		}
		private async Task OpenFilter()
		{
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters
            {
                {"Types","Transaction"}
            };

            var dialog = await DialogService.ShowAsync<Dialog_Box.FilterDialog>("Filter", parameters,options);

            var result = await dialog.Result;



            if (!result.Canceled)
            {
			
                transaction_get = (IEnumerable<Transaction_Model>?)result.Data;
            }
            else
            {
                transaction_get = data.GetTranscationAll();
            }


        }
        private async Task OpenDialogAsync()
		{
			var options = new DialogOptions { CloseOnEscapeKey = true };
	
		
		
			var parameter = new DialogParameters<Dialog_Box.TempDialog>
			{
				{"Type", "Transaction" }
			};
			var dialog = await DialogService.ShowAsync<Dialog_Box.TempDialog>("Simple Dialog", parameter, options);

			var result = await dialog.Result;

			if (result.Canceled)
			{

                transaction_get = data.GetTranscationAll();

            }
		
		}

		private async Task Search()
		{
			//Snackbar.Add("Searched Item " + search, Severity.Success);
			await Task.Run(() =>
			{
				transaction_get = data.GetTranscationAll().Where(x => (x._description.ToLower().Contains(_SEARCHING.ToLower())));
			});
			
		}
        
		private async Task Sort(string title)
		{
			
				if (title == "Description")
            {
				if(!sorted)
				{
					transaction_get = data.GetTranscationAll().OrderBy(x => x._description);
					sorted = true;
				}
				else
				{
					transaction_get = data.GetTranscationAll().OrderByDescending(x => x._description);
					sorted = false;
				}
				
			}
            else if(title == "Tags")
            {
				if (!sorted)
				{
					transaction_get = data.GetTranscationAll().OrderBy(x => x._tags);
					sorted = true;
				}
				else
				{
					transaction_get = data.GetTranscationAll().OrderByDescending(x => x._tags);
					sorted = false;
				}
				
			}
            else if(title == "Types")
            {
				if (!sorted)
				{
					transaction_get = data.GetTranscationAll().OrderBy(x => x._types);
					sorted = true;
				}
				else
				{
					transaction_get = data.GetTranscationAll().OrderByDescending(x => x._types);
					sorted = false;
				}
				
			}
            else if(title == "Amount")
            {
				if (!sorted)
				{
					transaction_get = data.GetTranscationAll().OrderBy(x => x._amount);
					sorted = true;
				}
				else
				{
					transaction_get = data.GetTranscationAll().OrderByDescending(x => x._amount);
					sorted = false;
				}

				
			}
            else if(title == "Date")
            {
				if (!sorted)
				{
					transaction_get = data.GetTranscationAll().OrderBy(x => x._date);
					sorted = true;
				}
				else
				{
					transaction_get = data.GetTranscationAll().OrderByDescending(x => x._date);
					sorted = false;
				}
				
			}

		

		}
	}
}
