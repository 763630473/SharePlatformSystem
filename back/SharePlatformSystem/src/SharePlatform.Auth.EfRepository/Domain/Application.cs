
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SharePlatformSystem.Auth.EfRepository.Core;

namespace SharePlatformSystem.Auth.EfRepository.Domain
{
    /// <summary>
	/// 应用
	/// </summary>
      [Table("Application")]
    public partial class Application : Entity
    {
        public Application()
        {
          this.Name= string.Empty;
          this.AppSecret= string.Empty;
          this.Description= string.Empty;
          this.Icon= string.Empty;
          this.CreateTime= DateTime.Now;
          this.CreateUser= string.Empty;
        }

        /// <summary>
	    /// 应用名称
	    /// </summary>
         [Description("应用名称")]
        public string Name { get; set; }
        /// <summary>
	    /// 应用密钥
	    /// </summary>
         [Description("应用密钥")]
        public string AppSecret { get; set; }
        /// <summary>
	    /// 应用描述
	    /// </summary>
         [Description("应用描述")]
        public string Description { get; set; }
        /// <summary>
	    /// 应用图标
	    /// </summary>
         [Description("应用图标")]
        public string Icon { get; set; }
        /// <summary>
	    /// 是否可用
	    /// </summary>
         [Description("是否可用")]
        public bool Disable { get; set; }
        /// <summary>
	    /// 创建日期
	    /// </summary>
         [Description("创建日期")]
        public System.DateTime CreateTime { get; set; }
        /// <summary>
	    /// 创建人
	    /// </summary>
         [Description("创建人")]
        public string CreateUser { get; set; }

    }
}