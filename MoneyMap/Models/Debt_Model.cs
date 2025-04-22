using System.Text.Json.Serialization;

namespace MoneyMap.Models
{
	public class Debt_Model
	{
		[JsonPropertyName("_debtSource")]
		public string _debtSource { get; set; }

		[JsonPropertyName("_debtAmount")]
		public double _debtAmount { get; set; }

		[JsonPropertyName("_debtDate")]
		public DateTime _debtDate { get; set; }

		[JsonPropertyName("_debt_status")]
		public string _debt_status { get; set; }

		[JsonConstructor]
		public Debt_Model(string _debtSource, double _debtAmount, DateTime _debtDate, string _debt_status)
		{
			this._debtSource = _debtSource;
			this._debtAmount = _debtAmount;
			this._debtDate = _debtDate;
			this._debt_status = _debt_status;
		}
	}



}
