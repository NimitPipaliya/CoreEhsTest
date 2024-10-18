using CoreEhsTest.Dtos;
using CoreEhsTest.Models;
using CoreEhsTest.Repositories.Contract;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Transactions;

namespace CoreEhsTest.Repositories.Implementation
{
    public class CarModelRepository:ICarModelRepository
    {
        private readonly string _connectionString;

        public CarModelRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable GetAllCarModels(string ?searchTerm)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetAllRecords", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GETALLRECORDS");
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        public int AddCarModel(CarModelDto model, List<byte[]> imageDataList)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CarModelManager", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "CREATE");
                        cmd.Parameters.AddWithValue("@Brand", model.Brand);
                        cmd.Parameters.AddWithValue("@Class", model.Class);
                        cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                        cmd.Parameters.AddWithValue("@ModelCode", model.ModelCode);
                        cmd.Parameters.AddWithValue("@Description", model.Description);
                        cmd.Parameters.AddWithValue("@Features", model.Features);
                        cmd.Parameters.AddWithValue("@Price", model.Price);
                        cmd.Parameters.AddWithValue("@ManufactureDate", model.ManufactureDate);
                        cmd.Parameters.AddWithValue("@Active", model.Active);
                        cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);

                        var carIdParam = new SqlParameter("@CarID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(carIdParam);

                        cmd.ExecuteNonQuery();
                        int carId = (int)carIdParam.Value;

                        foreach (var imageData in imageDataList)
                        {
                            using (SqlCommand imgCmd = new SqlCommand("INSERT INTO CarImages (CarID, ImageData) VALUES (@CarID, @ImageData)", conn, transaction))
                            {
                                imgCmd.Parameters.AddWithValue("@CarID", carId);
                                imgCmd.Parameters.AddWithValue("@ImageData", SqlDbType.VarBinary).Value = imageData;
                                imgCmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        return carId;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateCarModelByCode(UpdateCarModelDto model, List<byte[]> imageDataList)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand("sp_CarModelManager", conn,transaction))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UPDATE");
                    cmd.Parameters.AddWithValue("@CarID", model.CarID);
                    cmd.Parameters.AddWithValue("@Brand", model.Brand);
                    cmd.Parameters.AddWithValue("@Class", model.Class);
                    cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                    cmd.Parameters.AddWithValue("@ModelCode", model.ModelCode);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Features", model.Features);
                    cmd.Parameters.AddWithValue("@Price", model.Price);
                    cmd.Parameters.AddWithValue("@ManufactureDate", model.ManufactureDate);
                    cmd.Parameters.AddWithValue("@Active", model.Active);
                    cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);

                    cmd.ExecuteNonQuery();

                    foreach (var imageData in imageDataList)
                    {
                        using (SqlCommand imgCmd = new SqlCommand("INSERT INTO CarImages (CarID, ImageData) VALUES (@CarID, @ImageData)", conn, transaction))
                        {
                            imgCmd.Parameters.AddWithValue("@CarID", model.CarID);
                            imgCmd.Parameters.AddWithValue("@ImageData", SqlDbType.VarBinary).Value = imageData;
                            imgCmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        public void DeleteCarModel(string modelCode)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_CarModelManager", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DELETE");
                    cmd.Parameters.AddWithValue("@ModelCode", modelCode);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
