using FluentValidation;
using Inuveon.EventStore.Abstractions.Entities;
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Example.AnotherFeature;

public class AnotherAggregate : AggregateRoot<AnotherAggregateValidator>
{
    public override void Handle(ICommand command)
    {
        throw new NotImplementedException();
    }

    protected override void Apply(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}

public class AnotherAggregateValidator : AbstractValidator<AnotherAggregate>
{
}