namespace Practice_Store.Domain.Entities.Orders
{
    public enum OrderState
    {
        Processing = 0,
        AdminCanceled = -1,
        UserCanceled = -2,
        Posted = 1,
        Delivered = 2,
    }
}
