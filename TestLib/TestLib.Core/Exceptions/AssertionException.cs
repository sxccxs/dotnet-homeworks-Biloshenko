using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Core.Exceptions
{
    public class AssertionException : Exception
    {
        public AssertionException()
        {
        }

        public AssertionException(string message)
            : base(message)
        {
        }
    }
}
