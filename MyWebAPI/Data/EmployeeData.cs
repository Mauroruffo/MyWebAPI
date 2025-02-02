using MyWebAPI.Models;
using System.Data;
// using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
namespace MyWebAPI.Data
{
    public class EmployeeData
    {
        private readonly String connection;

        public EmployeeData(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("SQLConnectionString")!;
        }

        public async Task<List<Employee>> EmployeeList()
        {
            List<Employee> list = new List<Employee>();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_EmployeeList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Employee
                        {
                            IdEmployee = Convert.ToInt32(reader["Id_Employee"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Salary = Convert.ToDouble(reader["Salary"]),
                            HiredDate = reader["HiredDate"].ToString()
                        });

                    }
                }
            }
            return list;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            Employee emp = new Employee();

            using (var con = new SqlConnection(connection))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_GetEmployee", con);
                cmd.Parameters.AddWithValue("EmployeeId", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        emp = new Employee
                        {
                            IdEmployee = Convert.ToInt32(reader["Id_Employee"]),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Salary = Convert.ToDouble(reader["Salary"]),
                            HiredDate = reader["HiredDate"].ToString()
                        };

                    }
                }
            }
            return emp;
        }

        public async Task<bool> CreateEmployee(Employee emp)
        {
            bool response = true;

            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("usp_CreateEmployee", con);
                cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@HiredDate", emp.HiredDate);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    response = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    response = false;
                }
            }
            return response;
        }

        public async Task<bool> EditEmployee(Employee emp)
        {
            bool response = true;

            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("usp_EditEmployee", con);
                cmd.Parameters.AddWithValue("@EmployeeId", emp.IdEmployee);
                cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@HiredDate", emp.HiredDate);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    response = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    response = false;
                }
            }
            return response;
        }

        public async Task<bool> DeleteEmployee(int Id)
        {
            bool response = true;

            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("usp_DeleteEmployee", con);
                cmd.Parameters.AddWithValue("@EmployeeId", Id);

                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    response = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    response = false;
                }
            }
            return response;
        }

    }
}
