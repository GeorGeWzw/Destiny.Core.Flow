﻿using Destiny.Core.Flow.Model.DestinyIdentityServer4;
using DestinyCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Destiny.Core.Flow.Model.DestinyIdentityServer4.EntityConfigurations.IdentityServer4.Resources
{
    public class ApiResourceClaimConfiguration : EntityMappingConfiguration<ApiResourceClaim, Guid>
    {
        public override void Map(EntityTypeBuilder<ApiResourceClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable("ApiResourceClaim");
        }
    }
}
