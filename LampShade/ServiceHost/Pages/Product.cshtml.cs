using _01_LampShadeQuery.Contracts.Product;
using CommentManagement.Application.Contracts.CommentAppContract;
using CommentManagement.Infrastructure.Efcore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery,ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product=_productQuery.GetDetails(id);
        }
        public IActionResult OnPost(AddComment command, string productSlug)
        {
            command.Type = CommentTypes.Product;
            var result = _commentApplication.Add(command);
            return RedirectToPage("./Product", new {Id=productSlug});
        }
    }
}
