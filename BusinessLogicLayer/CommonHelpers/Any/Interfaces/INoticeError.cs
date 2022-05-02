namespace CommonHelpers.Any.Interfaces
{
    public interface INoticeError
    {
        void Throw();

        void Throw(string textSuffix);
    }
}
