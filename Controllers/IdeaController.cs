using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using cbelt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace cbelt.Controllers{
    public class IdeaController: Controller{
        private BeltContext _context;
        public IdeaController(BeltContext context){
            _context = context;
        }
        public IActionResult Dash(){
            if(HttpContext.Session.GetInt32("id") == null){
                return RedirectToAction("Index", "Home");
            }
            List<Idea> i = _context.Ideas.Include(a => a.Creator).Include(a => a.IdeaLikes).ThenInclude(a => a.User).ToList();
            User u = _context.Users.SingleOrDefault(a => a.UserId == (int) HttpContext.Session.GetInt32("id"));
            ViewBag.allIdeas = i;
            ViewBag.user = u;
            return View("Dashboard");
        } 
        public IActionResult AddIdea(Idea model){
            if(ModelState.IsValid){
                Idea i = new Idea{
                    IdeaText = model.IdeaText,
                    CreatorId = (int) HttpContext.Session.GetInt32("id")
                };
                _context.Ideas.Add(i);
                _context.SaveChanges();
            }
                return RedirectToAction("Dash");
        }
        public IActionResult Like(int id){
            IdeaLikes query = _context.IdeaLikes.SingleOrDefault(a => a.IdeaId == id && a.UserId == (int) HttpContext.Session.GetInt32("id"));
            if(query == null){
            IdeaLikes i = new IdeaLikes{UserId = (int) HttpContext.Session.GetInt32("id"),
            Quantity = 1,
            IdeaId = id};
            _context.IdeaLikes.Add(i);
            }
            else{
                query.Quantity++;
            }

            _context.SaveChanges();
            return RedirectToAction("Dash");
        }
        public IActionResult ShowUser(int id){
            User u = _context.Users.Include(a => a.IdeaLikes).ThenInclude(a => a.Idea).ThenInclude(a => a.Creator).SingleOrDefault(a => a.UserId == id);
            int likes = 0;
            foreach(IdeaLikes il in u.IdeaLikes){
                    likes += il.Quantity;
                }
            List<Idea> i = _context.Ideas.Where(a => a.CreatorId == id).ToList();
            ViewBag.likes = likes;
            ViewBag.posts = i.Count;            
            ViewBag.user = u;
            return View();
        }
        public IActionResult ShowIdea(int id){
            Idea i = _context.Ideas.Include(a => a.Creator).Include(a => a.IdeaLikes).ThenInclude(a => a.User).SingleOrDefault(a => a.IdeaId == id);
            ViewBag.idea = i;
            return View();
        }
        public IActionResult Delete(int id){
            Idea i = _context.Ideas.SingleOrDefault(a => a.IdeaId == id);
            _context.Ideas.Remove(i);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }
    }
}