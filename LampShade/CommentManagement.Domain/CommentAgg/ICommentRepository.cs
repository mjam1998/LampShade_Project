

using _0_Freamwork.Domain;
using CommentManagement.Application.Contracts.CommentAppContract;
using System.Collections.Generic;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository:IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
