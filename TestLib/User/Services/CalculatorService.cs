namespace User.Services
{
    internal class CalculatorService
    {
        public double Sum(double parameter1, double parameter2)
        {
            return parameter1 + parameter2;
        }

        public double Difference(double parameter1, double parameter2)
        {
            return parameter1 - parameter2;
        }

        public double Multiplication(double parameter1, double parameter2)
        {
            return parameter1 * parameter2;
        }

        public double Division(double parameter1, double parameter2)
        {
            return parameter2 != 0d ? parameter1 / parameter2 : throw new DivideByZeroException("Can't divide by zero");
        }
    }
}
