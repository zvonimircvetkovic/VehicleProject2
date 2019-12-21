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
        public async Task<PagedList<IMake>> GetAllAsync(PageModel page, SearchModel search, SortModel sort)
        {
            if (!String.IsNullOrEmpty(search.SearchString))
            {
                var searchMakes = _unitOfWork.Makes.GetAllAsync().Where(m => m.Name.Contains(search.SearchString)
                                                                        || m.Abrv.Contains(search.SearchString)).OrderByDescending(m => m.Name);

                return await Paginate(page, searchMakes);
            }

            switch (sort.SortOrder)
            {
                case "name_desc":
                {
                    var makes = _unitOfWork.Makes.GetAllAsync().OrderByDescending(m => m.Name);

                    return await Paginate(page, makes);
                }
                case "Abrv":
                {
                    var makes = _unitOfWork.Makes.GetAllAsync().OrderBy(m => m.Abrv);

                    return await Paginate(page, makes);
                }
                case "abrv_desc":
                {
                    var makes = _unitOfWork.Makes.GetAllAsync().OrderByDescending(m => m.Abrv);

                    return await Paginate(page, makes);
                }
                default:
                {
                    var makes = _unitOfWork.Makes.GetAllAsync().OrderBy(m => m.Name);

                    return await Paginate(page, makes);
                }
            }
        }

        //Gets the make from the repository by its id
        public async Task<IMake> GetByIdAsync(int id)
        {
            var make = await _unitOfWork.Makes.GetByIdAsync(id);
            var listMake = _mapper.Map<IMake>(make);
            return listMake;
        }

        //Method for pagination of a result
        public async Task<PagedList<IMake>> Paginate(PageModel page, IQueryable<IMakeEntity> makes)
        {
            var makesPage = await PagedList<IMakeEntity>.ToPagedList(makes, page.PageNumber, page.PageSize);

            var list = _mapper.Map<List<IMake>>(makesPage.Items);

            var listMakes = new PagedList<IMake>(list, makesPage.TotalCount, makesPage.CurrentPage, makesPage.PageSize);

            return listMakes;
        }
    }
}
