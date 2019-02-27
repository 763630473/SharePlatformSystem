﻿
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SharePlatformSystem.Auth.EfRepository.Core;

namespace SharePlatformSystem.Auth.EfRepository.Domain
{
    /// <summary>
	/// 工作流流程实例表
	/// </summary>
      [Table("FlowInstance")]
    public partial class FlowInstance : Entity
    {
        public FlowInstance()
        {
          this.InstanceSchemeId= string.Empty;
          this.Code= string.Empty;
          this.CustomName= string.Empty;
          this.ActivityId= string.Empty;
          this.ActivityName= string.Empty;
          this.PreviousId= string.Empty;
          this.SchemeContent= string.Empty;
          this.SchemeId= string.Empty;
          this.DbName= string.Empty;
          this.FrmData= string.Empty;
          this.FrmType= 0;
          this.FrmContentData= string.Empty;
          this.FrmContentParse= string.Empty;
          this.FrmId= string.Empty;
          this.SchemeType= string.Empty;
          this.Disabled= 0;
          this.CreateDate= DateTime.Now;
          this.CreateUserId= string.Empty;
          this.CreateUserName= string.Empty;
          this.FlowLevel= 0;
          this.Description= string.Empty;
          this.IsFinish= 0;
          this.MakerList= string.Empty;
        }

        /// <summary>
	    /// 流程实例模板Id
	    /// </summary>
         [Description("流程实例模板Id")]
        public string InstanceSchemeId { get; set; }
        /// <summary>
	    /// 实例编号
	    /// </summary>
         [Description("实例编号")]
        public string Code { get; set; }
        /// <summary>
	    /// 自定义名称
	    /// </summary>
         [Description("自定义名称")]
        public string CustomName { get; set; }
        /// <summary>
	    /// 当前节点ID
	    /// </summary>
         [Description("当前节点ID")]
        public string ActivityId { get; set; }
        /// <summary>
	    /// 当前节点类型（0会签节点）
	    /// </summary>
         [Description("当前节点类型（0会签节点）")]
        public int? ActivityType { get; set; }
        /// <summary>
	    /// 当前节点名称
	    /// </summary>
         [Description("当前节点名称")]
        public string ActivityName { get; set; }
        /// <summary>
	    /// 前一个ID
	    /// </summary>
         [Description("前一个ID")]
        public string PreviousId { get; set; }
        /// <summary>
	    /// 流程模板内容
	    /// </summary>
         [Description("流程模板内容")]
        public string SchemeContent { get; set; }
        /// <summary>
	    /// 流程模板ID
	    /// </summary>
         [Description("流程模板ID")]
        public string SchemeId { get; set; }
        /// <summary>
	    /// 数据库名称
	    /// </summary>
         [Description("数据库名称")]
        public string DbName { get; set; }
        /// <summary>
	    /// 表单数据
	    /// </summary>
         [Description("表单数据")]
        public string FrmData { get; set; }
        /// <summary>
	    /// 表单类型
	    /// </summary>
         [Description("表单类型")]
        public int FrmType { get; set; }
        /// <summary>
	    /// 表单中的控件属性描述
	    /// </summary>
         [Description("表单中的控件属性描述")]
        public string FrmContentData { get; set; }
        /// <summary>
	    /// 表单控件位置模板
	    /// </summary>
         [Description("表单控件位置模板")]
        public string FrmContentParse { get; set; }
        /// <summary>
	    /// 表单ID
	    /// </summary>
         [Description("表单ID")]
        public string FrmId { get; set; }
        /// <summary>
	    /// 流程类型
	    /// </summary>
         [Description("流程类型")]
        public string SchemeType { get; set; }
        /// <summary>
	    /// 有效标志
	    /// </summary>
         [Description("有效标志")]
        public int Disabled { get; set; }
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
        /// <summary>
	    /// 等级
	    /// </summary>
         [Description("等级")]
        public int FlowLevel { get; set; }
        /// <summary>
	    /// 实例备注
	    /// </summary>
         [Description("实例备注")]
        public string Description { get; set; }
        /// <summary>
	    /// 是否完成
	    /// </summary>
         [Description("是否完成")]
        public int IsFinish { get; set; }
        /// <summary>
	    /// 执行人
	    /// </summary>
         [Description("执行人")]
        public string MakerList { get; set; }

    }
}