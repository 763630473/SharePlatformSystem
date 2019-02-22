using System;
using System.Collections.Generic;
using System.Linq;
using SharePlatform.Auth.EfRepository.Domain;
using SharePlatform.Auth.EfRepository.Interface;
using SharePlatformSystem.Auth.App.Request;

namespace SharePlatformSystem.Auth.App
{
    /// <summary>
    /// 分类管理
    /// </summary>
    public class AppManager : BaseApp<Application>
    {
        public void Add(Application Application)
        {
            if (string.IsNullOrEmpty(Application.Id))
            {
                Application.Id = Guid.NewGuid().ToString();
            }
            Repository.Add(Application);
        }

        public void Update(Application Application)
        {
            Repository.Update(Application);
        }


        public List<Application> GetList(QueryAppListReq request)
        {
            var applications =  UnitWork.Find<Application>(null) ;
           
            return applications.ToList();
        }

        public AppManager(IUnitWork unitWork, IRepository<Application> repository) : base(unitWork, repository)
        {
        }
    }
}