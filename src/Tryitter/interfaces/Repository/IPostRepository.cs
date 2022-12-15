using Tryitter_Project.Models;

namespace Tryitter_Project.Repository;
public interface IPostRepository
{
    List<Post?> PostsByStudentId(int id);
    Post? LastPostsByStudentId(int id);
    Post? GetPostById(int id);
    Post? PostMessage(Post post);
    Post? PutMessage(Post post);
    Post? DeleteMessage(Post post);
}