using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_Calculator
{
    public class SystemEvaluator : IEvaluator
    {
        private readonly DataTable _table = new();

        double IEvaluator.Evaluate(string expr) => Convert.ToDouble(_table.Compute(expr, null));
    }
}
