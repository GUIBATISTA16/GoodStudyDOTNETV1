

using System.Data;
using System.Data.SqlTypes;
using GoodStudydotNET.Models;
using Microsoft.Data.SqlClient;

namespace GoodStudydotNET.Repositories
{
    public class PedidosRepository
    {
        private static string connectionString = "data source=DESKTOP-B8E792M\\SQLEXPRESS; initial catalog =GoodStudy; uid=sa; pwd=root;TrustServerCertificate=True";

        public static Pedido SavePedido(Pedido pedido)
        {
            string sql = "insert into Pedidos (explicador, explicando,estado,texto,data) output INSERTED.id" +
                         " values(@explicador,@explicando,@estado,@texto,@data)";
            pedido.Data = DateTime.Now;
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add("@explicador",SqlDbType.Int).Value = pedido.ExplicadorId;
                        cmd.Parameters.Add("@explicando", SqlDbType.Int).Value = pedido.ExplicandoId;
                        cmd.Parameters.Add("@estado", SqlDbType.NVarChar).Value = pedido.Estado;
                        cmd.Parameters.Add("@texto", SqlDbType.NVarChar).Value = pedido.Texto;
                        cmd.Parameters.Add("@data", SqlDbType.DateTime).Value = pedido.Data.ToString("yyyy-MM-dd HH:mm:ss.fff");

                        int id = (int)cmd.ExecuteScalar();

                        pedido.Id=id;
                        return pedido;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static List<Pedido> GetPedidosExplicando(int id)
        {
            List<Pedido> list = new List<Pedido>();
            string sql = "select * from Pedidos where explicando = @id AND estado = 'Waiting'";
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        SqlDataReader reader = cmd.ExecuteReader();

                        using (reader)
                        {
                            while (reader.Read())
                            {
                                Pedido p = new Pedido();
                                p.Id = Utils.SafeGetInt32(reader,0);
                                p.ExplicadorId = Utils.SafeGetInt32(reader, 1);
                                p.ExplicandoId = Utils.SafeGetInt32(reader, 2);
                                p.Estado = Utils.SafeGetString(reader, 3);
                                p.Texto = Utils.SafeGetString(reader, 4);
                                p.Data = Utils.SafeGetDate(reader, 5);

                                list.Add(p);
                            }
                        }
                    }
                    cnn.Close();
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return list;
            }
        }
    }
}
