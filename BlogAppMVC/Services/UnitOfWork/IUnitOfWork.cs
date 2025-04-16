using BlogAppMVC.Context.Entities;
using BlogAppMVC.Services.UnitOfWork;


namespace SchoolManagement.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        IRepository<Post> Post { get; }
        IRepository<User> User { get; }
        IRepository<Comment> Comment { get; }
        IRepository<Category> Category { get; }

    }
}
