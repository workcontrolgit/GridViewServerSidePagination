using AspNetWebformSample.BusinessLayer.Models;
using System.Collections.Generic;

namespace AspNetWebformSample.DataLayer
{
    /// <summary>
    /// Interface for handling operations related to user profiles in the database.
    /// </summary>
    public interface IProfileRepository
    {
        int TotalRowCount(int startRowIndex, int pageSize, string sortExpression);

        List<UserProfile> GetProfiles(int startRowIndex, int pageSize, string sortExpression);

        void CreateProfile(UserProfile profile);

        void UpdateProfile(UserProfile profile);

        void DeleteProfile(int profileId);

        UserProfile GetProfileById(int profileId);
    }
}