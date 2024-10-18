namespace CoreEhsTest.Dtos
{
    public class SalesComissionDto
    {
        public string SalesmanName { get; set; }
        public string Brand { get; set; }
        public decimal FixedCommission { get; set; }
        public decimal PercentageCommission { get; set; }
        public decimal Bonus { get; set; }
        public decimal TotalEarnings { get; set; }
    }
}
