using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Filter;
using Project.Model.Common;
using Project.Service.Common;
using Project.WebAPI.Helpers;
using Project.WebAPI.Models;

namespace Project.WebAPI.Controllers
{
    [Route("api/makes/{makeId}/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;

        public ModelsController(IModelService modelService, IMapper mapper)
        {
            _modelService = modelService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int makeId, [FromQuery]PageModel pageModel, [FromQuery]SearchModel search, [FromQuery]SortModel sort)
        {
            var models = await _modelService.GetAllByMakeIdAsync(makeId, pageModel, search, sort);

            Response.AddPagination(models.CurrentPage, models.PageSize, models.TotalCount, models.TotalPages);

            var modelsList = _mapper.Map<IEnumerable<IModel>>(models.Items);

            return Ok(modelsList);
        }
        
        [HttpGet("{id}", Name = "GetModel")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model = await _modelService.GetByIdAsync(id);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfirmed(ViewModel model)
        {
            var newModel = _mapper.Map<IModel>(model);

            await _modelService.AddAsync(newModel);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditConfirmed(ViewModel model)
        {
            var editModel = _mapper.Map<IModel>(model);

            await _modelService.UpdateAsync(editModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed([FromHeader]ViewModel model)
        {
            var deleteModel = _mapper.Map<IModel>(model);

            await _modelService.RemoveAsync(deleteModel);
            
            return NoContent();
        }
    }
}