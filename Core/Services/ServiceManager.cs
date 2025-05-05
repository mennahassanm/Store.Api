﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    public class ServiceManager(
        IUnitOfWork unitOfWork ,
        IMapper mapper,
        IBasketRepository basketRepository,
        ICacheRepository cacheRepository,
        UserManager<AppUser> userManager
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(unitOfWork , mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(userManager);
    }
}
