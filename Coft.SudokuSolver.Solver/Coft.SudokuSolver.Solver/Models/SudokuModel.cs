using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.SudokuSolver.Solver.Models
{
    public class SudokuModel
    {
        public BoxModel[] Boxes { get; set; } = new BoxModel[9];

        public BoxModel BoxA
        {
            get { return Boxes[0]; }
            set { Boxes[0] = value; }
        }

        public BoxModel BoxB
        {
            get { return Boxes[1]; }
            set { Boxes[1] = value; }
        }

        public BoxModel BoxC
        {
            get { return Boxes[2]; }
            set { Boxes[2] = value; }
        }

        public BoxModel BoxD
        {
            get { return Boxes[3]; }
            set { Boxes[3] = value; }
        }

        public BoxModel BoxE
        {
            get { return Boxes[4]; }
            set { Boxes[4] = value; }
        }

        public BoxModel BoxF
        {
            get { return Boxes[5]; }
            set { Boxes[5] = value; }
        }

        public BoxModel BoxG
        {
            get { return Boxes[6]; }
            set { Boxes[6] = value; }
        }

        public BoxModel BoxH
        {
            get { return Boxes[7]; }
            set { Boxes[7] = value; }
        }

        public BoxModel BoxI
        {
            get { return Boxes[8]; }
            set { Boxes[8] = value; }
        }

        public SudokuModel()
        {
            for (int i = 0; i < 9; i++)
            {
                Boxes[i] = new BoxModel();
            }
        }

        public override string ToString()
        {
            return $"A:{BoxA.BoxId}\n{BoxA}\nB:{BoxB.BoxId}\n{BoxB}\nC:{BoxC.BoxId}\n{BoxC}\nD:{BoxD.BoxId}\n{BoxD}\nE:{BoxE.BoxId}\n{BoxE}\nF:{BoxF.BoxId}\n{BoxF}\nG:{BoxG.BoxId}\n{BoxG}\nH:{BoxH.BoxId}\n{BoxH}\nI:{BoxI.BoxId}\n{BoxI}\n";
        }
    }
}
