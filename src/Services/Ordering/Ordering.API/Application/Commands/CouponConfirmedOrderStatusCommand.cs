namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;

public class CouponConfirmedOrderStatusCommand : IRequest<bool>
{

    [DataMember]
    public int OrderNumber { get; private set; }

    [DataMember]
    public int Discount { get; private set; }

    public CouponConfirmedOrderStatusCommand(int orderNumber, int discount)
    {
        OrderNumber = orderNumber;
        Discount = discount;
    }
}
