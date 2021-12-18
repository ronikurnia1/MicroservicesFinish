namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events;
public record OrderCouponConfirmedIntegrationEvent : IntegrationEvent
{
    public OrderCouponConfirmedIntegrationEvent(int orderId, int discount)
    {
        OrderId = orderId;
        Discount = discount;
    }

    public int OrderId { get; private set; }

    public int Discount { get; private set; }
}
