namespace ShareVideosAPI.Services.Database.Seeds
{
    public class SeedData : ISeedData
    {
        private readonly IUnitOfWork _unitOfWork;

        public SeedData(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ApplySeeds()
        {
            CategorySeeds categorySeeds = new(_unitOfWork);
            categorySeeds.ApplySeed();
        }
    }
}
