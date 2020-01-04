using BaseServcieInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TradeServiceInterface.Dtos
{
    public class AccountRechargeDto : BaseAuthDto
    {
        [Required(ErrorMessage = "必须填写操作金额")]
        [Range(-double.MaxValue, double.MaxValue, ErrorMessage = "金额超出范围")]
        public decimal RechargeBalance { get; set; }
    }
}
