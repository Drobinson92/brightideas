using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using cbelt.Models;

namespace cbelt.Models{
    public class Idea: BaseEntity{
        [Key]
        public int IdeaId {get; set;}
        [Required]
        public string IdeaText {get; set;}
        public int CreatorId {get; set;}
        public User Creator {get; set;}
        public List<IdeaLikes> IdeaLikes {get; set;}
        public Idea(){
            IdeaLikes = new List<IdeaLikes>();
        }
    }
}