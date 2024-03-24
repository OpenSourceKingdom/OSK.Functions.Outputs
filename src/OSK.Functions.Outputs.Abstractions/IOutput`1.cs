namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutput<TValue>: IOutput
    {
        TValue Value { get; }
    }
}
