using System;
using System.ComponentModel.DataAnnotations;

namespace GestionContratos.Models
{
    public class ClienteModel
    {
        public int ClienteID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
