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
