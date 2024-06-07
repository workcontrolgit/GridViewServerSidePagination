using System.Collections.Generic;

namespace WebFormBoostrap.App_Code
{
    public class ProfileData2
    {
        public List<UserProfile> GetProfiles(int startRowIndex, int maximumRows, string sortExpression)
        {
            // Replace this with your actual data retrieval code
            List<UserProfile> profiles = new List<UserProfile>();
            for (int i = startRowIndex; i < startRowIndex + maximumRows; i++)
            {
                profiles.Add(new UserProfile
                {
                    ProfileId = i,
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