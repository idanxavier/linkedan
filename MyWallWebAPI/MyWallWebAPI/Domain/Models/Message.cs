using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWallWebAPI.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public ApplicationUser Sender { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Receiver { get; set; }
        public string ReceiverId { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
    }
}
