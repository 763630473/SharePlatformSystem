using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SharePlatform.Auth.EfRepository.Core;

namespace SharePlatform.Auth.EfRepository.Domain
{
    /// <summary>
	/// 分类表，表示一个全集，比如：男、女、未知。关联的分类类型表示按什么进行的分类，如：按照性别对人类对象集进行分类
	/// </summary>
      [Table("Category")]
    public partial class Category : Entity
    {
        public Category()
        {
          this.Name= string.Empty;
          this.SortNo= 0;
          this.Icon= string.Empty;
          this.Description= string.Empty;
          this.TypeId= string.Empty;
        }

        /// <summary>
	    /// 名称
	    /// </summary>
         [Description("名称")]
        public string Name { get; set; }
        /// <summary>
	    /// 是否可用
	    /// </summary>
         [Description("是否可用")]
        public bool Disabled { get; set; }
        /// <summary>
	    /// 排序号
	    /// </summary>
         [Description("排序号")]
        public int SortNo { get; set; }
        /// <summary>
	    /// 分类图标
	    /// </summary>
         [Description("分类图标")]
        public string Icon { get; set; }
        /// <summary>
	    /// 分类描述
	    /// </summary>
         [Description("分类描述")]
        public string Description { get; set; }
        /// <summary>
	    /// 分类类型ID
	    /// </summary>
         [Description("分类类型ID")]
        public string TypeId { get; set; }

    }
}