using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using App.CoreLib.EF;
using App.CoreLib.EF.Data;
using App.CoreLib.EF.Data.Entity;

namespace App.Core.Web.AppControllers
{
    public abstract class InquiryBase<TRequest, TResponse, TQuery> : QueryControllerBase<TRequest, TResponse, TQuery>
        where TRequest : IQueryRequest, new()
        where TResponse : class, new()
        where TQuery : IQueryBase<TResponse>
    {
        public InquiryBase(TQuery query) : base(query)
        {
        }

        public ViewResult InquiryView(object model)
        {
            return View("~/Views/Shared/_InquiryIndex.cshtml", model);
        }
        protected abstract List<FieldDefinition> FieldDefinitions { get; }
        protected abstract List<string> KeyFields { get; }

        protected virtual string MaintenanceController => "";

        protected virtual string MaintenanceEditAction => "";

        protected virtual string DeleteAction => "";
        protected virtual string BackToController => "";
        protected virtual string BackToAction => "Index";


        protected abstract string GetFormTitle(IEntity entity);

        protected virtual List<LinkedItem> GetLinkedControlers(IEntity entity)
        {
            return new List<LinkedItem>();
        }

        protected string SerializeFields(List<FieldDefinition> columns)
        {
            return JsonConvert.SerializeObject(columns, Formatting.None);
        }
        protected string SerializeKeys(List<string> columns)
        {
            return JsonConvert.SerializeObject(columns, Formatting.None);
        }
        protected string SerializeFields(IEntity columns)
        {
            return JsonConvert.SerializeObject(columns, Formatting.None);
        }
        protected virtual InquiryDefinition Define(IEntity entity)
        {

            var formDef = new InquiryDefinition();
            formDef.Title = GetFormTitle(entity);
            formDef.ModelName = ControllerName;
            formDef.FormKeys = SerializeFields(entity);
            formDef.KeyColumns = SerializeKeys(KeyFields);
            formDef.GridColumns = SerializeFields(FieldDefinitions);

            formDef.ReadDataAction = GetUrl(AreaName, ControllerName, "Index");
            formDef.DeleteAction = string.IsNullOrWhiteSpace(DeleteAction)?string.Empty: GetUrl(AreaName, ControllerName, DeleteAction);
            formDef.BackToAction = string.IsNullOrWhiteSpace(BackToController) ?string.Empty: GetUrl(AreaName, BackToController, BackToAction);
            if (!string.IsNullOrWhiteSpace(MaintenanceController))
            {
                formDef.MaintenanceAction = string.IsNullOrWhiteSpace(MaintenanceEditAction) ? "" : HasPermission(AreaName, MaintenanceController, MaintenanceEditAction) ? GetUrl(AreaName, MaintenanceController, MaintenanceEditAction) : "";
            }

            formDef.LinkedItems = GetLinkedControlers(entity);
            return formDef;
        }
    }


    public class InquiryDefinition
    {
        public InquiryDefinition()
        {
            LinkedItems = new List<LinkedItem>();
        }
        public string MaintenanceAction { get; set; }

        public string ReadDataAction { get; set; }
        public string DeleteAction { get; set; }

        public string Title { get; set; }
        public List<LinkedItem> LinkedItems { get; set; }

        public string GridColumns { get; set; }
        public string KeyColumns { get; set; }

        public string ModelName { get; set; }

        public string FormKeys { get; set; }

        public string BackToAction { get; set; }
    }
}