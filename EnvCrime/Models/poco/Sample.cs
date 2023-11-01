using EnvCrime.Infrastructure.Shared.Generics;

namespace EnvCrime.Models.poco
{
    public class Sample : GenericEntity
    {
        public int SampleId { get; set; }

        public string SampleName { get; set; }

        public int ErrandId { get; set; }

		public override bool IsNew()
		{
			return SampleId == 0;
		}
	}
}
