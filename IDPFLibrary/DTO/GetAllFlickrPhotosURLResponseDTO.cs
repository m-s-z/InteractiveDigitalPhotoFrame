using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO

{
    public class Urls
    {
        public Urls()
        {

        }

        public Urls(string link, string photoId, CloudProviderType myProperty, DateTime uploadDate)
        {
            Link = link;
            PhotoId = photoId;
            MyProperty = myProperty;
            UploadDate = uploadDate;
        }

        public String Link { get; set; }
        public String PhotoId { get; set; }
        public CloudProviderType MyProperty { get; set; }
        public DateTime UploadDate { get; set; }
    }
    public class GetAllFlickrPhotosURLResponseDTO
    {
        public GetAllFlickrPhotosURLResponseDTO()
        {
                
        }

        public GetAllFlickrPhotosURLResponseDTO(List<Urls> urls)
        {
            Urls = urls;
        }

        public List<Urls> Urls { get; set; }
    }
}
