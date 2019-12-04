using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Common;
using Project.Service.Common;

namespace Project.WebAPI.Controllers
{
    public class MakesController : Controller
    {
        private readonly IMakeService _makeService;

        public MakesController(IMakeService makeService)
        {
            _makeService = makeService;
        }

        public async Task<IActionResult> Index()
        {
            var makes = await _makeService.GetAllAsync();

            return View(makes);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(IMake newMake)
        {
            await _makeService.AddAsync(newMake);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var make = await _makeService.GetByIdAsync(id);

            return View(make);
        }

        public async Task<IActionResult> Edit(IMake editMake)
        {
            await _makeService.UpdateAsync(editMake);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var make = await _makeService.GetByIdAsync(id);

            return View(make);
        }

        public async Task<IActionResult> DeleteConfirmed(IMake deleteMake)
        {
            await _makeService.RemoveAsync(deleteMake);
            return RedirectToAction("Index");
        }
    }
}