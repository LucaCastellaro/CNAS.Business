using CNAS.Business.Test.Handlers.Queries;
using FluentValidation;

namespace CNAS.Business.Test.Validators.Queries;

internal sealed class GetDataValidator : AbstractValidator<GetDataQuery>
{
    public GetDataValidator()
    {
        RuleFor(xx => xx.Value)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(1)
            ;
    }
}