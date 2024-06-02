using System.Collections.Generic;

namespace GridViewServerSidePagination.App_Code
{
    public class ProfileData
    {
        public List<Profile> GetProfiles(int startRowIndex, int maximumRows, string sortExpression)
        {
            // Replace this with your actual data retrieval code
            List<Profile> profiles = new List<Profile>();
            for (int i = startRowIndex; i < startRowIndex + maximumRows; i++)
            {
                profiles.Add(new Profile
                {
                    ProfileId = i.ToString(),
                    Name = "Name " + i,
                    Address = "Address " + i,
                    Email = "email" + i + "@example.com",
                    Mobile = "123-456-789" + i,
                    IsActive = (i % 2 == 0) ? "Active" : "Inactive"
                });
            }
            return profiles;
        }

        public int GetProfileCount()
        {
            // Replace this with your actual data count code
            return 100; // Example total count
        }
    }
}