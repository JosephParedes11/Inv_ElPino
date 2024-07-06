namespace GestionContratos.Models
{
    public class LoteModel
    {
        public int LoteID { get; set; }
        public int UrbanizacionID { get; set; }
        public string Manzana { get; set; }
        public string Letra { get; set; }
        public decimal MetrosCuadrados { get; set; }
        public decimal PrecioPorMetro { get; set; }

        // Propiedad de navegación
        public UrbanizacionModel Urbanizacion { get; set; }
    }
}
