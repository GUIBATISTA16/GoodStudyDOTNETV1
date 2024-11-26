using System.Text.Json.Serialization;

namespace GoodStudydotNET.Models.Requests
{
    public class Utilizador
    {

        public Dados Dados { get; set; }

        public Explicador? Explicador { get; set; }

        public Explicando? Explicando { get; set; }

        public IFormFile image { get; set; }

        public Utilizador(Dados dados, Explicador explicador)
        {
            Dados = dados;
            Explicador = explicador;
        }

        public Utilizador(Dados dados, Explicando explicando)
        {
            Dados = dados;
            Explicando = explicando;
        }

        public Utilizador(Explicador explicador)
        {
            Explicador = explicador;
        }

        public Utilizador(Explicando explicando)
        {
            Explicando = explicando;
        }

        public Utilizador()
        {
        }

        [JsonConstructor]
        public Utilizador(Dados dados, Explicador? explicador, Explicando? explicando)
        {
            Dados = dados;
            Explicador = explicador;
            Explicando = explicando;
        }
    }
}
