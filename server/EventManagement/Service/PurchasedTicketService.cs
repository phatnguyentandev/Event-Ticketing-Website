﻿using AutoMapper;
using EventManagement.Data.Helpers;
using EventManagement.Data.Models;
using EventManagement.Data.Repository;
using EventManagement.Data.Repository.IRepository;
using EventManagement.Models;
using EventManagement.Models.ModelsDto.AgendaDtos;
using EventManagement.Models.ModelsDto.EventDtos;
using EventManagement.Models.ModelsDto.PurchasedDtos;

namespace EventManagement.Service
{
    public interface IPurchasedTicketService
    {
        Task CreatePurchased(string idOrderDetail, string idOrderHeader);
        Task<PagedListDto<PurchasedTicketDto>> GetAllPurchasedTicket(string idOrderHeader, string searchString, string status, int pageSize = 0, int pageNumber = 1); 
        Task<PurchasedTicketDto> GetPurchasedTicketById(string idPurchasedTicket);
        Task UpdatePurchasedTicket(string IdPurchasedTicket, PurchasedTicketUpdateDto model);
    }

    public class PurchasedTicketService : IPurchasedTicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchasedTicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreatePurchased(string idOrderDetail,string idOrderHeader)
        {
            PurchasedTicket purchasedTicket = new PurchasedTicket
            {
                IdPurchasedTicket = Guid.NewGuid().ToString(),
                OrderDetailId = idOrderDetail,
                OrderHeaderId = idOrderHeader,
                Status = "Pending"
            };

            await _unitOfWork.PurchasedTicketRepository.CreateAsync(purchasedTicket);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PagedListDto<PurchasedTicketDto>> GetAllPurchasedTicket(string idOrderHeader, string searchString, string status, int pageSize = 0, int pageNumber = 1)
        {
            var pagedPurchasedTicket = await _unitOfWork.PurchasedTicketRepository.GetPagedAllAsync(x => x.OrderHeaderId == idOrderHeader
            && (string.IsNullOrEmpty(searchString) || x.FullName.ToLower().Contains(searchString.ToLower()))
            && (string.IsNullOrEmpty(searchString) || x.Status.ToLower().Contains(searchString.ToLower())),
            pageNumber: pageNumber, pageSize: pageSize);

            var listDto = _mapper.Map<List<PurchasedTicketDto>>(pagedPurchasedTicket);
            var pagedPurchasedTicketDto = new PagedListDto<PurchasedTicketDto>()
            {
                CurrentPage = pagedPurchasedTicket.CurrentNumber,
                PageSize = pagedPurchasedTicket.PageSize,
                TotalCount = pagedPurchasedTicket.TotalCount,
                TotalPage = pagedPurchasedTicket.TotalPage,
                Items = listDto,
            };

            return pagedPurchasedTicketDto;
        }

        public async Task<PurchasedTicketDto> GetPurchasedTicketById(string idPurchasedTicket)
        {
            var entity = await _unitOfWork.PurchasedTicketRepository.GetAsync(x => x.IdPurchasedTicket == idPurchasedTicket);
            return _mapper.Map<PurchasedTicketDto>(entity);
        }

        public async Task UpdatePurchasedTicket(string IdPurchasedTicket, PurchasedTicketUpdateDto model)
        {
            var purchasedTicket = _mapper.Map<PurchasedTicket>(model);
            purchasedTicket.IdPurchasedTicket = IdPurchasedTicket;
            await _unitOfWork.PurchasedTicketRepository.Update(purchasedTicket);
            await _unitOfWork.SaveAsync();
        }
    }
}
