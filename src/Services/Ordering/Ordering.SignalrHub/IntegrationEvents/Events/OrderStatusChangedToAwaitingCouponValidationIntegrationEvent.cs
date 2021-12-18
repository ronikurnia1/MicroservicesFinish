namespace Microsoft.eShopOnContainers.Services.Ordering.SignalrHub.IntegrationEvents;

public record OrderStatusChangedToAwaitingCouponValidationIntegrationEvent : IntegrationEvent
{
    public OrderStatusChangedToAwaitingCouponValidationIntegrationEvent(int orderId, string orderStatus, string buyerName, string code)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        BuyerName = buyerName;
        Code = code;
    }

    public int OrderId { get; private set; }

    public string OrderStatus { get; private set; }

    public string BuyerName { get; private set; }

    public string Code { get; private set; }
}