namespace GestionContratos.Models
{
    public class PaginacionViewModel<T>
    {
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalItems { get; set; }
        public List<T> Items { get; set; }
    }
}
