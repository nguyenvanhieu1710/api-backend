using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AdvertisementModel
    {
        public int AdvertisementId { get; set; } = 0;
        public string AdvertisementName { get; set; } = string.Empty;
        public string AdvertisementImage {  get; set; } = string.Empty ;
        public string Location {  get; set; } = string.Empty ;
        public int AdvertiserId { get; set; } = 0;
        public bool Deleted { get; set; } = false;
    }
}
