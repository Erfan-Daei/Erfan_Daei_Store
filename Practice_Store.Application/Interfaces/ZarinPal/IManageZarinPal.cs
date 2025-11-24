namespace Practice_Store.Application.Interfaces.ZarinPal
{
    public interface IManageZarinPal
    {
        Task<ResultRequestToZarinPalDto> RequestToZarinPal(RequestToZarinPalDto Request);
        Task<ValidateRequestFromZarinPalDto> ValidateRequestFromZarinPal(ResultValidateRequestFromZarinPalDto Request);
    }
}
