using FluentValidation;

namespace Inuveon.EventStore.Example.RegisterUser;

public class UserValidator : AbstractValidator<UserAggregate>
{
    public UserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
    }
}