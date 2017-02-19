using Coft.SudokuSolver.Solver.Models;
using Coft.SudokuSolver.Solver.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.SudokuSolver.CommandLineClient
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
         
        static void Main(string[] args)
        {
            SolverService solverService = new SolverService();

            var sudokuToSolve = new SudokuModel();

            sudokuToSolve.BoxA.Numbers[0] = 1;

            sudokuToSolve.BoxB.Numbers[1] = 7;
            sudokuToSolve.BoxB.Numbers[4] = 8;
            sudokuToSolve.BoxB.Numbers[5] = 4;
            sudokuToSolve.BoxB.Numbers[6] = 1;

            sudokuToSolve.BoxC.Numbers[5] = 1;

            sudokuToSolve.BoxD.Numbers[2] = 2;
            sudokuToSolve.BoxD.Numbers[6] = 8;

            sudokuToSolve.BoxE.Numbers[0] = 8;
            sudokuToSolve.BoxE.Numbers[8] = 7;

            sudokuToSolve.BoxF.Numbers[2] = 1;
            sudokuToSolve.BoxF.Numbers[6] = 4;

            sudokuToSolve.BoxG.Numbers[3] = 4;

            sudokuToSolve.BoxH.Numbers[2] = 2;
            sudokuToSolve.BoxH.Numbers[3] = 5;
            sudokuToSolve.BoxH.Numbers[4] = 6;
            sudokuToSolve.BoxH.Numbers[7] = 3;

            sudokuToSolve.BoxI.Numbers[8] = 5;

            var solvedSudoku = solverService.Solve(sudokuToSolve);

            logger.Info(solvedSudoku);
        }
    }
}
