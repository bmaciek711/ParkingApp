namespace ParkingApp.Application.PatternsInterpreter;

public interface IExpression
{
    void Interpret(SearchContext context);
}
