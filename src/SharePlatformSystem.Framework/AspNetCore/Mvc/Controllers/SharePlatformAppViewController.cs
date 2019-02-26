using System;
using Microsoft.AspNetCore.Mvc;
using SharePlatformSystem.Auditing;
using SharePlatformSystem.Core.Exceptions;
using SharePlatformSystem.Domain.Uow;

namespace SharePlatformSystem.Framework.AspNetCore.Mvc.Controllers
{
    public class SharePlatformAppViewController : SharePlatformController
    {
        [DisableAuditing]
        [UnitOfWork(IsDisabled = true)]
        public ActionResult Load(string viewUrl)
        {
            if (viewUrl.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(viewUrl));
            }

            return View(viewUrl.EnsureStartsWith('~'));
        }
    }
}
