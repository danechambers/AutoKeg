using AutoKeg.DataTransfer.Interfaces;

namespace AutoKeg.DataTransfer.Types.DataPush
{
    internal class FtpDataTransferResult : IApiResult
    {
        private bool Result { get; }

        public FtpDataTransferResult(bool result)
        {
            Result = result;
        }

        public bool IsSuccessful() => Result;
    }
}