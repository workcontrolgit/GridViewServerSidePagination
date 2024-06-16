using AspNetWebformSample.BusinessLayer.Models;
using AspNetWebformSample.DataLayer;

namespace AspNetWebformSample.BusinessLayer.Services
{
    /// <summary>
    /// Service class for managing user profiles.
    /// </summary>
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// Private field to store an instance of ProfileRepository class.
        /// </summary>
        private readonly ProfileRepository _repository;

        /// <summary>
        /// Constructor for ProfileService class.
        /// Initializes a new instance of ProfileRepository.
        /// </summary>
        public ProfileService()
        {
            _repository = new ProfileRepository();
        }

        /// <summary>
        /// Creates a new user profile by calling the CreateProfile method of the repository.
        /// </summary>
        /// <param name="profile">The user profile to be created</param>
        public void CreateProfile(UserProfile profile)
        {
            _repository.CreateProfile(profile);
        }

        /// <summary>
        /// Updates the user profile using the provided UserProfile object.
        /// </summary>
        /// <param name="profile">The UserProfile object containing the updated profile information.</param>
        public void UpdateProfile(UserProfile profile)
        {
            _repository.UpdateProfile(profile);
        }

        /// <summary>
        /// Retrieves a user profile by its ID from the repository.
        /// </summary>
        /// <param name="profileId">The ID of the user profile to retrieve</param>
        /// <returns>
        /// The user profile with the specified ID
        /// </returns>
        public UserProfile GetProfileById(int profileId)
        {
            return _repository.GetProfileById(profileId);
        }

        /// <summary>
        /// Deletes a profile based on the provided profileId.
        /// </summary>
        /// <param name="profileId">The unique identifier of the profile to be deleted.</param>
        public void DeleteProfile(int profileId)
        {
            _repository.DeleteProfile(profileId);
        }

        /// <summary>
        /// Gets the total number of profiles from the repository.
        /// </summary>
        /// <returns>
        /// The total number of profiles.
        /// </returns>
        public int GetTotalProfiles()
        {
            return _repository.TotalRowCount(0, 0, string.Empty);
        }
    }
}