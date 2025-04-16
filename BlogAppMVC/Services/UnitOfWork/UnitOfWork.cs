using BlogAppMVC.Context;
using BlogAppMVC.Context.Entities;
using BlogAppMVC.Services.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace SchoolManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _myContext;
        public UnitOfWork(BlogContext myContext)
        {
            _myContext = myContext;
        }
        private Repository<Post> _postRepository;
        private Repository<User> _userRepository;
        private Repository<Comment> _commentRepository;
        private Repository<Category> _categoryRepository;
        public IRepository<Post> Post {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new Repository<Post>(_myContext);
                }
                return _postRepository;
            }
        }

        public IRepository<User> User
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new Repository<User>(_myContext);
                }
                return _userRepository;
            }
        }

        public IRepository<Comment> Comment
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new Repository<Comment>(_myContext);
                }
                return _commentRepository;
            }
        }
        public IRepository<Category> Category
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new Repository<Category>(_myContext);
                }
                return _categoryRepository;
            }
        }


        public void Dispose()
        {
            _myContext.Dispose();
        }
    }
}
