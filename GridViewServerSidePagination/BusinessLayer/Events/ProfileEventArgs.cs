using AspNetWebformSample.BusinessLayer.Models;

// ProfileEventArgs.cs
using System;

namespace AspNetWebformSample.BusinessLayer.Events
{
    public class ProfileEventArgs : EventArgs
    {
        public UserProfile Profile { get; }

        public ProfileEventArgs(UserProfile profile)
        {
            Profile = profile;
        }
    }
}