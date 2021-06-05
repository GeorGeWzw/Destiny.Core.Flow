﻿using DestinyCore.AspNetCore;
using DestinyCore.Enums;
using DestinyCore.Extensions;
using DestinyCore.Permission;
using DestinyCore.Ui;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Destiny.Core.Flow.API.Filters
{

    /// <summary>
    /// 待重写大黄瓜
    /// </summary>
    public class PermissionAuthorizationFilter: IAsyncAuthorizationFilter
    {
        private readonly IAuthorityVerification _authority;
        private readonly IPrincipal _principal;
        private readonly ILogger<PermissionAuthorizationFilter> _logger = null;

        public PermissionAuthorizationFilter(IAuthorityVerification authority, IPrincipal principal, ILogger<PermissionAuthorizationFilter> logger)
        {
            _authority = authority;
            authority.NotNull(nameof(authority));
            _principal = principal;
            _logger = logger;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _logger.LogInformation($"进入权限判断");
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var linkurl = context.HttpContext.Request.Path.Value.Replace("/api/", "");
            var result = new AjaxResult(MessageDefinitionType.Unauthorized, AjaxResultType.Unauthorized);
            if (!action.EndpointMetadata.Any(x => x is AllowAnonymousAttribute))
            {
                if (!_principal.Identity.IsAuthenticated)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Result = new JsonResult(result);
                    return;
                }
                else
                {
                    if (!action.EndpointMetadata.Any(x => x is NoAuthorityVerificationAttribute))
                    {
                        var result1 = (await _authority.IsPermission(linkurl.ToLower()));
                        if (!result1.Success)
                        {
                            ////????不包含的时候怎么返回出去？这个请求终止掉
                            ///
                            _logger.LogError($"此{linkurl}地址没有权限");
                            //result = result1.ToAjaxResult();
                            //context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                            //context.Result.ToJsonResult(result);
                            result.Message = MessageDefinitionType.Uncertified;
                            result.Type = AjaxResultType.Uncertified;
                            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Result = new JsonResult(result);
                            return;
                        }
                    }
                }
            }
            _logger.LogInformation($"权限判断结束");
        }
    }
}