using Application.Repository;
using Domain.Interfaces;
using Persistence;
namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly FarmaciaContext _context;
    private IRolRepository _roles;
    private IUserRepository _users;
    public UnitOfWork(FarmaciaContext context)
    {
        _context = context;
    }
    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }
    
    private ICompra _compras;
    public ICompra Compras
    {
        get
        {
            if (_compras == null)
            {
                _compras = new CompraRepository(_context);
            }
            return _compras;
        }
    }
    private IEmpleado _empleados;
    public IEmpleado Empleados
    {
        get
        {
            if (_empleados == null)
            {
                _empleados = new EmpleadoRepository(_context);
            }
            return _empleados;
        }
    }
    private IMedicamentoComprado _medicamentosComprados;
    public IMedicamentoComprado MedicamentosComprados
    {
        get
        {
            if (_medicamentosComprados == null)
            {
                _medicamentosComprados = new MedicamentoCompradoRepository(_context);
            }
            return _medicamentosComprados;
        }
    }
    private IMedicamento _medicamentos;
    public IMedicamento Medicamentos
    {
        get
        {
            if (_medicamentos == null)
            {
                _medicamentos = new MedicamentoRepository(_context);
            }
            return _medicamentos;
        }
    }
    private IMedicamentoVendido _medicamentosVendidos;
    public IMedicamentoVendido MedicamentosVendidos
    {
        get
        {
            if (_medicamentosVendidos == null)
            {
                _medicamentosVendidos = new MedicamentoVendidoRepository(_context);
            }
            return _medicamentosVendidos;
        }
    }
    private IPaciente _pacientes;
    public IPaciente Pacientes
    {
        get
        {
            if ( _pacientes == null)
            {
                _pacientes = new PacienteRepository(_context);
            }
            return _pacientes;
        }
    }
    private IProveedor _proveedores;
    public IProveedor Proveedores
    {
        get
        {
            if (_proveedores == null)
            {
                _proveedores = new ProveedorRepository(_context);
            }
            return _proveedores;
        }
    }
    private IVenta _ventas;
    public IVenta Ventas
    {
        get
        {
            if (_ventas == null)
            {
                _ventas = new VentaRepository(_context);
            }
            return _ventas;
        }
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
