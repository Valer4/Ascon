using CommonHelpers.Any.Interfaces;
using System;

namespace CommonHelpers.Any
{
    public class NoticeError : INoticeError
    {
        private const string prefixErrorMsg = "Обратитесь к разработчикам программы.";

        public void Throw() => throw new Exception(prefixErrorMsg);

        public void Throw(string textSuffix) =>
            throw new Exception($"{prefixErrorMsg}{( ! string.IsNullOrEmpty(textSuffix) ? " " + textSuffix : string.Empty)}");
    }
}
