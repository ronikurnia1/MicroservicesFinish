namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;

public class CouponConfirmedOrderStatusCommandHandler : IRequestHandler<CouponConfirmedOrderStatusCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CouponConfirmedOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(CouponConfirmedOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);

        if (orderToUpdate == null)
        {
            return false;
        }

        orderToUpdate.ProcessCouponConfirmed();

        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}

public class CouponConfirmIdenfifiedCommandHandler : IdentifiedCommandHandler<CouponConfirmedOrderStatusCommand, bool>
{
    public CouponConfirmIdenfifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CouponConfirmedOrderStatusCommand, bool>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true;                // Ignore duplicate requests for processing order.
    }
}

