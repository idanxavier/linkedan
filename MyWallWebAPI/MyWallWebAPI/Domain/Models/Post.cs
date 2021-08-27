using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyWallWebAPI.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [JsonIgnore]
        public List<Like> LikePost { get; set; }
        public int QtdLikes { get; set; }
}
}
