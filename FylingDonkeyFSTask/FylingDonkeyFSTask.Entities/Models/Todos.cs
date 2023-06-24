using FylingDonkeyFSTask.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Entities.Models
{
    public partial class Todos : BaseDefinition
    {
        public string Explanation { get; set; }
        public string BgColour { get; set; }

        public ICollection<TodosTags> TodosTags { get; set; }

    }
}
