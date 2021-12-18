namespace Microsoft.eShopOnContainers.Services.Ordering.SignalrHub.AutofacModules;

public class ApplicationModule
    : Autofac.Module
{

    public string QueriesConnectionString { get; }

    public ApplicationModule()
    {
    }

    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterAssemblyTypes(typeof(OrderStatusChangedToAwaitingStockValidationIntegrationEvent).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

    }
}
