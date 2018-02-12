using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Users.Backend
{
    public class UsersRepository : IUsersRepository, IDisposable
    {
        private readonly string _connString;
        private readonly UserValidator _oUserValidator;

        public UsersRepository(UserValidator oUserValidator)
        {
            _connString = "Data Source=localhost;initial catalog=UsersDb; User ID=sa;Password=sqlpass;Integrated Security=SSPI;";
            _oUserValidator = oUserValidator;

        }

        public string AddUser(User user)
        {
            // add user to database
            string result = Messages.AddUserSuccess;
            if (_oUserValidator == null || !_oUserValidator.IsValid(user, out result))
            {
                return result;
            }
            try
            {
                IEnumerable<string> oUsers = GetRegisteredUsersEmailAddresses();
                if (oUsers.Any(u => u == user.EmailAddress))
                {
                    return Messages.UserExists;
                }
                InsertData(user.EmailAddress, user.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Messages.Error;
            }

            return Messages.AddUserSuccess;
        }

        private IEnumerable<string> GetRegisteredUsersEmailAddresses()
        {
            // define INSERT query with parameters
            string query = "SELECT * FROM dbo.users";
            // create connection and command
            List<string> result = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                    conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(Convert.ToString(reader["EmailAddress"]));
                        }
                    }
            }

            return result.AsEnumerable();
        }

        public void Dispose()
        {
            // do nothing for now
        }

        private void InsertData(string emailAddress, string password)
        {
            // define INSERT query with parameters
            string query = "INSERT INTO dbo.users (EmailAddress, PasswordHash) " +
                           "VALUES (@EmailAddress, @PasswordHash) ";
            
            byte[] passwordHash = CryptoUtilities.CreateHashWithSalt(password);
            // create connection and command
            using (SqlConnection cn = new SqlConnection(_connString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = emailAddress;
                cmd.Parameters.Add("@PasswordHash", SqlDbType.Binary, 64).Value = passwordHash;

                // open connection, execute INSERT, close connection
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}
