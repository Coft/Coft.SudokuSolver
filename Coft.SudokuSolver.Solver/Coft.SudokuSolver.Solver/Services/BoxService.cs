using Coft.SudokuSolver.Solver.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coft.SudokuSolver.Solver.Services
{
    public class BoxService
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        private int[][] allBoxes = new int[362880][];
        private List<int>[,] shelfs = new List<int>[9, 9];

        private List<int>[] boxRowCompatibility = new List<int>[362880];
        private List<int>[] boxColumnCompatibility = new List<int>[362880];

        #region boxes

        public void GenerateBoxes()
        {
            DateTime startActionTime = DateTime.Now;

            int i = 0;
            for (int j = 9; j > 0; j--)
            {
                for (int k = 9; k > 0; k--)
                {
                    if (j == k)
                    {
                        continue;
                    }

                    for (int m = 9; m > 0; m--)
                    {
                        if (j == m || k == m)
                        {
                            continue;
                        }

                        for (int n = 9; n > 0; n--)
                        {
                            if (j == n || k == n || m == n)
                            {
                                continue;
                            }

                            for (int o = 9; o > 0; o--)
                            {
                                if (j == o || k == o || m == o || n == o)
                                {
                                    continue;
                                }

                                for (int p = 9; p > 0; p--)
                                {
                                    if (j == p || k == p || m == p || n == p || o == p)
                                    {
                                        continue;
                                    }

                                    for (int r = 9; r > 0; r--)
                                    {
                                        if (j == r || k == r || m == r || n == r || o == r || p == r)
                                        {
                                            continue;
                                        }

                                        for (int s = 9; s > 0; s--)
                                        {
                                            if (j == s || k == s || m == s || n == s || o == s || p == s || r == s)
                                            {
                                                continue;
                                            }

                                            for (int t = 9; t > 0; t--)
                                            {
                                                if (j == t || k == t || m == t || n == t || o == t || p == t || r == t || s == t)
                                                {
                                                    continue;
                                                }

                                                allBoxes[i++] = new int[9]
                                                {
                                                    j,k,m,n,o,p,r,s,t
                                                };
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            logger.Info($"Generate Boxes complete in: { DateTime.Now - startActionTime}");
        }

        public int[] GetBox(int boxId)
        {
            return allBoxes[boxId];
        }

        #endregion

        #region Shelfs

        public void FillShelfs()
        {
            DateTime startActionTime = DateTime.Now;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    shelfs[i, j] = FindShelf(i, j);
                }
            }

            logger.Info($"Filling shelfs complete in: { DateTime.Now - startActionTime}");
        }

        private List<int> FindShelf(int index, int number)
        {
            return allBoxes.Where(b => b[index] == number).Select((b, idx) => idx).ToList();
        }

        public List<int> GetShelf(int index, int number)
        {
            return shelfs[index, number] ?? (shelfs[index, number] = FindShelf(index, number));
        }

        #endregion

        #region compatibility

        public void FindBoxColumnCompatibility()
        {
            DateTime startActionTime = DateTime.Now;

            for (int i = 0; i < 362880; i++)
            {
                boxColumnCompatibility[i] = FindColumnCompatibilityFor(i);

                if (i % 10 == 0)
                {
                    Console.WriteLine($"{DateTime.Now}: Found columns for: {i}");
                }
            }

            logger.Info($"Find box column compatibility complete in: { DateTime.Now - startActionTime}");
        }

        private List<int> FindColumnCompatibilityFor(int boxId)
        {
            List<int> columnCompatibility = new List<int>();

            var iColumnA = new int[] { allBoxes[boxId][0], allBoxes[boxId][3], allBoxes[boxId][6] };
            var iColumnB = new int[] { allBoxes[boxId][1], allBoxes[boxId][4], allBoxes[boxId][7] };
            var iColumnC = new int[] { allBoxes[boxId][2], allBoxes[boxId][5], allBoxes[boxId][8] };

            for (int j = 0; j < 362880; j++)
            {
                var jColumnA = new int[] { allBoxes[j][0], allBoxes[j][3], allBoxes[j][6] };
                if (jColumnA.Except(iColumnA).Count() < 3)
                {
                    continue;
                }

                var jColumnB = new int[] { allBoxes[j][1], allBoxes[j][4], allBoxes[j][7] };
                if (jColumnB.Except(iColumnB).Count() < 3)
                {
                    continue;
                }

                var jColumnC = new int[] { allBoxes[j][2], allBoxes[j][5], allBoxes[j][8] };
                if (jColumnC.Except(iColumnC).Count() < 3)
                {
                    continue;
                }

                columnCompatibility.Add(j);
            }

            return columnCompatibility;
        }

        public void FindBoxRowCompatibility()
        {
            DateTime startActionTime = DateTime.Now;

            for (int i = 0; i < 362880; i++)
            {
                boxRowCompatibility[i] = FindRowCompatibilityForBox(i);

                if (i % 10 == 0)
                {
                    Console.WriteLine($"{DateTime.Now}: Found rows for: {i}");
                }
            }

            logger.Info($"Find box row compatibility complete in: { DateTime.Now - startActionTime}");
        }

        private List<int> FindRowCompatibilityForBox(int boxId)
        {
            List<int> rowCompatibility = new List<int>();

            var iRowA = new int[] { allBoxes[boxId][0], allBoxes[boxId][1], allBoxes[boxId][2] };
            var iRowB = new int[] { allBoxes[boxId][3], allBoxes[boxId][4], allBoxes[boxId][5] };
            var iRowC = new int[] { allBoxes[boxId][6], allBoxes[boxId][7], allBoxes[boxId][8] };

            for (int j = 0; j < 362880; j++)
            {
                var jRowA = new int[] { allBoxes[j][0], allBoxes[j][1], allBoxes[j][2] };
                if (jRowA.Except(iRowA).Count() < 3)
                {
                    continue;
                }

                var jRowB = new int[] { allBoxes[j][3], allBoxes[j][4], allBoxes[j][5] };
                if (jRowB.Except(iRowB).Count() < 3)
                {
                    continue;
                }

                var jRowC = new int[] { allBoxes[j][6], allBoxes[j][7], allBoxes[j][8] };
                if (jRowC.Except(iRowC).Count() < 3)
                {
                    continue;
                }

                rowCompatibility.Add(j);
            }

            return rowCompatibility;
        }

        public List<int> GetRowCompatibleForBox(int boxId)
        {
            return boxRowCompatibility[boxId] ?? (boxRowCompatibility[boxId] = FindRowCompatibilityForBox(boxId));
        }

        public List<int> GetColumnCompatibleForBox(int boxId)
        {
            return boxColumnCompatibility[boxId] ?? (boxColumnCompatibility[boxId] = FindColumnCompatibilityFor(boxId));
        }

        #endregion

    }
}
