using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public enum ErrorType { Exception = 1, ControllerError = 2, SystemError = 3, UnknownError = 4 };

    public class ErrorRepo : BaseRepo<Error>
    {
        public static void LogException(Exception ex)
        {
            Error error = new Error()
            {
                ErrorDate = DateTime.Now,
                ErrorType = (int)ErrorType.Exception,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            };

            error.Save();
        }

        public static void LogControllerError(string message, string source)
        {
            Error error = new Error()
            {
                ErrorDate = DateTime.Now,
                ErrorType = (int)ErrorType.ControllerError,
                Message = message,
                StackTrace = source
            };

            error.Save();
        }

        public static void LogSystemError(string message, string source)
        {
            Error error = new Error()
            {
                ErrorDate = DateTime.Now,
                ErrorType = (int)ErrorType.SystemError,
                Message = message,
                StackTrace = source
            };

            error.Save();
        }

        public static void LogSystemError(Exception ex)
        {
            Error error = new Error()
            {
                ErrorDate = DateTime.Now,
                ErrorType = (int)ErrorType.SystemError,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            };

            error.Save();
        }

        public static void LogUnknownError(string message)
        {
            Error error = new Error()
            {
                ErrorDate = DateTime.Now,
                ErrorType = (int)ErrorType.SystemError,
                Message = message,
                StackTrace = ""
            };

            error.Save();
        }

    }
}
