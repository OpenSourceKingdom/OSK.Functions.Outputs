namespace OSK.Functions.Outputs.Abstractions
{
    public interface IOutputValidator
    {
        void Validate(IOutput output);

        void Validate<TValue>(IOutput<TValue> output);
    }
}
