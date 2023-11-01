using EnvCrime.Infrastructure.Shared.Generics;

namespace EnvCrime.Models.poco
{
    public class Sequence : GenericEntity
    {
        public int Id { get; set; }

        public int CurrentValue { get; set; }

		public override bool IsNew()
		{
			return Id == 0;
		}
	}
}
