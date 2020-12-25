using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.CoreLib.EF;
using App.CoreLib.EF.Data;

namespace App.Core.Web.AppControllers
{
    public abstract class QueryControllerBase<TRequest, TResponse, TQuery> : AppControllerBase
        where TRequest : IQueryRequest, new()
        where TResponse : class, new()
        where TQuery : IQueryBase<TResponse>
    {
        public TQuery Query { get; private set; }

        public QueryControllerBase(TQuery query) : base()
        {
            this.Query = query;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Index()
        {
            return await GetDataAsync();
        }

        protected virtual async Task<IActionResult> GetDataAsync()
        {
            TRequest request = GetRequest();
            //Returning Json Data  
            return await GetDataAsync(request);
        }

        protected virtual async Task<IActionResult> GetDataAsync(TRequest request)
        {
            SetRequestData(request);
            var response = await Query.ReadDataAsync(request);

            //Returning Json Data  
            return Json(new { draw = request.Draw, recordsFiltered = response.RecordsTotal, recordsTotal = response.RecordsTotal, data = response.Results });

        }
        protected virtual void SetRequestData(TRequest request)
        {

        }
        protected virtual TRequest GetRequest()
        {
            var request = new TRequest();

            request.Draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            // Skiping number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)  
            request.Take = length != null ? Convert.ToInt32(length) : 0;
            request.Skip = start != null ? Convert.ToInt32(start) : 0;
            request.OrderColumn = sortColumn;
            request.SortDirection = sortColumnDirection;
            request.SearchValue = searchValue;
            return request;
        }
    }
}