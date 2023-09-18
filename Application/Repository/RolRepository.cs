using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly JwtAppContext _context;

    public RolRepository(JwtAppContext context) : base(context)
    {
       _context = context;
    }
}
