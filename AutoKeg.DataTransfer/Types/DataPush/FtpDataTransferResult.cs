using AutoKeg.DataTransfer.Interfaces;

namespace AutoKeg.DataTransfer.Types.DataPush
{
    internal class FtpDataTransferResult : IApiResult
    {
        private string Result { get; }

        public FtpDataTransferResult(string result)
        {
            Result = result;
        }

        public bool IsSuccessful()
        {
            return true;
        }
    }
}