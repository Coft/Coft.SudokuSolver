using Coft.SudokuSolver.Solver.Helpers;
using Coft.SudokuSolver.Solver.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.SudokuSolver.Solver.Models
{
    public class BoardService
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private BoxService boxService;

        private List<BoardModel> allBoards;

        public BoardService() : this(new BoxService())
        {

        }

        public BoardService(BoxService boxService)
        {
            this.boxService = boxService;
        }

        private void WalkAllE(BoardModel model)
        {
            for (int i = 0; i < Consts.WholeBoxesAmount; i++)
            {
                model.BoxIdE = i;
                WalkAllD(model);
            }
        }

        private void WalkAllD(BoardModel model)
        {
            List<int> rowsCompatibilityForE = boxService.GetRowCompatibleForBox(model.BoxIdE);

            for (int i = 0; i < rowsCompatibilityForE.Count; i++)
            {
                model.BoxIdD = rowsCompatibilityForE[i];
                WalkAllB(model);
            }
        }

        private void WalkAllB(BoardModel model)
        {
            List<int> columnsCompatibilityForE = boxService.GetColumnCompatibleForBox(model.BoxIdE);

            for (int i = 0; i < columnsCompatibilityForE.Count; i++)
            {
                model.BoxIdB = columnsCompatibilityForE[i];
                WalkAllF(model);
            }
        }

        private void WalkAllF(BoardModel model)
        {
            List<int> rowsCompatibilityForE = boxService.GetRowCompatibleForBox(model.BoxIdE);
            List<int> rowsCompatibilityForD = boxService.GetRowCompatibleForBox(model.BoxIdD);
            List<int> rowsCompatibilityForED = rowsCompatibilityForE.Intersect(rowsCompatibilityForD).ToList();

            for (int i = 0; i < rowsCompatibilityForED.Count; i++)
            {
                model.BoxIdF = rowsCompatibilityForED[i];
                WalkAllH(model);
            }
        }

        private void WalkAllH(BoardModel model)
        {
            List<int> columnsCompatibilityForB = boxService.GetColumnCompatibleForBox(model.BoxIdB);
            List<int> columnsCompatibilityForE = boxService.GetColumnCompatibleForBox(model.BoxIdE);
            List<int> columnsCompatibilityForEB = columnsCompatibilityForE.Intersect(columnsCompatibilityForB).ToList();

            for (int i = 0; i < columnsCompatibilityForEB.Count; i++)
            {
                model.BoxIdH = columnsCompatibilityForEB[i];
                WalkAllA(model);
            }
        }

        private void WalkAllA(BoardModel model)
        {
            List<int> rowsCompatibilityForB = boxService.GetRowCompatibleForBox(model.BoxIdB);
            List<int> columnsCompatibilityForD = boxService.GetColumnCompatibleForBox(model.BoxIdD);
            List<int> compatibilityForDB = columnsCompatibilityForD.Intersect(rowsCompatibilityForB).ToList();

            for (int i = 0; i < compatibilityForDB.Count; i++)
            {
                model.BoxIdA = compatibilityForDB[i];
                WalkAllI(model);
            }
        }

        private void WalkAllI(BoardModel model)
        {
            List<int> columnsCompatibilityForF = boxService.GetColumnCompatibleForBox(model.BoxIdF);
            List<int> rowsCompatibilityForH = boxService.GetRowCompatibleForBox(model.BoxIdH);
            List<int> compatibilityForFH = columnsCompatibilityForF.Intersect(rowsCompatibilityForH).ToList();

            for (int i = 0; i < compatibilityForFH.Count; i++)
            {
                model.BoxIdI = compatibilityForFH[i];
                WalkAllC(model);
            }
        }

        private void WalkAllC(BoardModel model)
        {
            List<int> rowsCompatibilityForA = boxService.GetRowCompatibleForBox(model.BoxIdA);
            List<int> rowsCompatibilityForB = boxService.GetRowCompatibleForBox(model.BoxIdB);
            List<int> compatibilityForAB = rowsCompatibilityForA.Intersect(rowsCompatibilityForB).ToList();

            List<int> columnsCompatibilityForF = boxService.GetColumnCompatibleForBox(model.BoxIdF);
            List<int> columnsCompatibilityForI = boxService.GetColumnCompatibleForBox(model.BoxIdI);
            List<int> compatibilityForFI = columnsCompatibilityForF.Intersect(columnsCompatibilityForI).ToList();

            List<int> compatibilityForABFI = compatibilityForAB.Intersect(compatibilityForFI).ToList();

            for (int i = 0; i < compatibilityForABFI.Count; i++)
            {
                model.BoxIdC = compatibilityForABFI[i];
                WalkAllG(model);
            }
        }

        private void WalkAllG(BoardModel model)
        {
            List<int> rowsCompatibilityForH = boxService.GetRowCompatibleForBox(model.BoxIdH);
            List<int> rowsCompatibilityForI = boxService.GetRowCompatibleForBox(model.BoxIdI);
            List<int> compatibilityForHI = rowsCompatibilityForH.Intersect(rowsCompatibilityForI).ToList();

            List<int> columnsCompatibilityForA = boxService.GetColumnCompatibleForBox(model.BoxIdA);
            List<int> columnsCompatibilityForD = boxService.GetColumnCompatibleForBox(model.BoxIdD);
            List<int> compatibilityForAD = columnsCompatibilityForA.Intersect(columnsCompatibilityForD).ToList();

            List<int> compatibilityForADHI = compatibilityForAD.Intersect(compatibilityForHI).ToList();

            if (compatibilityForADHI.Count > 1)
            {
                throw new Exception("More then 1 in last box");
            }
            if (compatibilityForADHI.Count == 1)
            {
                model.BoxIdG = compatibilityForADHI[0];
                allBoards.Add(new BoardModel(model));
            }
        }

        public void FindAllBoards()
        {
            allBoards = new List<BoardModel>();
            WalkAllE(new BoardModel());
        }
    }
}
