using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrdersModel
    {
        public int OrderTableId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int StaffId { get; set; } = 0;
        public string OrderStatus { get; set; } = string.Empty;
        public DateTime DayBuy { get; set; } = DateTime.Now;
        public string DeliveryAddress { get; set; } = string.Empty;
        public int Evaluate {  get; set; } = 0;
        public bool Deleted { get; set; } = false;
        public List<OrderDetailModel> listjson_orderDetail { get; set; }
    }
}
