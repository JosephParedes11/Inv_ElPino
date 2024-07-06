using System.ComponentModel.DataAnnotations;

namespace GestionContratos.Models
{
    public class ContratoModel
    {
        public int ContratoID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public int LoteID { get; set; }
        public int ClienteID { get; set; }
        public string PagoTipo { get; set; }
        public int EmpresaID { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal CuotaInicial { get; set; }
        public decimal Igv { get; set; }
        public LoteModel Lote { get; set; }
        public ClienteModel Cliente { get; set; }
        public EmpresaModel Empresa { get; set; }

    }
}
