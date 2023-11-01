using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Models;
using EnvCrime.Models.poco;

namespace EnvCrime.Infrastructure.Services
{
    public class PictureService : GenericRepository<Picture, int>
    {
        public PictureService(ApplicationDbContext dbCtx) : base(dbCtx) { }
    }
}
