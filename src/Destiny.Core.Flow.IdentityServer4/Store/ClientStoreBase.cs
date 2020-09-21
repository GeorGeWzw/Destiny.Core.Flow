﻿using Destiny.Core.Flow.Extensions;
using Destiny.Core.Flow.Model.DestinyIdentityServer4;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Destiny.Core.Flow.IdentityServer.Store
{
    public abstract class ClientStoreBase : IClientStore
    {
        private readonly ILogger<ClientStoreBase> _logger;

        private readonly IEFCoreRepository<Client, Guid> _clientRepository;

        protected ClientStoreBase(ILogger<ClientStoreBase> logger, IEFCoreRepository<Client, Guid> clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        public async Task<IdentityServer4.Models.Client> FindClientByIdAsync(string clientId)
        {
            var client = await _clientRepository
                .Entities.Where(x => x.ClientId == clientId)
                .FirstOrDefaultAsync();
            return client?.MapTo<IdentityServer4.Models.Client>();
        }
    }
}
