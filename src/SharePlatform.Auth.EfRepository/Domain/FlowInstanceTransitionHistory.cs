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
	/// 工作流实例流转历史记录
	/// </summary>
      [Table("FlowInstanceTransitionHistory")]
    public partial class FlowInstanceTransitionHistory : Entity
    {
        public FlowInstanceTransitionHistory()
        {
          this.InstanceId= string.Empty;
          this.FromNodeId= string.Empty;
          this.FromNodeName= string.Empty;
          this.ToNodeId= string.Empty;
          this.ToNodeName= string.Empty;
          this.TransitionSate= 0;
          this.IsFinish= 0;
          this.CreateDate= DateTime.Now;
          this.CreateUserId= string.Empty;
          this.CreateUserName= string.Empty;
        }

        /// <summary>
	    /// 实例Id
	    /// </summary>
         [Description("实例Id")]
        public string InstanceId { get; set; }
        /// <summary>
	    /// 开始节点Id
	    /// </summary>
         [Description("开始节点Id")]
        public string FromNodeId { get; set; }
        /// <summary>
	    /// 开始节点类型
	    /// </summary>
         [Description("开始节点类型")]
        public int? FromNodeType { get; set; }
        /// <summary>
	    /// 开始节点名称
	    /// </summary>
         [Description("开始节点名称")]
        public string FromNodeName { get; set; }
        /// <summary>
	    /// 结束节点Id
	    /// </summary>
         [Description("结束节点Id")]
        public string ToNodeId { get; set; }
        /// <summary>
	    /// 结束节点类型
	    /// </summary>
         [Description("结束节点类型")]
        public int? ToNodeType { get; set; }
        /// <summary>
	    /// 结束节点名称
	    /// </summary>
         [Description("结束节点名称")]
        public string ToNodeName { get; set; }
        /// <summary>
	    /// 转化状态
	    /// </summary>
         [Description("转化状态")]
        public int TransitionSate { get; set; }
        /// <summary>
	    /// 是否结束
	    /// </summary>
         [Description("是否结束")]
        public int IsFinish { get; set; }
        /// <summary>
	    /// 转化时间
	    /// </summary>
         [Description("转化时间")]
        public System.DateTime CreateDate { get; set; }
        /// <summary>
	    /// 操作人Id
	    /// </summary>
         [Description("操作人Id")]
        public string CreateUserId { get; set; }
        /// <summary>
	    /// 操作人名称
	    /// </summary>
         [Description("操作人名称")]
        public string CreateUserName { get; set; }

    }
}