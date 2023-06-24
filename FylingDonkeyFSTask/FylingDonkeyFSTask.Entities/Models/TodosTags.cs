using FylingDonkeyFSTask.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Entities.Models
{
    public class TodosTags: BaseClass
    {
        public int TodoId { get; set; }
        public int TagId { get; set; }
        public Todos Todo { get; set; }
        public Tag Tag { get; set; }
    }
}
