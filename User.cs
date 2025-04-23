using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace TestAddUserDB
{
    public class User
    {
        public string Name { get; set; } = "user";
        public DateTime Birthday { get; set; } = new DateTime(2000, 01, 1);
        public string Email { get; set; } = "user@mail.com";
        public string Phone { get; set; } = "+7-999-999-99-99";
    }

    public class UserRepository
    {
        public static User CheckData(User user)
        {
            DateTime today = DateTime.Now;
            DateTime birth = user.Birthday;
            int age = today.Year - birth.Year;
            if (birth.Date > today.AddYears(-age)) age--;

            bool checkMail = Regex.IsMatch(user.Email, @"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]");
            bool checkPhone = Regex.IsMatch(user.Phone, @"\+7-[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}");


            if (!String.IsNullOrEmpty(user.Name) && user.Name.Length < 10 && age >= 18 && checkMail && user.Email.Length < 64 && checkPhone)
            {
                return new User { Name = user.Name, Birthday = user.Birthday, Email = user.Email, Phone = user.Phone };
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        public static bool AddToDB(User user)
        {
            string connectionString = "Server=192.168.10.151;Database=user_demo;User Id=wsr-2;Password=$Dc5d178fd40f#";
            string query = $"INSERT INTO Users (Name, Email, Phone, Birthday) VALUES ('{user.Name}', '{user.Email}', '{user.Phone}', @birth)";

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand insert = new SqlCommand(query, connection);

                connection.Open();

                insert.Parameters.AddWithValue("@birth", user.Birthday);

                int res = insert.ExecuteNonQuery();

                if(res != 0)
                {
                    return true;
                } else
                {
                    return false;
                }

            } catch
            {
                throw new Exception();
            } finally
            {
                if(connection != null)
                {
                    connection.Close();
                }
            }
        }
    }
}