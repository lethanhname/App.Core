using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using App.Core.Contract.Security;
using App.CoreLib.EF;
using App.CoreLib.EF.Data.Entity;
using App.CoreLib.EF.Messages;

namespace App.Core.Web.AppControllers
{
  public abstract class ApiControllerBase : ControllerBase
  {
    protected EntityResult GetModelErrors()
    {
      EntityResult result = new EntityResult();
      if (!ModelState.IsValid)
      {
        var errors = new List<EntityError>();
        foreach (var error in ModelState)
        {
          foreach (var message in error.Value.Errors)
          {
            errors.Add(new EntityError { Code = error.Key, Description = message.ErrorMessage });
          }
        }
        result = EntityResult.Failed(errors.ToArray());
      }
      return result;
    }
  }
}