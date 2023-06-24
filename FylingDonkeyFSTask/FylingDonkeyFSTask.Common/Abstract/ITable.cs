using System;
using System.Collections.Generic;
using System.Text;

namespace FylingDonkeyFSTask.Common.Abstract
{
    public interface ITable
    {
        public int Id { get; set; }

        public bool Deleted { get; set; }
    }
}
