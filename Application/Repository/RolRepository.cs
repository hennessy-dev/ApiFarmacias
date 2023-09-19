using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly FarmaciaContext _context;

    public RolRepository(FarmaciaContext context) : base(context)
    {
       _context = context;
    }
}
