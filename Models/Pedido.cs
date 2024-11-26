using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GoodStudydotNET.Models
{
    public class Pedido
    {

        public int Id { get; set; }
        public int ExplicadorId { get; set; }
        public int ExplicandoId { get; set; }
        public string Estado { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }

        public Explicando Explicando { get; set; }

        public Pedido () { }

        [JsonConstructor]
        Pedido (int id, int explicadorId, int explicandoId, string estado, string texto, DateTime data)
        {
            Id = id;
            ExplicadorId = explicadorId;
            ExplicandoId = explicandoId;
            Estado = estado;
            Texto = texto;
            Data = data;
        }
    }
}
