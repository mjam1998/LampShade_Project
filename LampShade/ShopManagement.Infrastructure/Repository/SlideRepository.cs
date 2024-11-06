using System.Collections.Generic;
using System.Linq;
using _0_Freamwork.Infrastructure;
using ShopManagement.Application.Contracts.SlideAppContract;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class SlideRepository:RepositoryBase<long,Slide>,ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository( ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Title = x.Title
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Heading = x.Heading,
                Picture = x.Picture,
                Title = x.Title,
                CreationDate = x.CreationDate.ToString(),
                IsRemoved = x.IsRemoved
            }).OrderByDescending(x => x.Id).ToList();

        }
    }
}
