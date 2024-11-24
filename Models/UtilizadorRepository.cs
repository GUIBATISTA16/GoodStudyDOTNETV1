using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace GoodStudydotNET.Models
{
    public class UtilizadorRepository
    {
        private static string connectionString = "data source=DESKTOP-B8E792M\\SQLEXPRESS; initial catalog =GoodStudy; uid=sa; pwd=root;TrustServerCertificate=True";

        public static Utilizador AddUtilizador(Dados dados,Explicador? explicador, Explicando? explicando)
        {
            string sql = "insert into Dados (email, password,tipoDeConta) output INSERTED.ID values(@email,@pass,@tipo)";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    int id;
                    cnn.Open();

                    
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = dados.Email;
                        cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = dados.Password;
                        cmd.Parameters.Add("@tipo", SqlDbType.Int).Value = dados.TipoDeConta;


                        id = (int)cmd.ExecuteScalar();
                        if (id > 0)
                            Console.WriteLine("Row inserted!!");
                        else
                            return null;
                    }

                    if (dados.TipoDeConta == 1 && explicador != null)
                    {
                        sql =
                            "insert into Explicador (nome,descricao,especialidadeId,precoHora,precoMes,precoAno,idDados)" +
                            " values(@nome,@desc,@espId,@precoHora,@precoMes,@precoAno,@idDados)";

                        using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        {
                            cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = explicador.Nome;
                            cmd.Parameters.Add("@desc", SqlDbType.NVarChar).Value = explicador.Descricao;
                            cmd.Parameters.Add("@espId", SqlDbType.Int).Value = explicador.EspecialidadeId;
                            cmd.Parameters.Add("@precoHora", SqlDbType.Int).Value = explicador.PrecoHora;
                            cmd.Parameters.Add("@precoMes", SqlDbType.Int).Value = explicador.PrecoMes;
                            cmd.Parameters.Add("@precoAno", SqlDbType.Int).Value = explicador.PrecoAno;
                            cmd.Parameters.Add("@idDados", SqlDbType.Int).Value = id;


                            cmd.ExecuteNonQuery();
                        }
                        return new Utilizador(dados, explicador);
                    }
                    else if(dados.TipoDeConta == 2 && explicando != null)
                    {
                        sql =
                            "insert into Explicando (nome,idade,distrito,idDados)" +
                            " values(@nome,@idade,@distrito,@idDados)";

                        using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        {
                            cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = explicando.Nome;
                            cmd.Parameters.Add("@idade", SqlDbType.Int).Value = explicando.Idade;
                            cmd.Parameters.Add("@distrito", SqlDbType.NVarChar).Value = explicando.Distrito;
                            cmd.Parameters.Add("@idDados", SqlDbType.Int).Value = id;


                            cmd.ExecuteNonQuery();
                        }
                        return new Utilizador(dados, explicando);
                    }

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro:" + ex.Message);
                    return null;
                }
            }

            return null;

        }

        public static Utilizador Login(Dados dados)
        {
            string sql = "select d.id,d.tipoDeConta from Dados d WHERE email = @email AND password = @pass";
            int id = 0;
            int tipo = 0;

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = dados.Email;
                    cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = dados.Password;

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        id = Utils.SafeGetInt32(reader, 0);
                        tipo = Utils.SafeGetInt32(reader, 1);
                    }
                    reader.Close();
                }

                if (tipo == 1)
                {
                    sql = "select * from Explicador ex join Especialidades es on es.id = ex.especialidadeId WHERE idDados = @idDados";
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add("@idDados", SqlDbType.Int).Value = id;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Explicador e = getExplicador(reader);
                                if (e.Nome != "")
                                {
                                    return new Utilizador(e);
                                }
                                return null;
                            }
                        }
                    }
                }
                else if (tipo == 2)
                {
                    return new Utilizador(getExplicando(cnn, id));
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        internal static List<Explicador> Pesquisa(Pesquisa pesquisa)
        {
            List<Explicador> list = new List<Explicador>();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                if (pesquisa.EspId != -1)
                {
                    string sql = "select * from Explicador ex join Especialidades es on es.id = ex.especialidadeId " +
                                 "WHERE UPPER(ex.nome) like UPPER(@nome+'%') " +
                                 "AND ex.precoHora BETWEEN @precoMin and @precoMax " +
                                 "AND ex.especialidadeId = @espId";
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = pesquisa.Nome;
                        cmd.Parameters.Add("@precoMin", SqlDbType.Int).Value = pesquisa.PrecoMin;
                        cmd.Parameters.Add("@precoMax", SqlDbType.Int).Value = pesquisa.PrecoMax;
                        cmd.Parameters.Add("@espId", SqlDbType.Int).Value = pesquisa.EspId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    Explicador e = getExplicador(reader);
                                    if (e.Nome != "")
                                    {
                                        list.Add(getExplicador(reader));
                                    }
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string sql = "select * from Explicador ex join Especialidades es on es.id = ex.especialidadeId " +
                                 "WHERE UPPER(ex.nome) like UPPER(@nome+'%') " +
                                 "AND ex.precoHora BETWEEN @precoMin and @precoMax";
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        cmd.Parameters.Add("@nome", SqlDbType.NVarChar).Value = pesquisa.Nome;
                        cmd.Parameters.Add("@precoMin", SqlDbType.Int).Value = pesquisa.PrecoMin;
                        cmd.Parameters.Add("@precoMax", SqlDbType.Int).Value = pesquisa.PrecoMax;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    Explicador e = getExplicador(reader);
                                    if (e.Nome != "")
                                    {
                                        list.Add(getExplicador(reader));
                                    }
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                    break;
                                }
                            }
                        }
                    }
                }
                cnn.Close();
            }

            return list;
        }

        private static Explicador getExplicador(SqlDataReader reader)
        {
            Explicador explicador = new Explicador();
            try
            {
                explicador.Id = Utils.SafeGetInt32(reader, 0);
                explicador.Nome = Utils.SafeGetString(reader, 1);
                explicador.Descricao = Utils.SafeGetString(reader, 2);
                explicador.PrecoHora = Utils.SafeGetInt32(reader, 4);
                explicador.PrecoMes = Utils.SafeGetInt32(reader, 5);
                explicador.PrecoAno = Utils.SafeGetInt32(reader, 6);
                explicador.Especialidade = new Especialidade();
                explicador.Especialidade.Id = Utils.SafeGetInt32(reader, 8);
                explicador.Especialidade.Nome = Utils.SafeGetString(reader, 9);
                return explicador;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static Explicando getExplicando(SqlConnection cnn, int id)
        {
            string sql = "select * from Explicando WHERE idDados = @idDados";
            Explicando explicando = new Explicando();
            using (SqlCommand cmd = new SqlCommand(sql, cnn))
            {
                cmd.Parameters.Add("@idDados", SqlDbType.Int).Value = id;


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        explicando.Id = Utils.SafeGetInt32(reader, 0);
                        explicando.Nome = Utils.SafeGetString(reader, 1);
                        explicando.Idade = Utils.SafeGetInt32(reader, 2);
                        explicando.Distrito = Utils.SafeGetString(reader, 3);
                    }
                }

            }
            return explicando;
        }
    }
}
