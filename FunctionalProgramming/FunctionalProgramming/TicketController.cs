using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    public class TheaterGateway
    {
        public Result Reserve(DateTime date, string customerName)
        {
            //reserve
            try
            {
                //call the client
                //if no exception
                return Result.Ok();
            }
            catch (HttpRequestException e)
            {
                return Result.Fail(ErrorType.UnableToConnnect);
            }
            catch (InvalidOperationException e)
            {
                return Result.Fail(ErrorType.TicketsAreNoLongerAvailable);
            }
        }
    }

    public class TicketRepository
    {
        public void Save(Ticket ticket)
        {
            //save ticket
        }

        public Maybe<Ticket> Get(int id)
        {
            return new Ticket(DateTime.Today, "foobar");
        }
    }

    public class Ticket
    {
        public readonly string CustomerName;
        public readonly DateTime Date;

        public Ticket(DateTime date, string customerName)
        {
            Date = date;
            CustomerName = customerName;
        }
    }

    public class TicketController
    {
        private readonly TicketRepository _repository;
        private readonly TheaterGateway _gateway;

        public TicketController(TicketRepository repository, TheaterGateway gateway)
        {
            _repository = repository;
            _gateway = gateway;
        }

        //normally this would be a actionresult
        public string BuyTicket(DateTime date, string customerName)
        {
            var validationResult = Validate(date, customerName);

            if (validationResult.IsFailure)
            {
                return "Validation failed";
            }

            var reserveResult = _gateway.Reserve(date, customerName);

            if (reserveResult.IsFailure)
            {
                return "Reservation failed";
            }

            var ticket = new Ticket(date, customerName);
            _repository.Save(ticket);
            return "Success";
        }

        public string GetTicket(int id)
        {
            var ticket = _repository.Get(id);

            if (ticket.HasNoValue)
                return "Ticket not found";

            return ticket.Value.CustomerName;
        }

        private Result Validate(DateTime date, string customerName)
        {
            if (date.Date < DateTime.Now.Date)
                return Result.Fail(ErrorType.CannotReservePastDate);
            if (string.IsNullOrWhiteSpace(customerName) || customerName.Length > 200)
                return Result.Fail(ErrorType.IncorrectCustomerName);

            return Result.Ok();
        }

    }

    [Serializable]
    internal class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
