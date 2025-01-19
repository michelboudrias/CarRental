using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using FluentResults;
using MediatR;

namespace CarRental.Application.UseCases.GetPricingDefinitions;

public class
    GetPricingDefinitionsQueryHandler : IRequestHandler<GetPricingDefinitionsQuery, Result<List<PricingDefinition>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPricingDefinitionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<PricingDefinition>>> Handle(GetPricingDefinitionsQuery request,
        CancellationToken cancellationToken)
    {
        var pricingDefinitionsResult = await _unitOfWork.PricingDefinitionRepository.GetAsync();
        if (pricingDefinitionsResult.IsFailed)
        {
            return Result.Fail<List<PricingDefinition>>(pricingDefinitionsResult.Errors);
        }

        return Result.Ok(pricingDefinitionsResult.Value);
    }
}