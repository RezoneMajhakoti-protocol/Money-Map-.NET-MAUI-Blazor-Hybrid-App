using Microsoft.AspNetCore.Components;
using MoneyMap.Models;
using MudBlazor;
using System.Transactions;
using static MudBlazor.Colors;

namespace MoneyMap.Components.Pages
{
    public partial class Dashboard
    {
        [CascadingParameter]
        private Action<string> AppBarTitle { get; set; }
		private string daterange { get; set; } = "Last 1 Day";
		String[] date_range = { "Last 1 Day", "Last 7 Days", "Last 30 Days", "Last 3 Months", "Last 6 Months", "Last 1 Year" };
	
		private string? total_Balance;
		private string? total_income = "0";
		/*	private string? total_saving = "0";*/
		private string? total_expens = "0";
		private string? _TOTAL_REMAINING_DEBT = "0";
		private string? _TOTAL_CLEARED_DEBT = "0";
		IEnumerable<Transaction_Model> highest_transaction;
		IEnumerable<Debt_Model> _REMAINING_DEBT_LIST;
		IEnumerable<Debt_Model> _CLEARD_DEBT_LIST;

		private string? currency;
		

		protected sealed override void OnInitialized()
		{

            highest_transaction = data.GetTranscationAll().OrderByDescending(x => x._amount).Take(10).ToList();
            currency = data._CUR();
			DateTime now = DateTime.Now;
			DateTime onedayago = now.AddDays(-1);
			AppBarTitle.Invoke("Dashboard");

	
				if(daterange == "Last 1 Day")
				{

                total_Balance = (data.GetTranscationAll()
					.Where(t => (t._types == "Income" || t._types == "Saving") &&
					(t._date >= onedayago &&
					t._date <= DateTime.Now))
					.Sum(t => t._amount)).ToString();
                Console.WriteLine(total_Balance + " to");


                total_income = (data.GetTranscationAll()
				.Where(t => (t._types == "Income") &&
				(t._date >= onedayago  &&
				t._date <= DateTime.Now))
				.Sum(t => t._amount)).ToString();

                total_expens = (data.GetTranscationAll()
                .Where(t => (t._types == "Expense") &&
                (t._date >= onedayago &&
                t._date <= DateTime.Now))
                .Sum(t => t._amount)).ToString();
			

				_TOTAL_REMAINING_DEBT = (data.GetAllDebt().Where(t => (t._debt_status == "Unpaid") &&
				(t._debtDate >= onedayago &&
				t._debtDate <= DateTime.Now)
				).Sum(d => d._debtAmount)).ToString();

				_TOTAL_CLEARED_DEBT = (data.GetAllDebt().Where(t => (t._debt_status == "Paid") &&
				(t._debtDate >= onedayago &&
				t._debtDate <= DateTime.Now)
				).Sum(d => d._debtAmount)).ToString();

				_REMAINING_DEBT_LIST = data.GetAllDebt().Where(d => (d._debt_status == "Unpaid") &&
				(d._debtDate >= onedayago &&
				d._debtDate <= DateTime.Now));

				_CLEARD_DEBT_LIST = data.GetAllDebt().Where(d => (d._debt_status == "Paid") &&
				(d._debtDate >= onedayago &&
				d._debtDate <= DateTime.Now));

			}

			

		}


