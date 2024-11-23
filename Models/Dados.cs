using System.ComponentModel.DataAnnotations;

namespace GoodStudydotNET.Models
{
    public class Dados
    {

        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int TipoDeConta { get; set; }

        public int IdConta { get; set; }

    }
}
