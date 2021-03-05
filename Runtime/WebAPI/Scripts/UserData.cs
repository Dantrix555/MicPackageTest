using System;

namespace WebAPI.Scripts
{
    [Serializable]
    public struct UserData
    {
        public string uid;
        public string email;
        public string firstname;
        public string lastname;
        public string sess_token;
        public int status;
        public bool verified;
    }
}