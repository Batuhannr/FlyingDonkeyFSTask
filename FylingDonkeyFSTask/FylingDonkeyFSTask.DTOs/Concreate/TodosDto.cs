using FylingDonkeyFSTask.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.DTOs.Concreate
{
    public class TodosDto : IDto
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }

        public string Name { get; set; }
        public string Explanation { get; set; }
        public List<TagDto> Tags { get; set; }
    }

    public class TagDto : IDto
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Name { get; set; }
    }
}
