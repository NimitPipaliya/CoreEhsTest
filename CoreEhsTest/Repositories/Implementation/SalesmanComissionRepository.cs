using CoreEhsTest.Dtos;
using CoreEhsTest.Repositories.Contract;
using Microsoft.Data.SqlClient;

namespace CoreEhsTest.Repositories.Implementation
{
    public class SalesmanComissionRepository:ISalesmanComissionRepository
    {
        private readonly string _connectionString;

        public SalesmanComissionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<SalesComissionDto> GenerateReport()
        {
            var commissions = new List<SalesComissionDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("EXEC GenerateSalesmanCommissionReport", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        commissions.Add(new SalesComissionDto
                        {
                            SalesmanName = reader["SalesmanName"].ToString(),
                            Brand = reader["Brand"].ToString(),
                            FixedCommission = (decimal)reader["FixedCommission"],
                            PercentageCommission = (decimal)reader["PercentageCommission"],
                            Bonus = (decimal)reader["Bonus"],
                            TotalEarnings = (decimal)reader["TotalEarnings"]
                        });
                    }
                }
            }

            return commissions;
        }
    }
}
