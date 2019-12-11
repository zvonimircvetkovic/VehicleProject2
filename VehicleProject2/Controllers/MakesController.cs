using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Models;
using Project.Model.Common;
using Project.Service.Common;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        private readonly IMakeService _makeService;
        private readonly IMapper _mapper;

        public MakesController(IMakeService makeService, IMapper mapper)
        {
            _makeService = makeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var makes = await _makeService.GetAllAsync();

            return Ok(makes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConfirmed(ViewMake make)
        {
            var newMake = _mapper.Map<IMake>(make);

            await _makeService.AddAsync(newMake);

            return NoContent();
        }

        [HttpGet("{id}", Name = "GetMake")]
        public async Task<IActionResult> GetMake(int id)
        {
            var make = await _makeService.GetByIdAsync(id);

            return Ok(make);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditConfirmed(ViewMake make)
        {
            var editMake = _mapper.Map<IMake>(make);

            await _makeService.UpdateAsync(editMake);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed([FromHeader]ViewMake make)
        {
            var deleteMake = _mapper.Map<IMake>(make);

            await _makeService.RemoveAsync(deleteMake);

            return NoContent();
        }
    }
}