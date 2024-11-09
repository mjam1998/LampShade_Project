
using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contracts.Slide;
using ShopManagement.Infrastructure;

namespace _01_LampShadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides.Where(x=>x.IsRemoved==false).Select(x => new SlideQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Link = x.Link,
                Text = x.Text,
                Title = x.Title
            }).ToList();
        }
    }
}
