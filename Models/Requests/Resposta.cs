namespace GoodStudydotNET.Models.Requests
{
    public class Resposta
    {
        public string Answer { get; set; }

        public Pedido Pedido { get; set; }

        public Resposta() { }

        public Resposta(string answer, Pedido pedido)
        {
            Answer = answer;
            Pedido = pedido;
        }
    }
}
