
using SharePlatform.Auth.EfRepository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharePlatform.Auth.EfRepository.Domain
{
    /// <summary>
	/// 分类类型
	/// </summary>
      [Table("CategoryType")]
    public partial class CategoryType : Entity
    {
        public CategoryType()
        {
          this.Name= string.Empty;
          this.CreateTime= DateTime.Now;
        }

        /// <summary>
	    /// 名称
	    /// </summary>
         [Description("名称")]
        public string Name { get; set; }
        /// <summary>
	    /// 创建时间
	    /// </summary>
         [Description("创建时间")]
        public System.DateTime CreateTime { get; set; }

    }
}