namespace NZWalks.api.Models.DTO
{
    public class AddWalkRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        //this model will have FK relate on the DB

        public Guid DifficultyId { get; set; }


        public Guid RegionId { get; set; }

    }
}
