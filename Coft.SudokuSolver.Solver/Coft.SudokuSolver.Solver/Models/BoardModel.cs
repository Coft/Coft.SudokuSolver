using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.SudokuSolver.Solver.Models
{
    public class BoardModel
    {
        private int boxIdA;

        public int BoxIdA
        {
            get { return boxIdA; }
            set { boxIdA = value; }
        }

        private int boxIdB;

        public int BoxIdB
        {
            get { return boxIdB; }
            set { boxIdB = value; }
        }

        private int boxIdC;

        public int BoxIdC
        {
            get { return boxIdC; }
            set { boxIdC = value; }
        }

        private int boxIdD;

        public int BoxIdD
        {
            get { return boxIdD; }
            set { boxIdD = value; }
        }

        private int boxIdE;

        public int BoxIdE
        {
            get { return boxIdE; }
            set { boxIdE = value; }
        }

        private int boxIdF;

        public int BoxIdF
        {
            get { return boxIdF; }
            set { boxIdF = value; }
        }

        private int boxIdG;

        public int BoxIdG
        {
            get { return boxIdG; }
            set { boxIdG = value; }
        }

        private int boxIdH;

        public int BoxIdH
        {
            get { return boxIdB; }
            set { boxIdB = value; }
        }

        private int boxIdI;

        public int BoxIdI
        {
            get { return boxIdI; }
            set { boxIdI = value; }
        }

        public BoardModel()
        {

        }

        public BoardModel(BoardModel growModel)
        {
            boxIdA = growModel.BoxIdA;
            boxIdB = growModel.BoxIdB;
            boxIdC = growModel.BoxIdC;

            boxIdD = growModel.BoxIdD;
            boxIdE = growModel.BoxIdE;
            boxIdF = growModel.BoxIdF;

            boxIdG = growModel.BoxIdG;
            boxIdH = growModel.BoxIdH;
            boxIdI = growModel.BoxIdI;
        }

        public override string ToString()
        {
            return $"A:{BoxIdA} B:{BoxIdB} C:{boxIdC}\nD:{BoxIdD} E:{BoxIdE} F:{boxIdF}\nG:{BoxIdG} H:{BoxIdH} I:{boxIdI}\n";
        }
    }
}
