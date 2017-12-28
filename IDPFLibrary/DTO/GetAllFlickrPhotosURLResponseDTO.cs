using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPFLibrary.DTO
{
    public class GetAllFlickrPhotosURLResponseDTO
    {
        public GetAllFlickrPhotosURLResponseDTO()
        {
                
        }

        public GetAllFlickrPhotosURLResponseDTO(List<string> urls)
        {
            Urls = urls;
        }

        public List<String> Urls { get; set; }
    }
}
