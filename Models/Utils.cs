using Microsoft.Data.SqlClient;

namespace GoodStudydotNET.Models
{
    public static class Utils//vaidar o output que o sql devolve
    {
        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);//o que esta nesta posicao é para converter em string e converter
            }

            return string.Empty;//O mesmo de meter " " para devolver string vazia

        }

        public static decimal SafeGetDecimal(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDecimal(colIndex);//o que esta nesta posicao é para converter em string e converter
            }

            return 0;//O mesmo de meter " " para devolver string vazia

        }

        public static int SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt32(colIndex);//o que esta nesta posicao é para converter em string e converter
            }

            return 0;//O mesmo de meter " " para devolver string vazia

        }

        public static DateTime SafeGetDate(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDateTime(colIndex);//o que esta nesta posicao é para converter em string e converter
            }

            return DateTime.MinValue;//O mesmo de meter " " para devolver string vazia

        }

        public static string SafeGetGuild(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetGuid(colIndex).ToString();//o que esta nesta posicao é para converter em string e converter
            }

            return string.Empty;//O mesmo de meter " " para devolver string vazia

        }

        public static byte[] SafeGetByteArray(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return (byte[])reader[colIndex];//o que esta nesta posicao é para converter em string e converter
            }

            return null;//O mesmo de meter " " para devolver string vazia

        }

    }
}
