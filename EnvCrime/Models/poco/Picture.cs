using EnvCrime.Infrastructure.Shared.Generics;

namespace EnvCrime.Models.poco
{
    public class Picture : GenericEntity<int>
    {
        public int PictureId { get; set; }

        public string PictureName { get; set; }

        public int ErrandId { get; set; }

        public override int GetId()
        {
            return PictureId;
        }
    }
}