		private void HandleTotal(ChangeEventArgs e)
		{
			daterange = e.Value?.ToString();

			var year = DateTime.Now.Year;
			DateTime now = DateTime.Now;
			DateTime onedayago = now.AddDays(-1);
			DateTime oneWeekAgo = now.AddDays(-7);
			DateTime _30_days_ago = now.AddDays(-30);
			DateTime mon_ago = now.AddMonths(-3);
			DateTime six_mon = now.AddMonths(-6);
			DateTime years = now.AddYears(-1);


			for(int i = 0; i < date_range.Length;i++)
			{
				if(daterange == date_range[i])
				{

					var _from_date = date_range[i] == date_range[0] ? onedayago :
					  date_range[i] == date_range[1] ? oneWeekAgo :
					  date_range[i] == date_range[2] ? _30_days_ago :
					  date_range[i] == date_range[3] ? mon_ago :
					  date_range[i] == date_range[4] ? six_mon :
					  date_range[i] == date_range[5] ? years : onedayago;

					

					total_Balance = (data.GetTranscationAll()
					.Where(t => (t._types == "Income" || t._types == "Saving") &&
					(t._date >= _from_date &&
					t._date <= DateTime.Now))
					.Sum(t => t._amount)).ToString();

					total_income = (data.GetTranscationAll()
					.Where(t => (t._types == "Income") &&
					(t._date >= _from_date &&
					t._date <= DateTime.Now))
					.Sum(t => t._amount)).ToString();

					total_expens = (data.GetTranscationAll()
                    .Where(t => (t._types == "Expense") &&
                    (t._date >= _from_date &&
                    t._date <= DateTime.Now))
                    .Sum(t => t._amount)).ToString();

					_TOTAL_REMAINING_DEBT = (data.GetAllDebt().Where(t => (t._debt_status == "Unpaid") &&
				(t._debtDate >= _from_date &&
				t._debtDate <= DateTime.Now)).Sum(d => d._debtAmount)).ToString();

					_TOTAL_CLEARED_DEBT = (data.GetAllDebt().Where(t => (t._debt_status == "Paid") &&
				(t._debtDate >= _from_date &&
				t._debtDate <= DateTime.Now)
				).Sum(d => d._debtAmount)).ToString();

					_REMAINING_DEBT_LIST = data.GetAllDebt().Where(d => (d._debt_status == "Unpaid") &&
			  (d._debtDate >= _from_date
				  &&
			  d._debtDate <= DateTime.Now));

					_CLEARD_DEBT_LIST = data.GetAllDebt().Where(d => (d._debt_status == "Paid") &&
					(d._debtDate >= _from_date &&
					d._debtDate <= DateTime.Now));

				}
			}

	

		}

		/*	public async Task DateFilter()
			{

				*//*var year = DateTime.Now.Year;
				DateTime now = DateTime.Now;
				DateTime oneWeekAgo = now.AddDays(-7);
				DateTime _30_days_ago = now.AddDays(-30);
				DateTime mon_ago = now.AddMonths(-3);
				DateTime six_mon = now.AddMonths(-6);
				DateTime years = now.AddYears(-1);


				for (int i = 0; i < date_range.Length; i++)
				{
					if (daterange == date_range[i])
					{
						total_Balance = data.GetTranscationAll()
							.Where(t => (t._types == "Income" || t._types == "Saving") && 
							(t._date >= (date_range[0] == "Last 1 Day" 
							? now 
							: date_range[1] == "Last 7 Days" 
							? oneWeekAgo
							: date_range[2] == "Last 30 Days"
							? _30_days_ago
							: date_range[3] == "Last 3 Months"
							? mon_ago
							: date_range[4] == "Last 6 Months"
							? six_mon
							: date_range[5] == "Last 1 Year"
							? years
							: now) && 
							t._date <= DateTime.Now))
							.Sum(t => t._amount);
					}
				}*//*



			}*/
		/*			if (daterange == "Last 1 Day")
					{

						total_Balance = data.GetTranscationAll().Where(t => (t._types == "Income" || t._types == "Saving") && (t._date >= new DateTime(2025, 1, 1) && t._date <= DateTime.Now)).Sum(t => t._amount);
				*//*	var startDate = new DateTime(2025, 1, 1); // Example start date
					var endDate = new DateTime(2025, 1, 31); // Example end date

					var total_Balance = data.GetTranscationAll()
						.Where(t =>
							(t._types == "Income" || t._types == "Saving") && // Filter by type
							t.Date >= startDate && t.Date <= endDate)        // Filter by date range
						.Sum(t => t.Amount);*//*
			}
					else
					{
						total_Balance = 0;
					}*/



		/*	string formattedNow = now.ToString("yyyy-MM-dd");
			string formattedOneWeekAgo = oneWeekAgo.ToString("yyyy-MM-dd");
			string formattedMon = _30_days_ago.ToString("yyyy-MM-dd");
			string formattedthree = mon_ago.ToString("yyyy-MM-dd");
			string formattedsix = six_mon.ToString("yyyy-MM-dd");
			string formattedyear = years.ToString("yyyy-MM-dd");*/

		/*	var startDate = new DateTime(2025, 1, 1);
			var endDate = new DateTime(2025, 1, 31); 

			var total_Balance = data.GetTranscationAll()
				.Where(t =>
					(t._types == "Income" || t._types == "Saving") && 
					t.Date >= startDate && t.Date <= endDate)       
				.Sum(t => t.Amount);*/
	}
}
