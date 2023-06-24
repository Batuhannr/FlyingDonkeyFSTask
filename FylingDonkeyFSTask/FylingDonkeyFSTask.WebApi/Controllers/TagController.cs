using FylingDonkeyFSTask.Business.Services;
using FylingDonkeyFSTask.Common.Concreate;
using FylingDonkeyFSTask.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FylingDonkeyFSTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : GenericController<Tag>
    {
        public IGenericService<Tag> _tagService;
        public IGenericService<TodosTags> _todosTagService;

        public TagController(IGenericService<Tag> genericService, IGenericService<TodosTags> todostagService) : base(genericService) {
            _tagService = genericService;
            _todosTagService = todostagService;
        }

        [HttpGet("top")]
        public virtual async Task<IActionResult> GetTopTag(int count = 0)
        {
           ICollection<TodosTags> mostUsingTag = _todosTagService.GetQueryable(x=> x.Deleted == false).Include(x=> x.Tag).ToList();
           ICollection<Tag> tags = _tagService.GetAllAsync().Result.ToList();
           List<Tag> result = new List<Tag>();
            //var a = mostUsingTag.GroupBy(s => s.TagId).Select(x => x.Count() > 5).ToList();

            List<TopTag> groups = mostUsingTag.GroupBy(n => n.TagId)
                         .Select(n => new TopTag
                         {
                             Tag = tags.Where(x=> x.Id == n.Key).FirstOrDefault(),
                             Count = n.Count()
                         })
                         .OrderBy(n => n.Count).ToList();
            if (count != 0)
            {
                return Ok(new ReturnHelper<List<TopTag>>(true, groups.Where(x => x.Count > count).ToList(), new List<string> { "Successful" }));
            }
            else
            {
                return Ok(new ReturnHelper<List<TopTag>>(true, groups.Where(x => x.Count > 5).ToList(), new List<string> { "Successful" }));
            }
        }

    }
    class TopTag
    {
        public Tag Tag { get; set;}
        public int Count { get; set; }
    }
}
