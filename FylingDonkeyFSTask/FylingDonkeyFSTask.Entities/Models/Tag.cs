using FylingDonkeyFSTask.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Entities.Models
{
    public class Tag: BaseDefinition
    {
        public ICollection<TodosTags> TodosTags { get; set; }
    }
}
