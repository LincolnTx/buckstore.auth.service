﻿using System.Collections.Generic;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.Responses
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}