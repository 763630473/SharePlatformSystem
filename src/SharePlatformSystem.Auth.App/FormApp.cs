using System;
using System.Linq;
using SharePlatform.Auth.EfRepository.Domain;
using SharePlatform.Auth.EfRepository.Interface;
using SharePlatformSystem.Auth.App.Request;
using SharePlatformSystem.Auth.App.Response;
using SharePlatformSystem.Infrastructure;

namespace SharePlatformSystem.Auth.App
{
    public class FormApp : BaseApp<Form>
    {
        private RevelanceManagerApp _revelanceApp;

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryFormListReq request)
        {
            var result = new TableData();
            var forms = UnitWork.Find<Form>(null);
            if (!string.IsNullOrEmpty(request.key))
            {
                forms = forms.Where(u => u.Name.Contains(request.key) || u.Id.Contains(request.key));
            }

            result.data = forms.OrderBy(u => u.Name)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit).ToList();
            result.count = forms.Count();
            return result;
        }

        public void Add(Form obj)
        {
            UnitWork.Add(obj);
            if (!string.IsNullOrEmpty(obj.DbName))
            {
                UnitWork.ExecuteSql(FormUtil.GetSql(obj));
            }
            UnitWork.Save();
        }

        public void Update(Form obj)
        {
            Repository.Update(u => u.Id == obj.Id, u => new Form
            {
                ContentData = obj.ContentData,
                Content = obj.Content,
                ContentParse = obj.ContentParse,
                Name = obj.Name,
                Enabled = obj.Enabled,
                DbName = obj.DbName,
                SortCode = obj.SortCode,
                Description = obj.Description,
                ModifyDate = DateTime.Now
            });

            if (!string.IsNullOrEmpty(obj.DbName))
            {
                UnitWork.ExecuteSql(FormUtil.GetSql(obj));
            }
        }

        public FormResp FindSingle(string id)
        {
            var form = Get(id);
            return form.MapTo<FormResp>();
        }

        public FormApp(IUnitWork unitWork, IRepository<Form> repository,
            RevelanceManagerApp app) : base(unitWork, repository)
        {
            _revelanceApp = app;
        }
    }
}