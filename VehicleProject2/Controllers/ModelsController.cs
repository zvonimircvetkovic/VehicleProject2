using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Common;
using Project.Service.Common;

namespace Project.WebAPI.Controllers
{
    public class ModelsController : Controller
    {
        private readonly IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        public async Task<IActionResult> Index(int makeId)
        {
            var models = await _modelService.GetAllByMakeIdAsync(makeId);

            return View(models);
        }

        public IActionResult Create(int makeId)
        {
            var newModel = new Model.Model
            {
                MakeId = makeId
            };

            return View(newModel);
        }

        public async Task<IActionResult> Create(IModel newModel)
        {
            await _modelService.AddAsync(newModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _modelService.GetByIdAsync(id);

            return View(model);
        }

        public async Task<IActionResult> Edit(IModel editModel)
        {
            await _modelService.UpdateAsync(editModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _modelService.GetByIdAsync(id);

            return View(model);
        }

        public async Task<IActionResult> DeleteConfirmed(IModel deleteModel)
        {
            await _modelService.RemoveAsync(deleteModel);
            return RedirectToAction("Index");
        }
    }
}