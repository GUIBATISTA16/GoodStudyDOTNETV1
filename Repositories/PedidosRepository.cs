

using System.Data;
using System.Data.SqlTypes;
using GoodStudydotNET.Models;
using GoodStudydotNET.Models.Requests;
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

        internal static List<Pedido> GetPedidosExplicador(int id)
        {
            List<Pedido> list = new List<Pedido>();
            string sql = "select * from Pedidos p join Explicando ex on ex.id = p.explicando where explicador = @id AND estado = 'Waiting'";
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
                                p.Id = Utils.SafeGetInt32(reader, 0);
                                p.ExplicadorId = Utils.SafeGetInt32(reader, 1);
                                p.ExplicandoId = Utils.SafeGetInt32(reader, 2);
                                p.Estado = Utils.SafeGetString(reader, 3);
                                p.Texto = Utils.SafeGetString(reader, 4);
                                p.Data = Utils.SafeGetDate(reader, 5);
                                Explicando e = new Explicando();
                                e.Id = Utils.SafeGetInt32(reader, 6);
                                e.Nome = Utils.SafeGetString(reader, 7);
                                e.Idade = Utils.SafeGetInt32(reader, 8);
                                e.Distrito = Utils.SafeGetString(reader, 9);
                                p.Explicando = e;
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

        internal static Pedido AnswerPedido(Resposta answer)
        {
            string sql = "update Pedidos set estado = @answer output inserted.id where id = @id";
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        if (answer.Answer != "Accepted" && answer.Answer != "Rejected")
                        {
                            return null;
                        }
                        cmd.Parameters.Add("@answer", SqlDbType.NVarChar).Value = answer.Answer;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = answer.Pedido.Id;

                        int id = (int)cmd.ExecuteScalar();

                        if (id != 0)
                        {
                            answer.Pedido.Estado = answer.Answer;
                            return answer.Pedido;
                        }
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
