using Infrastructure.Cache;
using Microsoft.AspNetCore.Http;
using OpenAuth.App.Interface;
using System;

namespace OpenAuth.App.SSO
{
    /// <summary>
    /// ʹ�ñ��ص�¼�����ע��IAuthʱ��ֻ��ҪOpenAuth.Mvcһ����Ŀ���ɣ�����webapi��֧��
    /// </summary>
    public class LocalAuth : IAuth
    {
        private IHttpContextAccessor _httpContextAccessor;

        private AuthContextFactory _app;
        private LoginParse _loginParse;
        private ICacheContext _cacheContext;

        public LocalAuth(IHttpContextAccessor httpContextAccessor
            , AuthContextFactory app
            , LoginParse loginParse
            , ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _app = app;
            _loginParse = loginParse;
            _cacheContext = cacheContext;
        }

        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query["Token"];
            if (!String.IsNullOrEmpty(token)) return token;

            token = _httpContextAccessor.HttpContext.Request.Headers["X-Token"];
            if (!String.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
            return cookie ?? String.Empty;
        }

        public bool CheckLogin(string token = "", string otherInfo = "")
        {
            if (string.IsNullOrEmpty(token))
            {
                token = GetToken();
            }

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {
                var result = _cacheContext.Get<UserAuthSession>(token) != null;
                return result;
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
            AuthStrategyContext context = null;
            var user = _cacheContext.Get<UserAuthSession>(GetToken());
            if (user != null)
            {
                context = _app.GetAuthStrategyContext(user.Account);
            }
            return context;
        }

        /// <summary>
        /// ��ȡ��ǰ��¼���û���
        /// <para>ͨ��URL�е�Token������Cookie�е�Token</para>
        /// </summary>
        /// <param name="otherInfo">The otherInfo.</param>
        /// <returns>System.String.</returns>
        public string GetUserName(string otherInfo = "")
        {
            var user = _cacheContext.Get<UserAuthSession>(GetToken());
            if (user != null)
            {
                return user.Account;
            }

            return "";
        }

        /// <summary>
        /// ��¼�ӿ�
        /// </summary>
        /// <param name="appKey">Ӧ�ó���key.</param>
        /// <param name="username">�û���</param>
        /// <param name="pwd">����</param>
        /// <returns>System.String.</returns>
        public LoginResult Login(string appKey, string username, string pwd)
        {
            return _loginParse.Do(new PassportLoginRequest
            {
                AppKey = appKey,
                Account = username,
                Password = pwd
            });
        }

        /// <summary>
        /// ע��
        /// </summary>
        public bool Logout()
        {
            var token = GetToken();
            if (String.IsNullOrEmpty(token)) return true;

            try
            {
                _cacheContext.Remove(token);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}