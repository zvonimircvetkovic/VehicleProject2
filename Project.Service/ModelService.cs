using AutoMapper;
using Project.Common.Filter;
using Project.DAL.Entities;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Adds a model
        public async Task AddAsync(IModel vehicleModel)
        {
            var newModel = _mapper.Map<ModelEntity>(vehicleModel);
            await _unitOfWork.Models.Add(newModel);
            await _unitOfWork.Complete();
        }

        //Gets all models my their make id
        public async Task<PagedList<IModel>> GetAllByMakeIdAsync(int id, PageModel page, SearchModel search, SortModel sort)
        {
            if (!String.IsNullOrEmpty(search.SearchString))
            {
                var searchModels = _unitOfWork.Models.GetAllByMakeIdAsync(id).Where(m => m.Name.Contains(search.SearchString)
                                                                        || m.Abrv.Contains(search.SearchString)).OrderByDescending(m => m.Name);

                return await Paginate(page, searchModels);
            }

            switch (sort.SortOrder)
            {
                case "name_desc":
                {
                    var models = _unitOfWork.Models.GetAllByMakeIdAsync(id).OrderByDescending(m => m.Name);

                    return await Paginate(page, models);
                }
                case "Abrv":
                {
                    var models = _unitOfWork.Models.GetAllByMakeIdAsync(id).OrderBy(m => m.Abrv);

                    return await Paginate(page, models);
                }
                case "abrv_desc":
                {
                    var models = _unitOfWork.Models.GetAllByMakeIdAsync(id).OrderByDescending(m => m.Abrv);

                    return await Paginate(page, models);
                }
                default:
                {
                    var models = _unitOfWork.Models.GetAllByMakeIdAsync(id).OrderBy(m => m.Name);

                    return await Paginate(page, models);
                }
            }
        }

        //Gets a model by its id
        public async Task<IModel> GetByIdAsync(int id)
        {
            var models = await _unitOfWork.Models.GetByIdAsync(id);
            var listModel = _mapper.Map<IModel>(models);
            return listModel;
        }

        //Removes a model
        public async Task RemoveAsync(IModel vehicleModel)
        {
            var deleteModel = _mapper.Map<ModelEntity>(vehicleModel);
            _unitOfWork.Models.Remove(deleteModel);
            await _unitOfWork.Complete();
        }

        //Updates a model
        public async Task UpdateAsync(IModel vehicleModel)
        {
            var editModel = _mapper.Map<ModelEntity>(vehicleModel);
            _unitOfWork.Models.Update(editModel);
            await _unitOfWork.Complete();
        }

        //Method for pagination of the result
        public async Task<PagedList<IModel>> Paginate(PageModel page, IQueryable<IModelEntity> models)
        {
            var modelsPage = await PagedList<IModelEntity>.ToPagedList(models, page.PageNumber, page.PageSize);

            var list = _mapper.Map<List<IModel>>(modelsPage.Items);

            var listModels = new PagedList<IModel>(list, modelsPage.TotalCount, modelsPage.CurrentPage, modelsPage.PageSize);

            return listModels;
        }
    }
}
