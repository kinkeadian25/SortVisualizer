using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class BubbleSort : ISortEngine
    {
        private int[] _theArray;
        private Graphics G;
        private int _maxVal;
        Brush WhiteBrush = new SolidBrush(Color.White);
        Brush BlackBrush = new SolidBrush(Color.Black);

        public BubbleSort(int[] theArray, Graphics g, int maxVal)
        {
            _theArray = theArray;
            G = g;
            _maxVal = maxVal;
        }

        public void NextStep()
        {
            for (int i = 0; i < _theArray.Count() - 1; i++)
            {
                if (_theArray[i] > _theArray[i + 1])
                {
                    Swap(i, i + 1);
                }
            }
        }

        private void Swap(int i, int p)
        {
            int temp = _theArray[i];
            _theArray[i] = _theArray[i + 1];
            _theArray[i + 1] = temp;

            DrawBar(i, _theArray[i]);
            DrawBar(p, _theArray[p]);
        }
        private void DrawBar(int position, int height)
        {
            G.FillRectangle(BlackBrush, position, 0, 1, _maxVal);
            G.FillRectangle(WhiteBrush, position, _maxVal - _theArray[position], 1, _maxVal);
        }
        public bool IsSorted()
        {
            for(int i = 0; i < _theArray.Count() - 1; i++)
            {
                if (_theArray[i] > _theArray[i + 1]) return false;
            }
            return true;
        }
        public void ReDraw()
        {
            for (int i = 0; i < _theArray.Count() - 1; i++)
            {
                G.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, _maxVal - _theArray[i], 1, _maxVal);
            }
        }
    }
}
