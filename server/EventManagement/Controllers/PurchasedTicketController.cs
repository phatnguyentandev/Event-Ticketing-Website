﻿using EventManagement.Data.Repository.IRepository;
using EventManagement.Models;
using EventManagement.Models.ModelsDto.PurchasedDtos;
using EventManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PurchasedTicketController : Controller
    {
        private readonly IPurchasedTicketService _purchasedTicketService;
        private readonly ApiResponse _apiResponse;

        public PurchasedTicketController(IPurchasedTicketService purchasedTicketService)
        {
            _purchasedTicketService = purchasedTicketService;
            _apiResponse = new ApiResponse();   
        }

        [HttpGet("[controller]")]
        public async Task<ActionResult<ApiResponse>> GetAllPurchasedTicket(string orderHeaderId, string searchString, string status,
            int pageSize = 0, int pageNumber = 1)
        {
            if (string.IsNullOrEmpty(orderHeaderId))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }

            var pagedModel = await _purchasedTicketService.GetAllPurchasedTicket(orderHeaderId, searchString, status, pageSize, pageNumber);

            if (pagedModel == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                _apiResponse.IsSuccess = false;
                return NotFound(_apiResponse);
            }

            PaginationDto pagination = new PaginationDto()
            {
                CurrentPage = pagedModel.CurrentPage,
                PageSize = pagedModel.PageSize,
                TotalRecords = pagedModel.TotalCount,
            };

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagination));

            _apiResponse.Result = pagedModel.Items;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);
        }

        [HttpGet("[controller]/{idPurchasedTicket}")]
        public async Task<ActionResult<ApiResponse>> GetPurchasedTicketById(string idPurchasedTicket)
        {
            var purchasedTicketDto = await _purchasedTicketService.GetPurchasedTicketById(idPurchasedTicket);

            if (purchasedTicketDto == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                _apiResponse.IsSuccess = false;
                return NotFound(_apiResponse);
            }

            _apiResponse.Result = purchasedTicketDto;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);
        }

        [HttpPut("[controller]/{idPurchasedTicket}")]
        public async Task<ActionResult<ApiResponse>> UpdatePurchasedTicket([FromRoute]string idPurchasedTicket, [FromBody] PurchasedTicketUpdateDto model)
        {
            if (string.IsNullOrEmpty(idPurchasedTicket))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }

            await _purchasedTicketService.UpdatePurchasedTicket(idPurchasedTicket, model);

            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);
        }
    }
}
