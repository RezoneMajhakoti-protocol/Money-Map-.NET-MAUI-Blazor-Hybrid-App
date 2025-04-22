using MoneyMap.Models;
using MudBlazor;
using System.Text.Json;
using System.Windows;
using static MudBlazor.CategoryTypes;

namespace MoneyMap.Application_Service
{
    public class Data : ITData
    {
        public List<Debt_Model> _DEBT_MODEL_LIST = new List<Debt_Model>();
		public List<Transaction_Model> _TRNSACTION_MODEL_LIST = new List<Transaction_Model>();

		public double _ALL_CALCULATED_TOTAL_DEBT;

		public IEnumerable<Debt_Model> GetAllDebt() => _DEBT_MODEL_LIST;
        public IEnumerable<Transaction_Model> GetTranscationAll() => _TRNSACTION_MODEL_LIST;
														/*.OrderByDescending(t => t._description);*/


		private string? _currency_types { get; set; } = null;
        public async Task Init_Data()
        {
			string transaction_filePath = Path.Combine(FileSystem.AppDataDirectory, "data_transaction.json");
			string debt_filePath = Path.Combine(FileSystem.AppDataDirectory, "data_debt.json");


            if (File.Exists(transaction_filePath) || File.Exists(debt_filePath))
            {
                try
                {
                    string transactionjsonData = await File.ReadAllTextAsync(transaction_filePath);
					string debtjsonData = await File.ReadAllTextAsync(debt_filePath);
                    _TRNSACTION_MODEL_LIST = JsonSerializer.Deserialize<List<Transaction_Model>>(transactionjsonData) ?? new List<Transaction_Model>();
					_DEBT_MODEL_LIST = JsonSerializer.Deserialize<List<Debt_Model>>(debtjsonData) ?? new List<Debt_Model>();

				}
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading data: {ex.Message}");
                    _TRNSACTION_MODEL_LIST = new List<Transaction_Model>();
					_DEBT_MODEL_LIST = new List<Debt_Model>();

				}
            }
            else
            {
                Console.WriteLine("File not found!");
                _TRNSACTION_MODEL_LIST = new List<Transaction_Model>();
				_DEBT_MODEL_LIST = new List<Debt_Model>();
			}
        }

 
        public async Task SaveTransactionData()
        {
            string transaction_filePath = Path.Combine(FileSystem.AppDataDirectory, "data_transaction.json");
			
			string transaction_jsonData = JsonSerializer.Serialize(_TRNSACTION_MODEL_LIST);
			
			await File.WriteAllTextAsync(transaction_filePath, transaction_jsonData);
			
		}

		public async Task SaveDebtData()
		{
			string debt_filePath = Path.Combine(FileSystem.AppDataDirectory, "data_debt.json");
			string debt_jsonData = JsonSerializer.Serialize(_DEBT_MODEL_LIST);
			await File.WriteAllTextAsync(debt_filePath, debt_jsonData);
		}


		public async Task Export(string title)
		{
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 

			if(title == "Transaction")
			{
				string filePath = Path.Combine(desktopPath, "transaction_data.json");
				string json = JsonSerializer.Serialize(_TRNSACTION_MODEL_LIST);
				await File.WriteAllTextAsync(filePath, json);
			}
			else if(title == "Debt")
			{
				string filePath = Path.Combine(desktopPath, "debt_data.json");
				string json = JsonSerializer.Serialize(_TRNSACTION_MODEL_LIST);
				await File.WriteAllTextAsync(filePath, json);
			}
		

		}
        public void Reset()
		{
			_TRNSACTION_MODEL_LIST.Clear();
            _DEBT_MODEL_LIST.Clear();
            SaveTransactionData();
        }


		public void _CURRENCY_TYPE(string CURRENCY)
		{
			_currency_types = CURRENCY;
		}

		public string _CUR()
		{
			return _currency_types != null ? _currency_types : "$";
		}


        public void AddTransaction(string description, string tags, string types, double amount, DateTime date)
        {
            _TRNSACTION_MODEL_LIST.Add(new Transaction_Model(description, tags, types, amount, date));
            SaveTransactionData();  
        }

        public void AddDebt(string debtsource, double amount, DateTime date, string status)
		{
			_DEBT_MODEL_LIST.Add(new Debt_Model(debtsource, amount, date, status));
			SaveDebtData();
		}

		public double getTotalBalance()
		{
			var _INCOME = _TRNSACTION_MODEL_LIST.Where(t => t._types == "Income").Sum(t => t._amount);
			var _DEBT_UNPAID_BALANCE = _DEBT_MODEL_LIST.Where(d => d._debt_status == "Unpaid").Sum(d => d._debtAmount);
			var _DEBT_PAID_BALANCE = _DEBT_MODEL_LIST.Where(d => d._debt_status == "Paid").Sum(d => d._debtAmount);
			var _EXPENSES = _TRNSACTION_MODEL_LIST.Where(t => t._types == "Expense").Sum(t => t._amount);
			var _ALL_CALCULATED_TOTAL_BALANCE = (_INCOME + _DEBT_UNPAID_BALANCE) - (_EXPENSES + _DEBT_PAID_BALANCE);

			return ((double)(_ALL_CALCULATED_TOTAL_BALANCE));
		
		}

		public double getTotalDebt()
		{
            var _TOTAL_DEBT_UNPAID_BALANCE = _DEBT_MODEL_LIST.Where(d => d._debt_status == "Unpaid").Sum(d => d._debtAmount);
            var _TOTAL_DEBT_PAID_BALANCE = _DEBT_MODEL_LIST.Where(d => d._debt_status == "Paid").Sum(d => d._debtAmount);

	
	
			_ALL_CALCULATED_TOTAL_DEBT = _TOTAL_DEBT_UNPAID_BALANCE - _TOTAL_DEBT_PAID_BALANCE;

            return ((double)(_ALL_CALCULATED_TOTAL_DEBT));

        }
	

		
	
		public double getTotalIncome() => _TRNSACTION_MODEL_LIST.Where(t => t._types == "Income").Sum(t => t._amount);
		/*public double getTotalSaving() => _TRNSACTION_MODEL_LIST.Where(t => t._types == "Saving").Sum(t => t._amount);*/
		public double getTotalExpense() => _TRNSACTION_MODEL_LIST.Where(t => t._types == "Expense").Sum(t => t._amount);

      
    }
}
