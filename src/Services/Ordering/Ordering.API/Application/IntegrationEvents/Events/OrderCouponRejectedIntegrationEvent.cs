namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events;
public record OrderCouponRejectedIntegrationEvent : IntegrationEvent
{
    public OrderCouponRejectedIntegrationEvent(int orderId, string code)
    {
        OrderId = orderId;
        Code = code;
    }

    public int OrderId { get; private set; }

    public string Code { get; private set; }
}
