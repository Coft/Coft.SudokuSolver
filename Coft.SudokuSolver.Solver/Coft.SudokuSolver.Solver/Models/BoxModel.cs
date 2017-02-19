using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.SudokuSolver.Solver.Models
{
    public class BoxModel
    {
        public int BoxId { get; set; }
        public int?[] Numbers { get; set; } = new int?[9];

        public override string ToString()
        {
            return $"{Numbers[0]}{Numbers[1]}{Numbers[2]}\n{Numbers[3]}{Numbers[4]}{Numbers[5]}\n{Numbers[6]}{Numbers[7]}{Numbers[8]}";
        }
    }
}
