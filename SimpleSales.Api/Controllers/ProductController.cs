
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using SimpleSales.Api.Attributes;
using SimpleSales.Api.Dtos;
using SimpleSales.Api.Dtos.Product;
using SimpleSales.Api.Models;
using SimpleSales.Api.Services;

namespace SimpleSales.Api.Controllers
{
    [ApiKey]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        public async Task<ContentResult> Get()
        {
            var response = new ResponseDto { Message = "Success" };

            try
            {
                var product = await _productService.GetAsync();

                var result = _mapper.Map<IList<ProductModel>, IList<ProductDto>>(product);

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
