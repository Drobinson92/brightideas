using System;
using System.ComponentModel.DataAnnotations;
namespace cbelt.Models{
    public class IdeaLikes: BaseEntity{
        public int IdeaLikesId {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public int IdeaId {get; set;}
        public Idea Idea {get; set;}
        public int Quantity {get; set;}

    }
}