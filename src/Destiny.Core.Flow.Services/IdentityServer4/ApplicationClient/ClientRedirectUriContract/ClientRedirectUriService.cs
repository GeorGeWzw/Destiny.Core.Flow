﻿using DestinyCore.Exceptions;
using DestinyCore.Extensions;
using Destiny.Core.Flow.IServices;
using Destiny.Core.Flow.Model.DestinyIdentityServer4;
using DestinyCore.Ui;
using IDN.Services.BasicsService.Dtos.IdentityServer4.ClientApplication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DestinyCore;

namespace Destiny.Core.Flow.Services
{
    public class ClientRedirectUriService : IClientRedirectUriService
    {
        private readonly IRepository<ClientRedirectUri, Guid> _clientRedirectUriRepository;

        public ClientRedirectUriService(IRepository<ClientRedirectUri, Guid> clientRedirectUriRepository)
        {
            _clientRedirectUriRepository = clientRedirectUriRepository;
        }
        public async Task<OperationResponse> CreatAsync(ClientRedirectUriInputDto input)
        {
            input.NotNull(nameof(input));
            return await _clientRedirectUriRepository.InsertAsync(input, async f =>
            {
                bool isExist = await _clientRedirectUriRepository.Entities.Where(x => x.RedirectUri == input.RedirectUri && x.ClientId == input.ClientId).AnyAsync();
                if (isExist)
                    throw new AppException($"此回调【{input.RedirectUri}】已存在!!!");
            });
        }
        //public async Task<OperationResponse> CreatAsync(PageRequest request)
        //{
        //    //input.NotNull(nameof(input));
        //    //return await _clientRedirectUriRepository.InsertAsync(input, async f =>
        //    //{
        //    //    bool isExist = await _clientRedirectUriRepository.Entities.Where(x => x.RedirectUri == input.RedirectUri && x.ClientId == input.ClientId).AnyAsync();
        //    //    if (isExist)
        //    //        throw new AppException($"此回调【{input.RedirectUri}】已存在!!!");
        //    //});
        //}
    }
}
