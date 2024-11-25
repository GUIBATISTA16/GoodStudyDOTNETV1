using GoodStudydotNET.Models;
using Microsoft.Data.SqlClient;

namespace GoodStudydotNET.Repositories
{
    public class EspecialidadeRepository
    {

        private static string connectionString = "data source=DESKTOP-B8E792M\\SQLEXPRESS; initial catalog =GoodStudy; uid=sa; pwd=root;TrustServerCertificate=True";

        public static List<Especialidade> GetEspecialidades()
        {
            List<Especialidade> _especialidades = new List<Especialidade>();
            try
            {
                SqlConnection sqlconn = new SqlConnection(connectionString);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Select * from Especialidades";
                sqlCmd.Connection = sqlconn;
                sqlconn.Open();

                SqlDataReader reader = sqlCmd.ExecuteReader();


                while (reader.Read())
                {
                    Especialidade e = new Especialidade();
                    e.Id = Utils.SafeGetInt32(reader, 0);
                    e.Nome = Utils.SafeGetString(reader, 1);
                    _especialidades.Add(e);
                }
                sqlconn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _especialidades;
        }
    }
}
