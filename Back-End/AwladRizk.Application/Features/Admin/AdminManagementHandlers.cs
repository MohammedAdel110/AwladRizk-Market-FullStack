using AutoMapper;
using MediatR;
using AwladRizk.Application.DTOs;
using AwladRizk.Application.Features.Admin.Commands;
using AwladRizk.Application.Features.Admin.Queries;
using AwladRizk.Domain.Entities;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Application.Features.Admin;

public sealed class CreateProductHandler(IRepository<Product> productRepository, IProductRepository productReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            NameAr = request.NameAr.Trim(),
            NameEn = request.NameEn.Trim(),
            Price = request.Price,
            OldPrice = request.OldPrice,
            ImageUrl = request.ImageUrl.Trim(),
            IsOnSale = request.IsOnSale,
            StockQty = request.StockQty,
            CategoryId = request.CategoryId
        };

        await productRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var withCategory = await productReadRepository.GetWithCategoryByIdAsync(entity.Id, cancellationToken) ?? entity;
        return mapper.Map<ProductDto>(withCategory);
    }
}

public sealed class UpdateProductHandler(IRepository<Product> productRepository, IProductRepository productReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateProductCommand, ProductDto?>
{
    public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;

        entity.NameAr = request.NameAr.Trim();
        entity.NameEn = request.NameEn.Trim();
        entity.Price = request.Price;
        entity.OldPrice = request.OldPrice;
        entity.ImageUrl = request.ImageUrl.Trim();
        entity.IsOnSale = request.IsOnSale;
        entity.StockQty = request.StockQty;
        entity.CategoryId = request.CategoryId;

        productRepository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var withCategory = await productReadRepository.GetWithCategoryByIdAsync(entity.Id, cancellationToken) ?? entity;
        return mapper.Map<ProductDto>(withCategory);
    }
}

public sealed class DeleteProductHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return false;
        productRepository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class CreateOfferHandler(IRepository<Offer> offerRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateOfferCommand, OfferDto>
{
    public async Task<OfferDto> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = new Offer
        {
            TitleAr = request.TitleAr.Trim(),
            TitleEn = request.TitleEn.Trim(),
            DescriptionAr = request.DescriptionAr.Trim(),
            DescriptionEn = request.DescriptionEn.Trim(),
            DiscountPercent = request.DiscountPercent,
            BadgeText = request.BadgeText.Trim(),
            Icon = request.Icon.Trim(),
            ValidUntil = request.ValidUntil,
            IsActive = request.IsActive
        };

        await offerRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<OfferDto>(entity);
    }
}

public sealed class UpdateOfferHandler(IRepository<Offer> offerRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateOfferCommand, OfferDto?>
{
    public async Task<OfferDto?> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = await offerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;

        entity.TitleAr = request.TitleAr.Trim();
        entity.TitleEn = request.TitleEn.Trim();
        entity.DescriptionAr = request.DescriptionAr.Trim();
        entity.DescriptionEn = request.DescriptionEn.Trim();
        entity.DiscountPercent = request.DiscountPercent;
        entity.BadgeText = request.BadgeText.Trim();
        entity.Icon = request.Icon.Trim();
        entity.ValidUntil = request.ValidUntil;
        entity.IsActive = request.IsActive;

        offerRepository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<OfferDto>(entity);
    }
}

public sealed class DeleteOfferHandler(IRepository<Offer> offerRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteOfferCommand, bool>
{
    public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = await offerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return false;
        offerRepository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class CreateTickerMessageHandler(IRepository<HomeTickerMessage> tickerRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateTickerMessageCommand, HomeTickerMessageDto>
{
    public async Task<HomeTickerMessageDto> Handle(CreateTickerMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new HomeTickerMessage
        {
            TextAr = request.TextAr.Trim(),
            TextEn = request.TextEn.Trim(),
            SortOrder = request.SortOrder,
            IsActive = request.IsActive
        };

        await tickerRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<HomeTickerMessageDto>(entity);
    }
}

public sealed class UpdateTickerMessageHandler(IRepository<HomeTickerMessage> tickerRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateTickerMessageCommand, HomeTickerMessageDto?>
{
    public async Task<HomeTickerMessageDto?> Handle(UpdateTickerMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await tickerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;

        entity.TextAr = request.TextAr.Trim();
        entity.TextEn = request.TextEn.Trim();
        entity.SortOrder = request.SortOrder;
        entity.IsActive = request.IsActive;

        tickerRepository.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<HomeTickerMessageDto>(entity);
    }
}

public sealed class DeleteTickerMessageHandler(IRepository<HomeTickerMessage> tickerRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTickerMessageCommand, bool>
{
    public async Task<bool> Handle(DeleteTickerMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await tickerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return false;
        tickerRepository.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class GetAllTickerMessagesHandler(IHomeTickerRepository homeTickerRepository, IMapper mapper)
    : IRequestHandler<GetAllTickerMessagesQuery, List<HomeTickerMessageDto>>
{
    public async Task<List<HomeTickerMessageDto>> Handle(GetAllTickerMessagesQuery request, CancellationToken cancellationToken)
    {
        var items = await homeTickerRepository.GetAllOrderedAsync(cancellationToken);
        return mapper.Map<List<HomeTickerMessageDto>>(items);
    }
}
