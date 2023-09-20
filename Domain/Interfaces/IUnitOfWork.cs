namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRolRepository Roles { get; }
    ICompra Compras { get; }
    IEmpleado Empleados { get; }
    IMedicamento Medicamentos { get; }
    IMedicamentoComprado MedicamentosComprados { get; }
    IMedicamentoVendido MedicamentosVendidos { get; }
    IPaciente Pacientes { get; }
    IProveedor Proveedores { get; }
    IVenta Ventas { get; }
    IUserRepository Users { get; }
    Task<int> SaveAsync();
}
