using WebFormBoostrap.BusinessLayer.Models;
using WebFormBoostrap.DataLayer;

namespace WebFormBoostrap.BusinessLayer.Services
{
    public class ProfileService
    {
        private readonly ProfileRepository _repository;

        public ProfileService()
        {
            _repository = new ProfileRepository();
        }

        public void CreateProfile(UserProfile profile)
        {
            _repository.CreateProfile(profile);
        }

        public void UpdateProfile(UserProfile profile)
        {
            _repository.UpdateProfile(profile);
        }

        public UserProfile GetProfileById(int profileId)
        {
            return _repository.GetProfileById(profileId);
        }

        public void DeleteProfile(int profileId)
        {
            _repository.DeleteProfile(profileId);
        }
    }
}