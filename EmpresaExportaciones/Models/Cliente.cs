using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpresaExportaciones.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "DNI")]
        public int dniCliente { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Display(Name = "NOMBRE")]
        public string nombreCliente { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Display(Name = "APELLIDO")]
        public string apellidoCliente { get; set; }

        public int getId()
        {
            return this.id;
        }
    }
}
