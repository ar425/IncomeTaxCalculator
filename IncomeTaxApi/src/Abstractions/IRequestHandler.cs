namespace IncomeTaxApi.Abstractions
{
    // This has been added to keep request handlers aligned within the project
    // This request handler interface can be expanded to contain multiple patterns
    public interface IRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}