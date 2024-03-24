namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutput
    {
        bool IsSuccessful => Code.IsSuccessCode;

        OutputStatusCode Code { get; }

        ErrorInformation? ErrorInformation { get; }

        IOutput<TValue> AsType<TValue>();
    }
}
