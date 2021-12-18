namespace Microsoft.eShopOnContainers.Services.Ordering.SignalrHub.IntegrationEvents;

public record OrderStatusChangedToAwaitingStockValidationIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public string OrderStatus { get; }
    public string BuyerName { get; }

    public OrderStatusChangedToAwaitingStockValidationIntegrationEvent(int orderId, string orderStatus, string buyerName)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        BuyerName = buyerName;
    }
}

