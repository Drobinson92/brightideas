using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using cbelt.Models;

namespace cbelt.Models{
    public abstract class BaseEntity{}
    public class User: BaseEntity{
        [Key]
        public int UserId {get; set;}
        public string Name {get; set;}
        public string Alias {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}

        public List<IdeaLikes> IdeaLikes {get; set;}
        public User(){
            IdeaLikes = new List<IdeaLikes>();
        }
        
       
    }

}