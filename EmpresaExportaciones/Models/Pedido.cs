using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmpresaExportaciones.Models
{
    public class Pedido
    {
        [Key]
        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID PEDIDO")]
        public int id { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0.")]
        [Display(Name = "CLIENTE")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "CLIENTE")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0.")]
        [Display(Name = "PRODUCTO")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "PRODUCTO")]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0.")]
        [Display(Name = "CANTIDAD")]
        public int cantProducto { get; set; }


        [Range(1, double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a 0.")]
        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Display(Name = "PRECIO")]
        public int precio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Display(Name = "FECHA")]
        public DateTime fecha { get; set; }

        [EnumDataType(typeof(Estado))]
        [Display(Name = "ESTADO")]
        public Estado estado { get; set; }
    }
}
