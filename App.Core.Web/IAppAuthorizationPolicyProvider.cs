using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Web
{
    public interface IAppAuthorizationPolicyProvider
    {
        string Name { get; }
        AuthorizationPolicy GetAuthorizationPolicy();
    }
}
