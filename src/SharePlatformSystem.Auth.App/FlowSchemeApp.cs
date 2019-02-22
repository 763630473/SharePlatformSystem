using SharePlatformSystem.Auth.EfRepository.Domain;
using SharePlatformSystem.Auth.EfRepository.Interface;
using SharePlatformSystem.Auth.App.Request;
using SharePlatformSystem.Auth.App.Response;
using System;

namespace SharePlatformSystem.Auth.App
{
    public class FlowSchemeApp :BaseApp<FlowScheme>
    {
        
        public void Add(FlowScheme flowScheme)
        {
            Repository.Add(flowScheme);
        }

        public void Update(FlowScheme flowScheme)
        {
            UnitWork.Update<FlowScheme>(u => u.Id == flowScheme.Id, u => new FlowScheme
            {
                SchemeContent = flowScheme.SchemeContent,
                SchemeName = flowScheme.SchemeName,
                ModifyDate = DateTime.Now,
                FrmId = flowScheme.FrmId,
                Disabled = flowScheme.Disabled
            });
        }

        public TableData Load(QueryFlowSchemeListReq request)
        {
            return new TableData
            {
                count = Repository.GetCount(null),
                data = Repository.Find(request.page, request.limit, "CreateDate desc")
            };
        }

        public FlowSchemeApp(IUnitWork unitWork, IRepository<FlowScheme> repository) : base(unitWork, repository)
        {
        }
    }
}
