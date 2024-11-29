

namespace CommentManagement.Application.Contracts.CommentAppContract
{
    public class AddComment
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Message { get;  set; }
        public long OwnerId { get;  set; }
        public int Type { get;  set; }
        public long ParentId { get;  set; }
        public string Website { get;  set; }


    }
}
