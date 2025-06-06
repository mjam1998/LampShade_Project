﻿

using _0_Freamwork.Application;
using System.Collections.Generic;

namespace CommentManagement.Application.Contracts.CommentAppContract
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
