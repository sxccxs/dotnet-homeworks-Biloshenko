using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Core.Exceptions;

namespace TestLib.Core
{
    public class Assert
    {
        public void AreEqual<T>(T parameter1, T parameter2)
        {
            if (!parameter1.Equals(parameter2))
            {
                throw new AssertionException($"Parameter1 is not equal to parameter2. Parameter1 = {parameter1}, parameter2 = {parameter2}");
            }
        }

        public void ThrowsException<T>(Action action)
            where T : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (T)
            {
                return;
            }
            catch (Exception ex)
            {
                throw new AssertionException($"{typeof(T).Name} should have been thrown, but {ex.GetType().Name} was throw intead.");
            }

            throw new AssertionException($"{typeof(T).Name} should have been thrown, but no exception was thrown");
        }

        public void NotThrowsExceptions(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                throw new AssertionException($"Nothing should have been thrown, but {ex.GetType().Name} was thrown");
            }
        }
    }
}
