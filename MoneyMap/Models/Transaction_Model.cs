using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoneyMap.Models
{
    public class Transaction_Model
    {
        [JsonPropertyName("_description")]
        public string _description { get; set; }

        [JsonPropertyName("_tags")]
        public string _tags { get; set; }

        [JsonPropertyName("_types")]
        public string _types { get; set; }

        [JsonPropertyName("_amount")]
        public double _amount { get; set; }

        [JsonPropertyName("_date")]
        public DateTime _date { get; set; }

        [JsonConstructor]
        public Transaction_Model(string _description, string _tags, string _types, double _amount, DateTime _date)
        {
            this._description = _description;
            this._tags = _tags;
            this._types = _types;
            this._amount = _amount;
            this._date = _date;
        }
    }

}
