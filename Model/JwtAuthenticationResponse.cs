using System;

namespace Parking_System
{
    [Serializable]
    public class JwtAuthenticationResponse
    {     
            public string token { get; set; }
            public string email { get; set; }
            public int expires_in { get; set; }
    }
    
}
