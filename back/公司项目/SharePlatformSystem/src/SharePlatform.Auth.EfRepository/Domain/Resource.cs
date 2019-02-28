﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SharePlatformSystem.Auth.EfRepository.Core;

namespace SharePlatformSystem.Auth.EfRepository.Domain
{
    /// <summary>
	/// 资源表
	/// </summary>
      [Table("Resource")]
    public partial class Resource : TreeEntity
    {
        public Resource()
        {
          this.CascadeId= string.Empty;
          this.Name= string.Empty;
          this.SortNo= 0;
          this.Description= string.Empty;
          this.ParentName= string.Empty;
          this.ParentId= string.Empty;
          this.AppId= string.Empty;
          this.AppName= string.Empty;
          this.TypeName= string.Empty;
          this.TypeId= string.Empty;
        }

        /// <summary>
	    /// 排序号
	    /// </summary>
         [Description("排序号")]
        public int SortNo { get; set; }
        /// <summary>
	    /// 描述
	    /// </summary>
         [Description("描述")]
        public string Description { get; set; }
        /// <summary>
	    /// 资源所属应用ID
	    /// </summary>
         [Description("资源所属应用ID")]
        public string AppId { get; set; }
        /// <summary>
	    /// 所属应用名称
	    /// </summary>
         [Description("所属应用名称")]
        public string AppName { get; set; }
        /// <summary>
	    /// 分类名称
	    /// </summary>
         [Description("分类名称")]
        public string TypeName { get; set; }
        /// <summary>
	    /// 分类ID
	    /// </summary>
         [Description("分类ID")]
        public string TypeId { get; set; }
        /// <summary>
	    /// 是否可用
	    /// </summary>
         [Description("是否可用")]
        public bool Disable { get; set; }

    }
}