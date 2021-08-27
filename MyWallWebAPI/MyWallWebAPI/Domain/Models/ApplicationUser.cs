using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        List<Post> Posts { get; set; }
        List<Message> Messages { get; set; }
        List<Like> Likes { get; set; }
    }
}