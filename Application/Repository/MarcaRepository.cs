using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class MarcaRepository : GenericRepository<Marca>, IMarcaRepository
{
    public MarcaRepository(JwtAppContext context) : base(context)
    {
    }
}
