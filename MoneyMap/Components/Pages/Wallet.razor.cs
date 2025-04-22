using Microsoft.AspNetCore.Components;
using MoneyMap.Application_Service;
using MoneyMap.Models;

namespace MoneyMap.Components.Pages
{
    public partial class Wallet
    {
        [CascadingParameter]
        private Action<string> AppBarTitle { get; set; }
		IEnumerable<Debt_Model> _TOTAL_REMAINING_DEBT_LIST;
        IEnumerable<Debt_Model> _TOTAL_CLEARD_DEBT_LIST;

		private string? currency;
		


		protected sealed override void OnInitialized()
        {
			currency = data._CUR();
			AppBarTitle.Invoke("Wallet");
            _TOTAL_REMAINING_DEBT_LIST = data.GetAllDebt().Where(d => d._debt_status == "Unpaid");
            _TOTAL_CLEARD_DEBT_LIST = data.GetAllDebt().Where(d => d._debt_status == "Paid");
        }

 
        public double Remaining_debt()
        {
            return data.GetAllDebt().Where(d => d._debt_status == "Unpaid").Sum(d => d._debtAmount);

        }

        public double Cleared_debt()
        {
            return data.GetAllDebt().Where(d => d._debt_status == "Paid").Sum(d => d._debtAmount);
        }
    }
}
