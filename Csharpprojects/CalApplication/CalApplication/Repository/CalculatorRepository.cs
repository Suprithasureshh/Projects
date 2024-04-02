using CalApplication.Data;
using CalApplication.Interface;
using CalApplication.Model;

namespace CalApplication.Repository
{
    public class CalculatorRepository : ICalculator
    {

        private readonly CalculatorContext _context;

        public CalculatorRepository(CalculatorContext context)
        {
            _context = context;
        }


        public string PerformOperation(ArithmeticOperation cal)
        {

            double Result =
             cal.Operator == "+" ? cal.value1 + cal.value2 :
             cal.Operator == "-" ? cal.value1 - cal.value2 :
             cal.Operator == "*" ? cal.value1 * cal.value2 :
             cal.Operator == "/" ? cal.value1 / cal.value2 : 0;
            cal.Result = Result;
            _context.Calculator.Add(cal);
            _context.SaveChanges();
            return Result.ToString();
        }
        public IEnumerable<ArithmeticOperation> Get()
        {
            return _context.Calculator;
        }
        public void delete()
        {
            var delete1 = _context.Calculator.ToList();
            _context.Calculator.RemoveRange(delete1);
            _context.SaveChanges();
        }
    }

}
