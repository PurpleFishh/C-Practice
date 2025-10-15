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
        private DataTable table;

        public SystemEvaluator()
        {
            table = new DataTable();
        }

        double IEvaluator.Evaluate(string expr) => Convert.ToDouble(table.Compute(expr, null));
    }
}
