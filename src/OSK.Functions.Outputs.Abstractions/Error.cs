namespace OSK.Functions.Outputs.Abstractions
{
    public readonly struct Error(string message)
    {
        #region Variables

        public string Message => message;

        #endregion
    }
}
