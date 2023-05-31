using System;

namespace MyStore.Server
{
    internal static class ResultFactory
    {
        public static IResult InternalError(String message)
        {
            return new Result()
            {
                Status = EResultStatus.Failed,
                Message = message ?? throw new ArgumentException(nameof(message)),
            };
        }

        public static IResult InternalError()
        {
            return InternalError("Internal server error");
        }

        public static IResult Success(String message) 
        {
            return new Result()
            {
                Status = EResultStatus.Success,
                Message = message ?? throw new ArgumentException(nameof(message)),
            };
        }

        public static IResult Success()
        {
            return Success("OK");
        }
    }
}
