using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(JwtAppContext context) : base(context)
    {
    }
}
