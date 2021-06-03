﻿using DestinyCore.Entity;
using System;
using System.ComponentModel;

namespace Destiny.Core.Flow
{
    public abstract class UserRoleBase<TUserKey, TRoleKey> : EntityBase<Guid>
          where TUserKey : IEquatable<TUserKey>
          where TRoleKey : IEquatable<TRoleKey>
    {
        [DisplayName("用户编号")]
        public TUserKey UserId { get; set; }

        [DisplayName("角色编号")]
        public TRoleKey RoleId { get; set; }
    }
}