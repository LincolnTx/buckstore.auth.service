﻿using Newtonsoft.Json;

namespace buckstore.auth.service.infrastructure.CrossCutting.identity.OAuthIdentity.Facebook.Contracts
{
    public class FacebookUserInfo
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public string FacebookId { get; set; }
    }
}