using MoneyMap.Models;

namespace MoneyMap.Application_Service
{
	public interface ITData
	{
		IEnumerable<Debt_Model> GetAllDebt();
		IEnumerable<Transaction_Model> GetTranscationAll();

		Task Init_Data();

        Task SaveTransactionData();

		Task Export(string title);
        void AddTransaction(string description, string tags, string types, double amount, DateTime date);

		double getTotalBalance();

		double getTotalIncome();
		/*	double getTotalSaving();*/

		double getTotalDebt();
		double getTotalExpense();
		void AddDebt(string debtsource, double amount, DateTime date, string status);

		void Reset();

		string _CUR();

		void _CURRENCY_TYPE(string CURRENCY);


	}
}
