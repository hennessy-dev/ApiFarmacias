namespace ApiJwt.Dtos
{
    public class MedicamentoXProveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int ProovedorId { get; set; }

        public ProveedorDto Proveedor { get; set; }
    }
}