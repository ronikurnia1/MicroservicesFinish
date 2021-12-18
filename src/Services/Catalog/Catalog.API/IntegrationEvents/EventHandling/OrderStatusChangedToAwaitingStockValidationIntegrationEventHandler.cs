namespace Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.EventHandling;
    
public class OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler :
    IIntegrationEventHandler<OrderStatusChangedToAwaitingStockValidationIntegrationEvent>
{
    private readonly CatalogContext _catalogContext;
    private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;
    private readonly ILogger<OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler> _logger;

    public OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler(
        CatalogContext catalogContext,
        ICatalogIntegrationEventService catalogIntegrationEventService,
        ILogger<OrderStatusChangedToAwaitingStockValidationIntegrationEventHandler> logger)
    {
        _catalogContext = catalogContext;
        _catalogIntegrationEventService = catalogIntegrationEventService;
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task Handle(OrderStatusChangedToAwaitingStockValidationIntegrationEvent @event)
    {
        using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            var confirmedOrderStockItems = new List<ConfirmedOrderStockItem>();

            foreach (var orderStockItem in @event.OrderStockItems)
            {
                var catalogItem = _catalogContext.CatalogItems.Find(orderStockItem.ProductId);
                var hasStock = catalogItem.AvailableStock >= orderStockItem.Units;
                var confirmedOrderStockItem = new ConfirmedOrderStockItem(catalogItem.Id, hasStock);

                confirmedOrderStockItems.Add(confirmedOrderStockItem);
            }

            var confirmedIntegrationEvent = confirmedOrderStockItems.Any(c => !c.HasStock)
                ? (IntegrationEvent)new OrderStockRejectedIntegrationEvent(@event.OrderId, confirmedOrderStockItems)
                : new OrderStockConfirmedIntegrationEvent(@event.OrderId);

            await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(confirmedIntegrationEvent);
            await _catalogIntegrationEventService.PublishThroughEventBusAsync(confirmedIntegrationEvent);

        }
    }
}
