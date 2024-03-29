﻿using Core.Entities;

namespace WebApi.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Lastname { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Admin { get; set; }
    }
}
