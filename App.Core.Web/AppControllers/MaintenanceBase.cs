using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.CoreLib.EF;
using App.CoreLib.EF.Data;
using App.CoreLib.EF.Data.Entity;

namespace App.Core.Web.AppControllers
{
    public abstract class MaintenanceBase<TEntity> : AppControllerBase
        where TEntity : class, IEntity, new()
    {
        public IRepository<TEntity> _repositoryService { get; private set; }
        public MaintenanceBase(IRepository<TEntity> repository) : base()
        {
            _repositoryService = repository;
        }
        
        protected virtual async Task<TEntity> CreateOrEditModelAsync(TEntity createOrEdit)
        {      
            if (createOrEdit.RowVersion == 0)
                _repositoryService.Add(createOrEdit);

            else _repositoryService.Edit(createOrEdit);

            var result = await _repositoryService.SaveChangesAsync();
            if(!result.Succeeded)
            {
                AddErrors(result);
            }
        
            return createOrEdit;
        }

        protected virtual async Task DeleteModelAsync(params object[] keyValues)
        {
            var entity = _repositoryService.Find(keyValues);

            _repositoryService.Remove(entity);
            var result = await _repositoryService.SaveChangesAsync();
            if (!result.Succeeded)
            {
                AddErrors(result);
            }
        }

        public ViewResult MaintenanceView(object model)
        {
            return View("~/Views/Shared/_MaintenanceIndex.cshtml", model);
        }

        protected MaintenanceDefinition Define(IEntity entity)
        {
            var response = new MaintenanceDefinition();
            response.Title = GetFormTitle(entity);
            response.Data = entity == null ? new TEntity() : entity;

            response.LinkedItems = GetLinkedControlers(entity);
           
            response.InquiryAction = string.IsNullOrWhiteSpace(BackToController) ? "":Url.Action(BackToAction, BackToController);
            response.AddAction = string.IsNullOrWhiteSpace(AddAction) ? "": HasPermission(AreaName, ControllerName, AddAction) ? GetUrl(AreaName, ControllerName, AddAction):"";

            response.ViewName = "ItemView";
            return response;
        }
        protected abstract string GetFormTitle(IEntity entity);

        protected virtual List<LinkedItem> GetLinkedControlers(IEntity entity)
        {
            return new List<LinkedItem>();
        }


        protected abstract string AddAction { get; }
        protected abstract string BackToController { get; }
        protected virtual string BackToAction => "Index";
    }


    public class MaintenanceDefinition
    {
        public MaintenanceDefinition()
        {
            LinkedItems = new List<LinkedItem>();
        }
        public string AddAction { get; set; }
        public string LookupAction { get; set; }

        public string DeleteAction { get; set; }

        public string InquiryAction { get; set; }
        public string Title { get; set; }
        public string ViewName { get; set; }
        
        public List<LinkedItem> LinkedItems { get; set; }

        public IEntity Data { get; set; }
    }
}