using System;
using System.Collections.Generic;
using System.Text;

namespace BaseServcieInterface
{
    public class BaseAuthDto
    {
        public Guid UserId { get; set; }
        public string Account { get; set; }
        public decimal Balance { get; set; }
    }
}
