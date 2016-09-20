﻿using Autofac;
using Autofac.Integration.Mvc;
using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Cactus.Controllers.Expand
{
    //后段管理使用
    public class AdminBaseController : BaseController
    {
        public IUserServer userServer = IocHelper.AutofacResolveNamed<IUserServer>("UserServer");
        /// <summary>
        /// 登录用户信息
        /// </summary>
        protected User LoginUser = null;
        protected string m_token = "";

        /// <summary>
        /// 获取已验证用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            var token = CookieHelper.GetCookieValue("Admin");
            if (!string.IsNullOrEmpty(token))
            {
                m_token = token;
                //没有系统缓存重新登录
                this.LoginUser = CacheHelper.GetCache(Constant.CacheKey.LoginAdminInfoCacheKey + "_" + token) as User;
            }
        }
    }
}
