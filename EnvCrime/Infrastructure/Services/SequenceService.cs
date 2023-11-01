using EnvCrime.Infrastructure.Shared.Generics;
using EnvCrime.Models;
using EnvCrime.Models.poco;

namespace EnvCrime.Infrastructure.Services
{
    public class SequenceService : GenericRepository<Sequence, int>
    {
        public SequenceService(ApplicationDbContext dbCtx) : base(dbCtx) { }

        public int GetNextSequence()
        {
            var lastSequence = Search(seq => seq.Id == 1).First();
            var nextSequenceValue = ++lastSequence.CurrentValue;
            Save(lastSequence);
            return nextSequenceValue;
        }
    }
}
