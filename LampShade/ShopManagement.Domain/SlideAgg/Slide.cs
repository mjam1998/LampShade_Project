
using _0_Freamwork.Domain;

namespace ShopManagement.Domain.SlideAgg
{
    public class Slide:EntityBase
    {
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string BtnText { get; private set; }
        public string Link { get; private set; }
        public bool IsRemoved { get; private set; }

        public Slide(string picture, string pictureTitle, string pictureAlt, string heading, string title, string text, string btnText,string link)
        {
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
            IsRemoved = false;
        }
        public void Edit(string picture, string pictureTitle, string pictureAlt, string heading, string title, string text, string btnText, string link)
        {
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;

        }

        public void Remove()
        {
            IsRemoved=true;
        }

        public void ReStore()
        {
            IsRemoved=false;
        }
    }
}
