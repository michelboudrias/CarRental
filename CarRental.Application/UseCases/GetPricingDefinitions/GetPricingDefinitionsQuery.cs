using CarRental.Domain.Entities;
using MediatR;
using FluentResults;

namespace CarRental.Application.UseCases.GetPricingDefinitions;

public class GetPricingDefinitionsQuery : IRequest<Result<List<PricingDefinition>>>
{
}