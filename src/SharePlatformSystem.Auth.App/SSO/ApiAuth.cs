// ***********************************************************************
// Assembly         : OpenAuth.App
// Author           : ����
// Created          : 07-05-2018
//
// Last Modified By : ����
// Last Modified On : 07-05-2018
// ***********************************************************************
// <copyright file="ApiAuth.cs" company="OpenAuth.App">
//     Copyright (c) http://www.openauth.me. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenAuth.App.Interface;

namespace OpenAuth.App.SSO
{
    /// <summary>
    /// ��������վ��¼��֤��
    /// <para>��¼ʱ��</para>
    /// <code>
    ///  var result = IAuth.Login(AppKey, username, password);
    ///  if (result.Success)
    ///       return Redirect("/home/index?Token=" + result.Token);
    /// </code>
    /// </summary>
    public class ApiAuth :IAuth
    {
        private IOptions<AppSetting> _appConfiguration;
        private HttpHelper _helper;
        private IHttpContextAccessor _httpContextAccessor;
        private AuthContextFactory _authContextFactory;


        public ApiAuth(IOptions<AppSetting> appConfiguration
            , IHttpContextAccessor httpContextAccessor
            ,AuthContextFactory authContextFactory
            )
        {
            _appConfiguration = appConfiguration;
            _helper = new HttpHelper(_appConfiguration.Value.SSOPassport);
            _authContextFactory = authContextFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query["Token"];
            if (!String.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
            return cookie == null ? String.Empty : cookie;
        }
        /// <summary>
        /// ͨ��WebApi����token�Ƿ���Ч
        /// </summary>
        /// <remarks>http://www.openauth.me</remarks>
        public bool CheckLogin(string token="", string otherInfo = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                token = GetToken();
            }

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
         
            var requestUri = String.Format("/api/Check/GetStatus?token={0}&requestid={1}", token, otherInfo);

            try
            {
                var value = _helper.Get(null, requestUri);
                var result = JsonHelper.Instance.Deserialize<Response<bool>>(value);
                if (result.Code == 200)
                {
                    return result.Result;
                }
                throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ��¼���û���Ϣ
        /// <para>ͨ��URL�е�Token������Cookie�е�Token</para>
        /// </summary>
        /// <param name="otherInfo">The otherInfo.</param>
        /// <returns>LoginUserVM.</returns>
        public AuthStrategyContext GetCurrentUser(string otherInfo = "")
        {
            string username = GetUserName();
            return _authContextFactory.GetAuthStrategyContext(username);
        }


        /// <summary>
        /// ��ȡWebApi�е�ǰ��¼���û���
        /// <para>ͨ��URL�е�Token������Cookie�е�Token</para>
        /// </summary>
        /// <param name="otherInfo">The otherInfo.</param>
        /// <returns>System.String.</returns>
        public string GetUserName(string otherInfo = "")
        {
            var requestUri = String.Format("/api/Check/GetUserName?token={0}&requestid={1}", GetToken(), otherInfo);

            try
            {
                var value = _helper.Get(null, requestUri);
                var result = JsonHelper.Instance.Deserialize<Response<string>>(value);
                if (result.Code == 200)
                {
                    return result.Result;
                }
                throw new Exception(result.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// ͨ��WebApi��¼���û���Ϣ�����webapi��
        /// </summary>
        /// <param name="appKey">Ӧ�ó���key.</param>
        /// <param name="username">�û���</param>
        /// <param name="pwd">����</param>
        /// <returns>System.String.</returns>
        public LoginResult Login(string appKey, string username, string pwd)
        {
            var requestUri = "/api/Check/Login";

            try
            {
                var value = _helper.Post(new
                {
                    AppKey = appKey,
                    Account = username,
                    Password = pwd
                }, requestUri);

                var result = JsonHelper.Instance.Deserialize<LoginResult>(value);
                return result;
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// ע��
        /// </summary>
        public bool Logout()
        {
            var token = GetToken();
            if (String.IsNullOrEmpty(token)) return true;

            var requestUri = String.Format("/api/Check/Logout?token={0}&requestid={1}", token, "");

            try
            {
                var value = _helper.Post(null, requestUri);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}