using SharePlatformSystem.Auth.EfRepository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharePlatformSystem.Auth.EfRepository.Domain
{
    /// <summary>
	/// 工作流实例操作记录
	/// </summary>
      [Table("FlowInstanceOperationHistory")]
    public partial class FlowInstanceOperationHistory : Entity
    {
        public FlowInstanceOperationHistory()
        {
          this.InstanceId= string.Empty;
          this.Content= string.Empty;
          this.CreateDate= DateTime.Now;
          this.CreateUserId= string.Empty;
          this.CreateUserName= string.Empty;
        }

        /// <summary>
	    /// 实例进程Id
	    /// </summary>
         [Description("实例进程Id")]
        public string InstanceId { get; set; }
        /// <summary>
	    /// 操作内容
	    /// </summary>
         [Description("操作内容")]
        public string Content { get; set; }
        /// <summary>
	    /// 创建时间
	    /// </summary>
         [Description("创建时间")]
        public System.DateTime CreateDate { get; set; }
        /// <summary>
	    /// 创建用户主键
	    /// </summary>
         [Description("创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
	    /// 创建用户
	    /// </summary>
         [Description("创建用户")]
        public string CreateUserName { get; set; }

    }
}