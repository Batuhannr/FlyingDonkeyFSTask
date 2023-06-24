using FylingDonkeyFSTask.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Entities.Base
{
    public class BaseClass : ITable
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
