using System.ComponentModel.DataAnnotations;

namespace GoodStudydotNET.Models
{
    public class Explicador
    {

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required]
        public int EspecialidadeId { get; set; }

        [Required]
        public int PrecoHora { get; set; }
        public int PrecoMes { get; set; }
        public int PrecoAno { get; set; }

        public Especialidade Especialidade { get; set; }

        public Explicador() { }

        // Construtor parametrizado
        public Explicador(
            int id,
            string nome,
            string descricao,
            int especialidadeId,
            int precoHora,
            int precoMes,
            int precoAno,
            Especialidade especialidade)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            EspecialidadeId = especialidadeId;
            PrecoHora = precoHora;
            PrecoMes = precoMes;
            PrecoAno = precoAno;
            Especialidade = especialidade;
        }
    }
}
