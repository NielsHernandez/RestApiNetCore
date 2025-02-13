namespace NZWalks.api.Models.Domain
{
    public class Walk
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        //this model will have FK relate on the DB

        //public Guid DifficultyId  { get; set; }


        //public Guid RegionId { get; set; }

        //navigation properties

        public Difficulty Difficulty { get; set; }

        public Region Region { get; set; }


    }
}
