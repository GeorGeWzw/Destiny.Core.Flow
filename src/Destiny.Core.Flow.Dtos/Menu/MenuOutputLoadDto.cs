﻿using AutoMapper;
using DestinyCore.Entity;
using Destiny.Core.Flow.Model.Entities.Menu;
using System;
using System.ComponentModel;

namespace Destiny.Core.Flow.Dtos.Menu
{
    /// <summary>
    /// 编辑或修改一个菜单输出Dto
    /// </summary>
    [AutoMap(typeof(MenuEntity))]
    public class MenuOutputLoadDto : OutputDto<Guid>
    {
        public MenuOutputLoadDto()
        {
         
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [DisplayName("菜单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 路由地址(前端)
        /// </summary>
        [DisplayName("路由地址(前端)")]
        public string Path { get; set; }

        /// <summary>
        ///获取或设置 描述
        /// </summary>
        [DisplayName("描述")]
        public virtual string Description { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        [DisplayName("组件地址")]
        public string Component { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        [DisplayName("深度")]
        public int Depth { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [DisplayName("菜单图标")]
        public string Icon { get; set; }

        ///// <summary>
        ///// 功能Id
        ///// </summary>
        //public Guid[] FunctionIds { get; set; }

        /// <summary>
        /// 当前菜单以上所有的父级
        /// </summary>
        [DisplayName("当前菜单以上所有的父级")]
        public string ParentNumber { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [DisplayName("父级菜单ID")]
        public Guid ParentId { get; set; }

        [DisplayName("类型")]
        public MenuEnum Type { get; set; }

        /// <summary>
        /// 模板页
        /// </summary>
        [DisplayName("模板页")]
        public string Layout { get; set; }


        /// <summary>
        /// 是否隐藏
        /// </summary>
        [DisplayName("是否隐藏")]
        public bool IsHide { get; set; }

        /// <summary>
        /// 事件名
        /// </summary>
        [DisplayName("事件名")]
        public string EventName { get; set; }
    }
}