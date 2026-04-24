using Test4.Models.Enums;

namespace Test4.Models.Models
{
    public class Calculator
    {
        public decimal Number1 { get; set; }

        public decimal Number2 { get; set; }

        public CalcOperation Operation { get; set; }

        public decimal Answer { get; set; }
    }
}