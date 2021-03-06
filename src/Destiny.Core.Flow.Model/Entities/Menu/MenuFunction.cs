﻿using DestinyCore.Data;
using DestinyCore.Entity;
using System;
using System.ComponentModel;

namespace Destiny.Core.Flow.Model.Entities.Menu
{
    [DisplayName("菜单功能")]
    public class MenuFunction : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public MenuFunction()
        {
            Id = ComnGuid.NewGuid();
        }

        #region 公共字段

        /// <summary>
        /// 获取或设置 最后修改用户
        /// </summary>
        [DisplayName("最后修改用户")]
        public virtual Guid? LastModifierUserId { get; set; }

        /// <summary>
        /// 获取或设置 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public virtual DateTime? LastModifionTime { get; set; }

        /// <summary>
        ///获取或设置 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        ///获取或设置 创建用户ID
        /// </summary>
        [DisplayName("创建用户ID")]
        public virtual Guid? CreatorUserId { get; set; }

        /// <summary>
        ///获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreatedTime { get; set; }

        #endregion 公共字段

        [DisplayName("菜单ID")]
        public Guid MenuId { get; set; }

        [DisplayName("功能ID")]
        public Guid FunctionId { get; set; }
    }
}