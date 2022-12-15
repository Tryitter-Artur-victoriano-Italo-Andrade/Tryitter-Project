using Microsoft.AspNetCore.Mvc;
using Tryitter_Project.Models;
using System.Collections.Generic;
using Tryitter_Project.Context;
using Tryitter_Project.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Tryitter_Project.Repository;

namespace Tryitter_Project.Test;

public class TestPostController
{
    public TestPostController()
    {
    }

    [Theory]
    [InlineData(0)]
    public async void PostsByStudents(int i)
    {

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.PostsByStudentId(i)).Returns(It.IsAny<List<Post>>());

        var _controller = new PostController(a.Object);
        var b = _controller.GetPostsByStudentId(i);

        a.Verify(x => x.PostsByStudentId(i), Times.Once);
        b.Should().NotBeNull();
    }

    [Fact]
    public async void LastPostsByStudents()
    {

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.LastPostsByStudentId(0)).Returns(It.IsAny<Post>);

        var _controller = new PostController(a.Object);
        var b = _controller.GetLastPostByStudentId(0);

        a.Verify(x => x.LastPostsByStudentId(0), Times.Once);
        b.Should().NotBeNull();
    }

    [Fact]
    public async void GetById()
    {

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.GetPostById(0)).Returns(It.IsAny<Post>);

        var _controller = new PostController(a.Object);
        Post newPost = new() { PostId = 0, Messages = "aa", Image = "aa", StudentId = 0 };
        _controller.Post(newPost);
        var b = _controller.Get(0);

        a.Verify(x => x.GetPostById(0), Times.Once);
        b.Should().NotBeNull();
    }

    [Fact]
    public async void PostPosts()
    {

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.PostMessage(It.IsAny<Post>())).Returns(It.IsAny<Post>());

        var _controller = new PostController(a.Object);
        Post newPost = new() { PostId = 0, Messages = "aa", Image = "aa", StudentId = 0 };
        var b = _controller.Post(newPost);

        a.Verify(x => x.PostMessage(It.IsAny<Post>()), Times.Once);
        b.Should().NotBeNull();
    }
    [Fact]
    public async void PutPosts()
    {

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.PutMessage(It.IsAny<Post>())).Returns(It.IsAny<Post>());

        var _controller = new PostController(a.Object);
        Post newPost = new() { PostId = 0, Messages = "aa", Image = "aa", StudentId = 0 };
        var b = _controller.Put(0, newPost);

        a.Verify(x => x.PutMessage(It.IsAny<Post>()), Times.Once);
        b.Should().NotBeNull();
    }

    [Fact]
    public async void PutPosts_ShouldReturnNull()
    {

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.PutMessage(It.IsAny<Post>())).Returns(It.IsAny<Post>());

        var _controller = new PostController(a.Object);
        Post newPost = new() { PostId = 0, Messages = "aa", Image = "aa", StudentId = 0 };
        var b = _controller.Put(1, newPost);

        a.Verify(x => x.PutMessage(It.IsAny<Post>()), Times.Never);
    }

    [Fact]
    public async void DeletePosts()
    {
        Post newPost = new() { PostId = 0, Messages = "aa", Image = "aa", StudentId = 0 };

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.DeleteMessage(It.IsAny<Post>())).Returns(It.IsAny<Post>());
        a.Setup(x => x.GetPostById(0)).Returns(newPost);

        var _controller = new PostController(a.Object);
        var b = _controller.Delete(0);

        a.Verify(x => x.DeleteMessage(It.IsAny<Post>()), Times.Once);
        b.Should().NotBeNull();
    }

    [Fact]
    public async void DeletePosts_ShouldReturnNull()
    {
        Post newPost = new() { PostId = 0, Messages = "aa", Image = "aa", StudentId = 0 };

        var a = new Mock<IPostRepository>();
        a.Setup(x => x.GetPostById(0)).Returns(newPost);
        a.Setup(x => x.DeleteMessage(It.IsAny<Post>())).Returns(It.IsAny<Post>());

        var _controller = new PostController(a.Object);
        var b = _controller.Delete(1);

        a.Verify(x => x.DeleteMessage(It.IsAny<Post>()), Times.Never);
    }
}

