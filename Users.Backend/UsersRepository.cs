using System;
using System.Data;
using System.Data.SqlClient;

namespace Users.Backend
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connString;

        public UsersRepository()
        {
            _connString = "Data Source=localhost;initial catalog=UsersDb; User ID=sa;Password=sqlpass;Integrated Security=SSPI;";
        }

        public string AddUser(User user)
        {
            // add user to database
            if (string.IsNullOrEmpty(user?.EmailAddress)
                ||
                string.IsNullOrEmpty(user.Password))
                return Messages.UserInformationInvalid;
            try
            {
                InsertData(_connString, user.EmailAddress, user.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }

            return Messages.AddUserSuccess;
        }

        private void InsertData(string connectionString, string emailAddress,  string password)
        {
            // define INSERT query with parameters
            string query = "INSERT INTO dbo.users (EmailAddress, PasswordValue) " +
                           "VALUES (@EmailAddress, @PasswordValue) ";

            // create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = emailAddress;
                cmd.Parameters.Add("@PasswordValue", SqlDbType.VarChar, 50).Value = password;

                // open connection, execute INSERT, close connection
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}
