namespace GoodStudydotNET.Models.Requests
{
    public class Pesquisa
    {
        public string Nome { get; set; } = "";

        public int EspId { get; set; } = -1;
        public int PrecoMin { get; set; }
        public int PrecoMax { get; set; }


    }
}
