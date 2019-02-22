
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SharePlatform.Auth.EfRepository.Core;

namespace SharePlatform.Auth.EfRepository.Domain
{
    /// <summary>
	/// 出入库信息表
	/// </summary>
      [Table("Stock")]
    public partial class Stock : Entity
    {
        public Stock()
        {
          this.Name= string.Empty;
          this.Number= 0;
          this.Price= 0;
          this.Status= 0;
          this.Viewable= string.Empty;
          this.User= string.Empty;
          this.Time= DateTime.Now;
          this.OrgId= string.Empty;
        }

        /// <summary>
	    /// 产品名称
	    /// </summary>
         [Description("产品名称")]
        public string Name { get; set; }
        /// <summary>
	    /// 产品数量
	    /// </summary>
         [Description("产品数量")]
        public int Number { get; set; }
        /// <summary>
	    /// 产品单价
	    /// </summary>
         [Description("产品单价")]
        public decimal Price { get; set; }
        /// <summary>
	    /// 出库/入库
	    /// </summary>
         [Description("出库/入库")]
        public int Status { get; set; }
        /// <summary>
	    /// 可见范围
	    /// </summary>
         [Description("可见范围")]
        public string Viewable { get; set; }
        /// <summary>
	    /// 操作人
	    /// </summary>
         [Description("操作人")]
        public string User { get; set; }
        /// <summary>
	    /// 操作时间
	    /// </summary>
         [Description("操作时间")]
        public System.DateTime Time { get; set; }
        /// <summary>
	    /// 组织ID
	    /// </summary>
         [Description("组织ID")]
        public string OrgId { get; set; }

    }
}