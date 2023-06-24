using FylingDonkeyFSTask.Business.Services;
using FylingDonkeyFSTask.Common.Concreate;
using FylingDonkeyFSTask.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace FylingDonkeyFSTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : GenericController<Todos>
    {
        public IGenericService<TodosTags> _todosTagsService;
        public IGenericService<Todos> _todosService;
        public IGenericService<Tag> _tagService;

        public TodosController(IGenericService<Todos> todosService, IGenericService<TodosTags> todosTagsService, IGenericService<Tag> tagService) : base(todosService)
        {
            _todosService = todosService;
            _todosTagsService = todosTagsService;
            _tagService = tagService;
        }
        [HttpGet("Detail")]
        public virtual async Task<IActionResult> Detail(int Id = 0, bool deleted = false)
        {

            List<Todos> _tempData;
            if (!deleted)
            {
                if (Id != 0)
                {
                    _tempData = _todosService.GetQueryable(s => s.Deleted == false && s.Id == Id).Include(s => s.TodosTags.Where(s => s.Deleted == false)).Include("TodosTags.Tag").ToList();

                }
                else
                {
                    _tempData = _todosService.GetQueryable(s => s.Deleted == false).Include(s => s.TodosTags.Where(s => s.Deleted == false)).Include("TodosTags.Tag").ToList();

                }
            }
            else
            {
                if (Id != 0)
                {
                    _tempData = _todosService.GetQueryable(s => s.Deleted == true && s.Id == Id).Include(s => s.TodosTags.Where(s => s.Deleted == false)).Include("TodosTags.Tag").ToList();

                }
                else
                {
                    _tempData = _todosService.GetQueryable(s => s.Deleted == true).Include(s => s.TodosTags.Where(s => s.Deleted == false)).Include("TodosTags.Tag").ToList();

                }
            }
            
            if (_tempData != null)
            {
                return Ok(new ReturnHelper<List<Todos>>(true, _tempData, new List<string> { "Successful" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<List<Todos>>(false, new List<Todos>(), new List<string> { "Unsuccessful" }));
            }
        }
        [HttpPost("DetailByTag")]
        public virtual async Task<IActionResult> DetailByTag(int[] idList)
        {
            List<Todos> returnlist = new List<Todos>();
            List<TodosTags> _tempData = new List<TodosTags>();
            _tempData = _todosTagsService.GetQueryable(s => s.Deleted == false).Include(s=> s.Todo).ToList();
            foreach (int item in idList)
            {
                List<Todos> todoslist = _tempData.Where(s => s.TagId == item).Select(x=> x.Todo).ToList();
                foreach (Todos todosItem in todoslist)
                {
                    if (!(returnlist.Where(s => s.Id == todosItem.Id).Count() > 0))
                    {
                        returnlist.Add(todosItem);
                    }
                }
            }
            if (returnlist != null)
            {
                return Ok(new ReturnHelper<List<Todos>>(true, returnlist, new List<string> { "Successful" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<List<Todos>>(false, new List<Todos>(), new List<string> { "Unsuccessful" }));
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Post(Todos data)
        {
            List<Tag>_tags = _tagService.GetAllAsync().Result.ToList();
            List<TodosTags> _tempData = new List<TodosTags>() ;
            foreach (TodosTags todostag in data.TodosTags)
            {
                Tag tagControl = _tags.Where(s => s.Name == todostag.Tag.Name).FirstOrDefault();
                if (tagControl != null)
                {
                    todostag.TagId = tagControl.Id;
                    _todosTagsService.Add(todostag);
                }
                else
                {

                    HttpClient client = new HttpClient();
                    var json = JsonConvert.SerializeObject(todostag.Tag);
                    var dataTag = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync("https://localhost:44323/api/tag", dataTag);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Tag newtag = _tagService.GetAllAsync().Result.Where(s => s.Name == todostag.Tag.Name).FirstOrDefault();
                        todostag.TagId = newtag.Id;
                        _todosTagsService.Add(todostag);
                    }
                }
            }
            _todosService.Add(data);
            int sonuc = await _todosService.SaveChangeAsync();
            if (sonuc > 0)
            {
                return Ok(new ReturnHelper<Todos>(true, data, new List<string> { "Successful" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<Todos>(false, data, new List<string> { "Unsuccessful" }));
            }
        }

        [HttpPut]
        public override async Task<IActionResult> Update(Todos todos)
        {
            Todos _data = await _todosService.GetQueryable(s => s.Id == todos.Id).Include(s => s.TodosTags.Where(s => s.Deleted == false)).Include("TodosTags.Tag").FirstOrDefaultAsync();
            foreach (TodosTags activeTag in _data.TodosTags)
            {
                activeTag.Deleted = true;
            }
            foreach (TodosTags newTags in todos.TodosTags)
            {
                TodosTags control = _data.TodosTags.Where(s => s.Tag.Name == newTags.Tag.Name).FirstOrDefault();
                if (control != null)
                {
                    control.Deleted = false;
                }
                else
                {
                    Tag tagControl = _tagService.GetAllAsync().Result.ToList().Where(s => s.Name == newTags.Tag.Name).FirstOrDefault();
                    if (tagControl != null)
                    {
                        newTags.TagId = tagControl.Id;
                        _data.TodosTags.Add(newTags);
                    }
                    else
                    {
                        HttpClient client = new HttpClient();
                        var json = JsonConvert.SerializeObject(newTags.Tag);
                        var dataTag = new StringContent(json, Encoding.UTF8, "application/json");
                        var result = await client.PostAsync("https://localhost:44323/api/tag", dataTag);
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Tag newtag = _tagService.GetAllAsync().Result.Where(s => s.Name == newTags.Tag.Name).FirstOrDefault();
                            newTags.TagId = newtag.Id;
                            newTags.Tag = newtag;
                            _data.TodosTags.Add(newTags);
                        }
                    }

                }
            }
            _data.Name = todos.Name;
            _data.Explanation = todos.Explanation;
            _data.BgColour = todos.BgColour;
            _todosService.Update(_data);
            Task<int> a = _GenericService.SaveChangeAsync();
            if (a.Result >= 1)
            {
                return Ok(new ReturnHelper<Todos>(true, todos, new List<string> { "Successful" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<Todos>(false, todos, new List<string> { "Unsuccessful" }));
            }
        }
       
    }


}
