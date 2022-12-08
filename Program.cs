namespace Final;

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
class Program
{
    static void Main(string[] args)
    {
         string connStr = "server=20.172.0.16;user=jrruiz1;database=jrruiz1;port=8080;password=jrruiz1";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
          
            string sql = "Residents";
           
            MySqlCommand cmd = new MySqlCommand(sql, conn);
           
            
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"Username: {rdr[0]} -- Password: {rdr[1]} -- Role: {rdr[2]}");
            }
         
            rdr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        conn.Close();
        Console.WriteLine("Done.");
    }
}


