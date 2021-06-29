
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using SimpleSales.Api.Attributes;
using SimpleSales.Api.Dtos;
using SimpleSales.Api.Dtos.Order;
using SimpleSales.Api.Dtos.Product;
using SimpleSales.Api.Models;
using SimpleSales.Api.Services;

namespace SimpleSales.Api.Controllers
{
    [ApiKey]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ContentResult> Get()
        {
            var response = new ResponseDto { Message = "Success" };

            try
            {
                var orders = await _orderService.GetAsync();

                var result = _mapper.Map<IList<OrderModel>, IList<OrderDto>>(orders);

                response.Data = result;
            }
            catch (Exception)
            {
                response.Message = "Something went wrong";
            }

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(response)
            };
        }

        [HttpPost]
        public async Task<ContentResult> Create([FromBody] CreateOrderDto request)
        {
            var response = new ResponseDto { Message = "Success" };

            try
            {
                var order = _mapper.Map<CreateOrderDto, OrderModel>(request);

                if(! await _orderService.Create(order)) throw new Exception();

                var result = _mapper.Map<OrderModel, OrderDto>(order);

                response.Data = result;
            }
            catch (Exception)
            {
                response.Message = "Something went wrong";
            }

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(response)
            };
        }
    }
}
