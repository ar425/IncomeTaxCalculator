namespace IncomeTaxApi.Abstractions
{
    // This has been added to keep converters aligned within the project
    // This converter interface can be expanded to contain multiple patterns
    public interface IConverter<in TIn, out TOut>
    {
        TOut Convert(TIn value);
    }
}