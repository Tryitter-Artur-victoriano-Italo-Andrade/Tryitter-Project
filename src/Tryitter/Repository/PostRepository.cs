using Microsoft.AspNetCore.Mvc;
using Tryitter_Project.Models;
using System.Collections.Generic;
using Tryitter_Project.Context;
using System;
using Microsoft.EntityFrameworkCore;

namespace Tryitter_Project.Repository;
public class PostRepository : IPostRepository
{
    private TryitterDbContext _context;
    public PostRepository(TryitterDbContext context)
    {
        _context = context;
    }

    public List<Post?> PostsByStudentId(int id)
    {
        var posts = _context.Posts.Include(x => x.StudentId).Where(x => x.StudentId == id).AsNoTracking().ToList();
        return posts;
    }

    public Post? LastPostsByStudentId(int id)
    {
        var posts = _context.Posts.AsNoTracking().LastOrDefault(x => x.StudentId == id);
        return posts;
    }
    public Post? GetPostById(int id)
    {
        var posts = _context.Posts.AsNoTracking().FirstOrDefault(post => post.PostId == id);
        return posts;
    }
    public Post? PostMessage(Post post)
    {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
    }
    public Post? PutMessage(Post post)
    {
        try
        {
        _context.Entry(post).State = EntityState.Modified;
        _context.SaveChanges();   
        return post;  
        }
        catch (System.Exception)
        {   
            throw;
        }
    }
    public Post? DeleteMessage(Post post)
    {
        try
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return post;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

}

