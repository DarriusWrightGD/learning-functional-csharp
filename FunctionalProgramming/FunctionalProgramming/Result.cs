using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    public class Result
    {
        public bool IsSuccess { get; }
        public ErrorType? Error { get;  }
        public string ErrorMessage { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, ErrorType? error)
        {
            if (isSuccess && error != null) throw new InvalidOperationException();
            if (!isSuccess && error == null) throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        protected Result(bool isSuccess, string errorMessage)
        {
            if (isSuccess && errorMessage != null) throw new InvalidOperationException();
            if (!isSuccess && errorMessage == null) throw new InvalidOperationException();

            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Fail(ErrorType errorType)
        {
            return new Result(false, errorType);
        }

        public static Result<T> Fail<T>(ErrorType errorType)
        {
            return new Result<T>(default(T), false, errorType);
        }

        public static Result Fail(string errorMessage)
        {
            return new Result(false, errorMessage);
        }

        public static Result<T> Fail<T>(string errorMessage)
        {
            return new Result<T>(default(T), false, errorMessage);
        }

        public static Result Ok()
        {
            return new Result(true, (string)null);
        }


        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value,true, (string)null);
        }

        public static Result Combine(params Result [] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return result;
            }

            return Ok();
        }

    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!IsSuccess) throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal Result(T value, bool isSuccess, ErrorType? error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        protected internal Result(T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            _value = value;
        }
    }

    public enum ErrorType
    {
        DatabaseOffline,
        CustomerAlreadyExists,
        CannotReservePastDate,
        IncorrectCustomerName,
        UnableToConnnect,
        TicketsAreNoLongerAvailable,
        InvalidMoneyAmount
    }
}
