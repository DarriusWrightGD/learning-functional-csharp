using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Method signature honesty
Doesn't affect or refer to the global state


    Why functional programming?
    - complexity
    
    Applying functional programming principles help reduce code complexity and thus help prevent creating issues in the code base.

    Programming with mathmatical functions help us understand the system easier.
    Very composible. They are easy to reason about. In addition easy to unit tests.

    Vocabulary 

    Immutability - inability to change data 
    State - data that changes over time
    Side effect - a change that is made to some state

    Why does immutability matter?

    Mutable operations = Dishonest code

    When creating methods you should list all the possibile outcomes

    When not immutable then you have to look into the method to see what possible there are.

    Structures that change over time tend to become more error prone, and also run into issues when it comes to 
    multi threading.

    Immutability Limitations 

    - CPU Usage
    - Memory Usage

    Command Query separation is a way that we can deal with side effects in a clear way.

    Exceptions hide the possiblities of a method
    Using them to hand control flow 
    Prefer using return values over exceptions

    Exceptions are for exceptional situtations, never use thing in what you expect to happen

    How to handle exceptions?
    Fail fast - stop the current operation if anything wrong happens

    Shortening the feedback look, confidence in the working software, protects the persistence state.

    The most useful times to take advantage of exceptions.

    At the top most level of the application, to log the exception, and shut the operation down. There should be no domain logic here.
    The next is 3rd party libraries. In these cases can retry or say just try again. Catch these at the lowest level. Only catch the exceptions that you know 
    how to handle, and nothing else.
    Narrow the exceptions down the exception as far as possible.

    Primitive obsession 
    makes the code dishonest, and encourages code duplication
    Create a separate class for each concept in your domain model.
    Dont create classes for simple concepts, and always use value objects inside your domain model, convert them into primitives only once they have level the domain.

    Avoiding nulls with the Maybe type
    The inventor calls it his billion dollar mistake.
    It is dishonest
*/


namespace FunctionalProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
