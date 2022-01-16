using TestLib.Core;
using TestLib.Core.Attributes;

namespace User.Services
{
    [TestClass]
    public class CalculatorServiceTests
    {
        public void BeforeEach()
        {
            Console.WriteLine("before each");
        }

        public void AfterAll()
        {
            Console.WriteLine("after group");
        }

        [TestMethod]
        public void Sum_Number1_Number2_SumOfNumber()
        {
            double number1 = 5, number2 = 10, correctResult = 15;
            var calculator = new CalculatorService();

            var result = calculator.Sum(number1, number2);

            new Assert().AreEqual(result, correctResult);
        }

        [TestMethod]
        public void Division_Number1_Zero_ThrowDivideByZeroException()
        {
            double number1 = 5, number2 = 0;
            var calculator = new CalculatorService();

            Action act = () => calculator.Division(number1, number2);

            new Assert().ThrowsException<DivideByZeroException>(act);
        }
    }
}
