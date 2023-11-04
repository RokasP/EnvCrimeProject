using EnvCrime.Infrastructure.Shared.Generics;

namespace EnvCrime.Models.poco
{
    public class Sequence : GenericEntity<int>
    {
        public int Id { get; set; }

        public int CurrentValue { get; set; }

        public override int GetId()
        {
            return Id;
        }
    }
}
