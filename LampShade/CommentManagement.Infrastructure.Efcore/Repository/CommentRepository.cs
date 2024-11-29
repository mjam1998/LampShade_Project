

using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using CommentManagement.Application.Contracts.CommentAppContract;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.Efcore;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.Repository
{
    public class CommentRepository : RepositoryBase<long, Comment>, ICommentRepository
    {
       private readonly CommentContext _context;

        public CommentRepository(CommentContext context):base(context) 
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Select(x => new CommentViewModel 
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Message = x.Message,
                Website=x.Website,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                OwnerId=x.OwnerId,
                Type=x.Type,
                CommentDate=x.CreationDate.ToFarsi()
                
            });
            if(!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query=query.Where(x=>x.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }
            return query.OrderByDescending(x => x.Id).ToList(); 
        }
    }
}
