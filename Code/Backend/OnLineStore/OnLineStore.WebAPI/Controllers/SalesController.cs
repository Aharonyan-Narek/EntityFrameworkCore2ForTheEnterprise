﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.WebAPI.Requests;
using OnLineStore.WebAPI.Responses;

namespace OnLineStore.WebAPI.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalesController : ControllerBase
    {
        protected ILogger Logger;
        protected ISalesService SalesService;

        public SalesController(ILogger<SalesController> logger, ISalesService salesService)
        {
            Logger = logger;
            SalesService = salesService;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Retrieves the orders generated by customers
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="orderStatusID">Order status</param>
        /// <param name="customerID">Customer</param>
        /// <param name="employeeID">Employee</param>
        /// <param name="shipperID">Shipper</param>
        /// <param name="currencyID">Currency</param>
        /// <param name="paymentMethodID">Payment method</param>
        /// <returns>A sequence of orders</returns>
        [HttpGet("Order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOrdersAsync(int? pageSize = 50, int? pageNumber = 1, short? orderStatusID = null, int? customerID = null, int? employeeID = null, int? shipperID = null, short? currencyID = null, Guid? paymentMethodID = null)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrdersAsync));

            // Get response from business logic
            var response = await SalesService.GetOrdersAsync((int)pageSize, (int)pageNumber, orderStatusID, customerID, employeeID, shipperID, currencyID, paymentMethodID);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Retrieves an existing order by id
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>An existing order</returns>
        [HttpGet("Order/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOrderAsync(long id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetOrderAsync));

            // Get response from business logic
            var response = await SalesService.GetOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Retrieves the request model to create a new order
        /// </summary>
        /// <returns>A model that represents the request to create a new order</returns>
        [HttpGet("CreateOrderRequest")]
        public async Task<IActionResult> GetCreateOrderRequestAsync()
        {
            Logger?.LogDebug("{0} has been invoked", nameof(GetCreateOrderRequestAsync));

            // Get response from business logic
            var response = await SalesService.GetCreateOrderRequestAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="request">Model request</param>
        /// <returns>A result that contains the order ID generated by application</returns>
        [HttpPost]
        [Route("Order")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderHeaderRequest request)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CreateOrderAsync));

            // Get response from business logic
            var response = await SalesService.CreateOrderAsync(request.GetOrder(), request.GetOrderDetails().ToArray());

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Creates a new order from existing order
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>A model for a new order</returns>
        [HttpGet("CloneOrder/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CloneOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(CloneOrderAsync));

            // Get response from business logic
            var response = await SalesService.CloneOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }

        /// <summary>
        /// Deletes an existing order
        /// </summary>
        /// <param name="id">ID for order</param>
        /// <returns>A success response if order is deleted</returns>
        [HttpDelete("Order/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            Logger?.LogDebug("{0} has been invoked", nameof(DeleteOrderAsync));

            // Get response from business logic
            var response = await SalesService.RemoveOrderAsync(id);

            // Return as http response
            return response.ToHttpResponse();
        }
    }
}
