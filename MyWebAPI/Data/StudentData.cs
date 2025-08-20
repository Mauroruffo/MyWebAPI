using MyWebAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
namespace MyWebAPI.Data
{
    public class StudentData
    {
        private readonly String connection;

        public StudentData (IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("SQLConnectionString");
        }

        public async Task<List<Student>> StudentList()
        {
            // Create list to return
            List<Student> students = new List<Student>();
            // Create connection to SQL DB
            using (var con = new SqlConnection(connection))
            {
                // Open DB connection asynchronously
                await con.OpenAsync();
                // Define what procedure to use from connection
                SqlCommand cmd = new SqlCommand("usp_StudentList", con);
                // Define what type of command to run (Stored Procedure)
                cmd.CommandType = CommandType.StoredProcedure;
                // Execute command asynchronously
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    // Read results by rows
                    while (await reader.ReadAsync())
                    {
                        // Pass information from reader to Student List
                        students.Add(new Student 
                        { 
                            Id = Convert.ToInt32(reader["ID"]),
                            FullName = reader["FullName"].ToString()
                        });
                    }
                }
            }
            return students;
        }

    }
}
