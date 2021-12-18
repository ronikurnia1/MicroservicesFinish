using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.API.IntegrationEvents.Events
{
    public record OrderStatusChangedToCancelledIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedToCancelledIntegrationEvent(int orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }

        public int OrderId { get; private set; }

        public string OrderStatus { get; private set; }
        
        public string BuyerName { get; private set; }
        
        public string DiscountCode { get; private set; }
    }
}
