using System.Text.Json.Serialization;

namespace GoodStudydotNET.Models
{
    public class Utilizador
    {

        public Dados Dados { get; set; }

        public Explicador? Explicador { get; set; }

        public Explicando? Explicando { get; set; }

        public Utilizador(Dados dados, Explicador explicador)
        {
            this.Dados= dados;
            this.Explicador= explicador;
        }

        public Utilizador(Dados dados, Explicando explicando)
        {
            this.Dados = dados;
            this.Explicando = explicando;
        }

        public Utilizador(Explicador explicador)
        {
            this.Explicador = explicador;
        }

        public Utilizador(Explicando explicando)
        {
            this.Explicando = explicando;
        }

        public Utilizador()
        {
        }

        [JsonConstructor]
        public Utilizador(Dados dados, Explicador? explicador, Explicando? explicando)
        {
            this.Dados = dados;
            this.Explicador = explicador;
            this.Explicando = explicando;
        }
    }
}
