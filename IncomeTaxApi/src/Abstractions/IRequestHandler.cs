namespace IncomeTaxApi.Abstractions
{
    public interface IRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}