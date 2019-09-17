using vITGrid.Common.Log.Types;

namespace vITGrid.Log.Tests.Models
{
    public class Error
    {
        public Error()
        {

        }

        public Error(ErrorType errorType, string fileName, string errorMessage, string errorLabel)
        {
            ErrorType = errorType;
            FileName = fileName;
            ErrorMessage = errorMessage;
            ErrorLabel = errorLabel;
        }

        public ErrorType ErrorType { get; set; }
        public string FileName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorLabel { get; set; }
    }
}