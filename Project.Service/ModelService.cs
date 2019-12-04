using AutoMapper;
using Project.DAL.Entities;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public ModelService(IModelRepository makeRepository, IMapper mapper)
        {
            _modelRepository = makeRepository;
            _mapper = mapper;
        }

        //Adds a model
        public async Task AddAsync(IModel vehicleModel)
        {
            var newModel = _mapper.Map<IModelEntity>(vehicleModel);
            await _modelRepository.AddAsync(newModel);
        }

        //Gets all models my their make id
        public async Task<IList<IModel>> GetAllByMakeIdAsync(int id)
        {
            var models = await _modelRepository.GetAllByMakeIdAsync(id);
            var listModels = _mapper.Map<List<IModel>>(models);
            return listModels;
        }

        //Gets a model by its id
        public async Task<IModel> GetByIdAsync(int id)
        {
            var models = await _modelRepository.GetByIdAsync(id);
            var listModel = _mapper.Map<IModel>(models);
            return listModel;
        }

        //Removes a model
        public async Task RemoveAsync(IModel vehicleModel)
        {
            var deleteModel = _mapper.Map<IModelEntity>(vehicleModel);
            await _modelRepository.RemoveAsync(deleteModel);
        }

        //Updates a model
        public async Task UpdateAsync(IModel vehicleModel)
        {
            var editModel = _mapper.Map<IModelEntity>(vehicleModel);
            await _modelRepository.UpdateAsync(editModel);
        }
    }
}
