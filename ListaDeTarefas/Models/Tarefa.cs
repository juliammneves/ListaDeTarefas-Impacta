using System.ComponentModel.DataAnnotations;

namespace ListaDeTarefas.Models
{
    public class Tarefa
    {
        public int ID { get; set; }

        [Required]
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Status { get; set; } = "A fazer";

        public DateOnly DataInicio { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DataConclusao { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime DataInicio { get; set; } = DateTime.Now;

        //[DataType(DataType.Date)]
        //public DateTime? DataConclusao { get; set; }
    }
}
