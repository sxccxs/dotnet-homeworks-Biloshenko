using Core.DataClasses;

namespace BLL.Abstractions.Interfaces
{
    public interface IParserService
    {
        OptionalResult<string> Parse(string input);
    }
}
