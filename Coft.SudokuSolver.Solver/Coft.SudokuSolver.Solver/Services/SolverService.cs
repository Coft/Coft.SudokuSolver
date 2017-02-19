using Coft.SudokuSolver.Solver.Helpers;
using Coft.SudokuSolver.Solver.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.SudokuSolver.Solver.Services
{

    public class SolverService
    {
        private Logger logger = LogManager.GetCurrentClassLogger(); 
        private BoxService boxService;

        public SolverService() : this(new BoxService())
        {
            
        }

        public SolverService(BoxService boxService)
        {
            this.boxService = boxService;
        }

        private bool SolveE(SudokuModel model)
        {
            var candidates = GetPossibleBoxes(model.BoxE);

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxE.BoxId = candidates[i];
                if (SolveD(model))
                {
                    return true;
                }
                if (i % 100 == 0)
                {
                    Console.WriteLine($"{DateTime.Now} {i}/{candidates.Count}");
                }
            }

            return false;
        }

        private bool SolveD(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxD);
            var rowsCompatibilityForE = boxService.GetRowCompatibleForBox(model.BoxE.BoxId);
            var candidates = possibleBoxes.Intersect(rowsCompatibilityForE).ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxD.BoxId = candidates[i];
                if (SolveB(model))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SolveB(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxB);
            var columnsCompatibilityForE = boxService.GetColumnCompatibleForBox(model.BoxE.BoxId);
            var candidates = possibleBoxes.Intersect(columnsCompatibilityForE).ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxB.BoxId = candidates[i];
                if (SolveF(model))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SolveF(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxF);
            var rowsCompatibilityForE = boxService.GetRowCompatibleForBox(model.BoxE.BoxId);
            var rowsCompatibilityForD = boxService.GetRowCompatibleForBox(model.BoxD.BoxId);
            var rowsCompatibilityForED = rowsCompatibilityForE.Intersect(rowsCompatibilityForD).ToList();
            var candidates = possibleBoxes.Intersect(rowsCompatibilityForED).ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxF.BoxId = candidates[i];
                if (SolveH(model))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SolveH(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxH);
            var columnsCompatibilityForB = boxService.GetColumnCompatibleForBox(model.BoxB.BoxId);
            var columnsCompatibilityForE = boxService.GetColumnCompatibleForBox(model.BoxE.BoxId);
            var columnsCompatibilityForEB = columnsCompatibilityForE.Intersect(columnsCompatibilityForB).ToList();
            var candidates = possibleBoxes.Intersect(columnsCompatibilityForEB).ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxH.BoxId = candidates[i];
                if (SolveA(model))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SolveA(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxA);
            List<int> rowsCompatibilityForB = boxService.GetRowCompatibleForBox(model.BoxB.BoxId);
            List<int> columnsCompatibilityForD = boxService.GetColumnCompatibleForBox(model.BoxD.BoxId);
            List<int> compatibilityForDB = columnsCompatibilityForD.Intersect(rowsCompatibilityForB).ToList();
            var candidates = possibleBoxes.Intersect(compatibilityForDB).ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxA.BoxId = candidates[i];
                if (SolveI(model))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SolveI(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxI);
            List<int> columnsCompatibilityForF = boxService.GetColumnCompatibleForBox(model.BoxF.BoxId);
            List<int> rowsCompatibilityForH = boxService.GetRowCompatibleForBox(model.BoxH.BoxId);
            List<int> compatibilityForFH = columnsCompatibilityForF.Intersect(rowsCompatibilityForH).ToList();
            var candidates = possibleBoxes.Intersect(compatibilityForFH).ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxI.BoxId = candidates[i];
                if (SolveC(model))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SolveC(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxC);
            List<int> rowsCompatibilityForA = boxService.GetRowCompatibleForBox(model.BoxA.BoxId);
            List<int> rowsCompatibilityForB = boxService.GetRowCompatibleForBox(model.BoxB.BoxId);
            List<int> compatibilityForAB = rowsCompatibilityForA.Intersect(rowsCompatibilityForB).ToList();

            List<int> columnsCompatibilityForF = boxService.GetColumnCompatibleForBox(model.BoxF.BoxId);
            List<int> columnsCompatibilityForI = boxService.GetColumnCompatibleForBox(model.BoxI.BoxId);
            List<int> compatibilityForFI = columnsCompatibilityForF.Intersect(columnsCompatibilityForI).ToList();

            List<int> compatibilityForABFI = compatibilityForAB.Intersect(compatibilityForFI).ToList();
            var candidates = possibleBoxes.Intersect(compatibilityForABFI).ToList();

            for (int i = 0; i < candidates.Count; i++)
            {
                model.BoxC.BoxId = candidates[i];
                if (SolveG(model))
                {
                    return true;
                }
            }

            return false;
        }

        private bool SolveG(SudokuModel model)
        {
            var possibleBoxes = GetPossibleBoxes(model.BoxG);

            List<int> rowsCompatibilityForH = boxService.GetRowCompatibleForBox(model.BoxH.BoxId);
            List<int> rowsCompatibilityForI = boxService.GetRowCompatibleForBox(model.BoxI.BoxId);
            List<int> compatibilityForHI = rowsCompatibilityForH.Intersect(rowsCompatibilityForI).ToList();

            List<int> columnsCompatibilityForA = boxService.GetColumnCompatibleForBox(model.BoxA.BoxId);
            List<int> columnsCompatibilityForD = boxService.GetColumnCompatibleForBox(model.BoxD.BoxId);
            List<int> compatibilityForAD = columnsCompatibilityForA.Intersect(columnsCompatibilityForD).ToList();

            List<int> compatibilityForADHI = compatibilityForAD.Intersect(compatibilityForHI).ToList();
            var candidates = possibleBoxes.Intersect(compatibilityForADHI).ToList();

            if (candidates.Count > 1)
            {
                throw new Exception("More then 1 in last box");
            }
            if (candidates.Count == 1)
            {
                model.BoxG.BoxId = candidates[0];
                return true;
            }

            return false;
        }

        private List<int> GetPossibleBoxes(BoxModel model)
        {
            List<int> boxes = null;

            for (int i = 0; i < 9; i++)
            {
                if (model.Numbers[i].HasValue) {
                    var shelfContent = boxService.GetShelf(i, model.Numbers[i].Value);

                    if (boxes == null)
                    {
                        boxes = shelfContent;
                    }
                    else
                    {
                        boxes = boxes.Intersect(shelfContent).ToList();
                    }
                }
            }

            return boxes;
        } 

        private void UpdateBoxesNumbers(SudokuModel model)
        {
            foreach (BoxModel box in model.Boxes)
            {
                int[] boxNumbers = boxService.GetBox(box.BoxId);
                for (int i = 0; i < 9; i++)
                {
                    box.Numbers[i] = boxNumbers[i];
                }
            }
        }

        public SudokuModel Solve(SudokuModel model)
        {
            boxService.GenerateBoxes();

            DateTime startActionTime = DateTime.Now;

            if (SolveE(model)) {

                UpdateBoxesNumbers(model);

                logger.Info($"Find sudoku board complete in: { DateTime.Now - startActionTime}");

            }
            else
            {
                logger.Info($"Model not found, time spend: { DateTime.Now - startActionTime}");
            }

            return model;
        }
    }
}
