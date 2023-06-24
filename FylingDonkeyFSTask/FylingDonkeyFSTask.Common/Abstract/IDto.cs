using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Common.Abstract
{
    public interface IDto
    {
        public int Id{ get; set; }
        public bool Deleted{ get; set; }
    }
}
