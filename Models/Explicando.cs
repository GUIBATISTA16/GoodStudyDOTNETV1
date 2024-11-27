using System.ComponentModel.DataAnnotations;

namespace GoodStudydotNET.Models
{
    public class Explicando
    {

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int Idade { get; set; }

        [Required]
        public string Distrito { get; set; }

        public byte[]? imageData { get; set; }

        public string? imageType { get; set; }

        public string? imageName { get; set; }

        public Explicando() { }

        public Explicando(int id, string nome, int idade, string distrito, byte[] imageData, string imageType, string imageName)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            Distrito = distrito;
            this.imageData = imageData;
            this.imageType = imageType;
            this.imageName = imageName;
        }
    }
}
