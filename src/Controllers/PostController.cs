using Microsoft.AspNetCore.Mvc;
using Tryitter_Project.Models;
using System.Collections.Generic;
using Tryitter_Project.Context;
using System;
using Microsoft.EntityFrameworkCore;

namespace Tryitter_Project.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly TryitterDbContext _context;


    public PostController (TryitterDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int}")]
    public ActionResult<IEnumerable<Post>> GetPostsByStudentId(int id)
    {
        //var posts = _context.Posts.Include(x => x.StudentId).Where(x => x.StudentId == id).AsNoTracking().ToList(); 
        var posts = _context.Posts.Where(x => x.StudentId == id).AsNoTracking().ToList();
        if (posts is null)
        {
            return NotFound("Nenhum Post Encontrado");
        }
        return posts;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Post> GetLastPostByStudentId(int id)
    {
        var posts = _context.Posts.AsNoTracking().LastOrDefault(x => x.StudentId == id);
        if (posts is null)
        {
            return NotFound("Nenhum Post Encontrado");
        }
        return posts;
    }

    [HttpGet("{id:int}", Name="PostById")]
    public ActionResult<Post> Get(int id)
    {
        var posts = _context.Posts.AsNoTracking().FirstOrDefault(post => post.PostId == id);
        if(posts is null)
        {
            return NotFound("Post não Encontrado");
        }
        return posts;
    }

    [HttpPost]
    public ActionResult Post(Post post)
    {
        if (post is null) return BadRequest();
        _context.Posts.Add(post);
        _context.SaveChanges();

        return new CreatedAtRouteResult("PostById", new { id = post.PostId }, post);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Post post)
    {
        if ( id != post.PostId) return BadRequest();

        _context.Entry(post).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(post);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var post = _context.Posts.FirstOrDefault(post => post.PostId == id);
        if (post is null)
        {
            return NotFound("Post não Encontrado");
        }
        _context.Posts.Remove(post);
        _context.SaveChanges();

        return Ok(post); 
    }

} 