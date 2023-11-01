using EnvCrime.Infrastructure.Shared.Generics;

namespace EnvCrime.Models.poco
{
    public class Picture : GenericEntity
    {
        public int PictureId { get; set; }

        public string PictureName { get; set; }

        public int ErrandId { get; set; }

		public override bool IsNew()
		{
			return PictureId == 0;
		}
	}
}
