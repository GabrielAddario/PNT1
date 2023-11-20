using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EmpresaExportaciones.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [StringLength(20)]
        [Display(Name = "NOMBRE")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [StringLength(50)]
        [Display(Name = "DESCRIPCION")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]

        [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo.")]
        [Display(Name = "PRECIO")]
        public double precio { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [Range(0, double.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        [Display(Name = "STOCK")]
        public double stock { get; set; }

        [Required(ErrorMessage = "Debe especificar un valor para {0}.")]
        [EnumDataType(typeof(TipoDeProducto))]
        [Display(Name = "TIPO")]
        public TipoDeProducto tipoDeProducto { get; set; }

    }
}
