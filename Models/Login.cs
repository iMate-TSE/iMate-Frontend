﻿namespace iMate.Models
{
    class Login
    {
        public Login() { }
        public Login(string username, string password) 
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
