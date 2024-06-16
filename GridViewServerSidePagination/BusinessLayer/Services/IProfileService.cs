using AspNetWebformSample.BusinessLayer.Models;

namespace AspNetWebformSample.BusinessLayer.Services
{
    /// <summary>
    /// Interface for managing user profiles.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Creates a new user profile.
        /// </summary>
        /// <param name="profile">The user profile to be created</param>
        void CreateProfile(UserProfile profile);

        /// <summary>
        /// Updates the user profile using the provided UserProfile object.
        /// </summary>
        /// <param name="profile">The UserProfile object containing the updated profile information.</param>
        void UpdateProfile(UserProfile profile);

        /// <summary>
        /// Retrieves a user profile by its ID.
        /// </summary>
        /// <param name="profileId">The ID of the user profile to retrieve</param>
        /// <returns>
        /// The user profile with the specified ID
        /// </returns>
        UserProfile GetProfileById(int profileId);

        /// <summary>
        /// Deletes a profile based on the provided profileId.
        /// </summary>
        /// <param name="profileId">The unique identifier of the profile to be deleted.</param>
        void DeleteProfile(int profileId);

        /// <summary>
        /// Gets the total number of profiles.
        /// </summary>
        /// <returns>
        /// The total number of profiles.
        /// </returns>
        int GetTotalProfiles();
    }
}