using AutoMapper;
using Project.DAL.Entities;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Service
{
    public class MakeService : IMakeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MakeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Adds a make
        public async Task AddAsync(IMake vehicleMake)
        {
            var newMake = _mapper.Map<MakeEntity>(vehicleMake);
            await _unitOfWork.Makes.Add(newMake);
            await _unitOfWork.Complete();
        }

        //Updates a make
        public async Task UpdateAsync(IMake vehicleMake)
        {
            var editMake = _mapper.Map<MakeEntity>(vehicleMake);
            _unitOfWork.Makes.Update(editMake);
            await _unitOfWork.Complete();
        }

        //Removes a make
        public async Task RemoveAsync(IMake vehicleMake)
        {
            var deleteMake = _mapper.Map<MakeEntity>(vehicleMake);
            _unitOfWork.Makes.Remove(deleteMake);
            await _unitOfWork.Complete();
        }

        //Gets all makes from the repository
        public async Task<IEnumerable<IMake>> GetAllAsync()
        {
            var makes = await _unitOfWork.Makes.GetAllAsync();
            var listMakes = _mapper.Map<IEnumerable<IMake>>(makes);
            return listMakes;
        }

        //Gets the make from the repository by its id
        public async Task<IMake> GetByIdAsync(int id)
        {
            var make = await _unitOfWork.Makes.GetByIdAsync(id);
            var listMake = _mapper.Map<IMake>(make);
            return listMake;
        }

        //public async Task<IList<IMake>> ToPagedList(IPageModel pageModel)
        //{
        //    IList<IMake> makes = await _makeRepository.GetAllAsync();

        //    pageModel.TotalCount = makes.Count;
        //    pageModel.TotalPages = (int)Math.Ceiling(pageModel.TotalCount / (double)pageModel.PageSize);

        //    return makes.Skip((pageModel.CurrentPage - 1) * pageModel.PageSize).Take(pageModel.PageSize).ToList();
        //}
    }
}
