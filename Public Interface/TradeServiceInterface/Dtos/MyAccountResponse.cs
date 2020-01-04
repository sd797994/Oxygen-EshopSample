using System;
using System.Collections.Generic;
using System.Text;

namespace TradeServiceInterface.Dtos
{
    public class MyAccountResponse
    {
        public decimal Balance { get; set; }
        public List<BalanceRecord> Records { get; set; }
    }
    public class BalanceRecord
    {
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
