using FylingDonkeyFSTask.Business.Services;
using FylingDonkeyFSTask.Common.Abstract;
using FylingDonkeyFSTask.Common.Concreate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FylingDonkeyFSTask.WebApi.Controllers
{
    public abstract class GenericController<T> : ControllerBase
       where T : class, ITable, new()
    {
        public IGenericService<T> _GenericService;

        public GenericController(IGenericService<T> genericService)
        {
            _GenericService = genericService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            List<T> _tempData = await _GenericService.GetAllAsync();

            if (_tempData != null)
            {
                return Ok(new ReturnHelper<List<T>>(true, _tempData, new List<string> { "Başarılı" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<List<T>>(false, new List<T>(), new List<string> { "Başarısız" }));
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            T _tempData = await _GenericService.GetByIdAsync(id);
            if (_tempData != null)
            {
                return Ok(new ReturnHelper<T>(true, _tempData, new List<string> { "Başarılı" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<T>(false, new T(), new List<string> { "Başarısız" }));
            }
        }

        [HttpPut("delete")]
        public virtual async Task<IActionResult> Delete(T data)
        {
            data.Deleted = true;
            _GenericService.Update(data);
            int a = await _GenericService.SaveChangeAsync();
            if (a >= 1)
            {
                return Ok(new ReturnHelper<T>(true, data, new List<string> { "Başarılı" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<T>(false, data, new List<string> { "Başarısız" }));
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(T data)
        {
            T _data = await _GenericService.GetByIdAsync(data.Id);
            _GenericService.Update(data);
            Task<int> a = _GenericService.SaveChangeAsync();
            if (a.Result == 1)
            {
                return Ok(new ReturnHelper<T>(true, data, new List<string> { "Başarılı" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<T>(false, data, new List<string> { "Başarısız" }));
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T data)
        {
            _GenericService.Add(data);
            int sonuc = await _GenericService.SaveChangeAsync();
            if (sonuc > 0)
            {
                return Ok(new ReturnHelper<T>(true, data, new List<string> { "Başarılı" }));
            }
            else
            {
                return BadRequest(new ReturnHelper<T>(false, data, new List<string> { "Başarısız" }));
            }
        }
    }
}
