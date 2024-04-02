using CalApplication.Model;

namespace CalApplication.Interface
{
    public interface ICalculator
    {
        string PerformOperation(ArithmeticOperation cal);
        IEnumerable<ArithmeticOperation> Get();
        void delete();

    }
}
