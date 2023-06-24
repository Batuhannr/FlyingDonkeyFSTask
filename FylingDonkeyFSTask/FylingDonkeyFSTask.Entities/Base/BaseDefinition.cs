using FylingDonkeyFSTask.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Entities.Base
{
    public class BaseDefinition : BaseClass, IDefinitionTable
    {
        public string Name { get; set; }
    }
}
