using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    public class Database
    {
        public Maybe<Customer> Get(int customerId)
        {
            return new Customer("",new Address(""));
        }

        public Result Save(Customer customer)
        {
            return Result.Ok();
        }
    }
    public class CustomerBalanceService
    {
        private Database _database;
        private Logger _logger;
        private PaymentGateway _paymentGateway;

        public CustomerBalanceService(Database database, PaymentGateway paymentGateway)
        {
            _database = database;
            _paymentGateway = paymentGateway;
        }

        public string RefillBalance(int customerId, decimal moneyAmount)
        {
            //if(!IsMoneyAmountValid(moneyAmount))
            //{
            //    _logger.Log("Money amount is invalid");
            //    return "Money amount is invalid";
            //}

            //Customer customer = _database.Get(customerId);

            //if(customer == null)
            //{
            //    _logger.Log("Customer is not found");

            //    return "Customer is not found";
            //}

            //customer.Balance += moneyAmount;
            //try
            //{
            //    _paymentGateway.ChargePayment(customer.BillingInfo, moneyAmount);
            //}
            //catch(ChargePaymentException e)
            //{
            //    _logger.Log("Unable to charge the credit card");
            //    return "Unable to connect to the database";

            //}

            //try
            //{
            //    _database.Save(customer);
            //}
            //catch (SqlException e)
            //{
            //    _paymentGateway.RollbackLastTransaction();
            //    _logger.Log("Unable to charge the credit card");
            //    return "Unable to connect to the database";

            //}

            //_logger.Log("Ok");
            //return "OK";

            var moneyToCharge = MoneyToCharge.Create(moneyAmount);
            var customer = _database.Get(customerId).ToResult("Customer is not found");
            return Result.Combine(moneyToCharge, customer)
                .OnSuccess(() => customer.Value.AddBalance(moneyToCharge.Value))
                .OnSuccess(() => _paymentGateway.ChargePayment(customer.Value.BillingInfo, moneyToCharge.Value))
                .OnSuccess(() => _database.Save(customer.Value).OnFailure(() => _paymentGateway.RollbackLastTransaction()))
                .OnBoth(result => Log(result))
                .OnBoth(result => result.IsSuccess ? "Ok" : result.ErrorMessage);

        }

        private void Log(Result result)
        {
            if(result.IsFailure)
            {
                _logger.Log(result.ErrorMessage);
            }
            else
            {
                _logger.Log("Ok");
            }
        }
    }

    public class MoneyToCharge
    {
        private decimal _amount;

        private MoneyToCharge(decimal amount)
        {
            _amount = amount;
        }

        public static Result<MoneyToCharge> Create(decimal amount)
        {
            if (!IsMoneyAmountValid(amount))
                return Result.Fail<MoneyToCharge>(ErrorType.InvalidMoneyAmount);

            return Result.Ok(new MoneyToCharge(amount));
        }

        private static bool IsMoneyAmountValid(decimal moneyAmount)
        {
            return moneyAmount > 0 && moneyAmount < 1000;
        }

        public static explicit operator MoneyToCharge(decimal moneyToCharge) => Create(moneyToCharge).Value;

        public static implicit operator decimal (MoneyToCharge moneyToCharge) => moneyToCharge._amount;
    }

    [Serializable]
    public class ChargePaymentException : Exception
    {
        public ChargePaymentException()
        {
        }

        public ChargePaymentException(string message) : base(message)
        {
        }

        public ChargePaymentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChargePaymentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class Logger
    {
        internal void Log(string v)
        {
        }
    }

    public class PaymentGateway
    {
        //public void ChargePayment(BillingInfo billingInfo, decimal moneyAmount)
        //{

        //}

        public Result ChargePayment(BillingInfo billingInfo, decimal moneyAmount)
        {

            return Result.Ok();
        }


        public void RollbackLastTransaction()
        {
        }
    }

    public class BillingInfo
    {
    }
}
