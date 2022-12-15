using Microsoft.AspNetCore.Mvc;
using Tryitter_Project.Models;
using System.Collections.Generic;
//using Tryitter_Project.Context;
using System;
//using Microsoft.EntityFrameworkCore;
using Tryitter_Project.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Tryitter_Project.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
  private IPostRepository _repository;


  public PostController(IPostRepository repository)
  {
    _repository = repository;
  }

  [HttpGet("PostsByStudentId/{id}", Name = "PostsByStudentId")]
  public ActionResult<IEnumerable<Post>> GetPostsByStudentId(int id)
  {
    var posts = _repository.PostsByStudentId(id);
    if (posts is null)
    {
      return NotFound("Nenhum Post Encontrado");
    }
    return Ok(posts);
  }

  [HttpGet("LastPostsByStudentId/{id}", Name = "LastPostsByStudentId")]
  public ActionResult<Post> GetLastPostByStudentId(int id)
  {
    var posts = _repository.LastPostsByStudentId(id);
    if (posts is null)
    {
      return NotFound("Nenhum Post Encontrado");
    }
    return Ok(posts);
  }

  [HttpGet("PostById/{id}", Name = "PostById")]
  public ActionResult<Post> Get(int id)
  {
    var posts = _repository.GetPostById(id);
    if (posts is null)
    {
      return NotFound("Post não Encontrado");
    }
    return Ok(posts);
  }

  [HttpPost]
  [Authorize(Policy = "student")]
  public ActionResult Post(Post post)
  {
    if (post is null) return BadRequest();
    _repository.PostMessage(post);
    return new CreatedAtRouteResult("PostById", new { id = post.PostId }, post);
  }

  [HttpPut("{id:int}")]
  [Authorize(Policy = "student")]

  public ActionResult Put(int id, Post post)
  {
    if (id != post.PostId) return BadRequest();
    _repository.PutMessage(post);
    return Ok(post);
  }

  [HttpDelete("{id:int}")]
  [Authorize(Policy = "student")]

  public ActionResult Delete(int id)
  {
    Post? post = _repository.GetPostById(id);
    if (post is null)
    {
      return NotFound("Post não Encontrado");
    }
    _repository.DeleteMessage(post);
    return Ok(post);
  }

}