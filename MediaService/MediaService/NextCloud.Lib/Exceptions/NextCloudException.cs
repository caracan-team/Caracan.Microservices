using System;

namespace NextCloud.Lib.Exceptions
{
    public class NextCloudException : Exception
    {
        public NextCloudException(string msg)
            :base($"NextCloud exception: {msg}")
        {
            
        }
    }
}