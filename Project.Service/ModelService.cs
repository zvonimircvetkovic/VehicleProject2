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
        public async Task<IEnumerable<IModel>> GetAllByMakeIdAsync(int id)
        {
            var models = await _unitOfWork.Models.GetAllByMakeIdAsync(id);
            var listModels = _mapper.Map<IEnumerable<IModel>>(models);
            return listModels;
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
    }
}
