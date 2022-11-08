using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public class MoveToBack : ISortEngine
    {
        private int[] _theArray;
        private Graphics G;
        private int _maxVal;
        Brush WhiteBrush = new SolidBrush(Color.White);
        Brush BlackBrush = new SolidBrush(Color.Black);

        private int CurrentListPointer = 0;

        public MoveToBack(int[] theArray, Graphics g, int maxVal)
        {
            _theArray = theArray;
            G = g;
            _maxVal = maxVal;
        }

        public void NextStep()
        {
            if (CurrentListPointer >= _theArray.Count() - 1) CurrentListPointer = 0;
            if (_theArray[CurrentListPointer] > _theArray[CurrentListPointer + 1])
            {
                Rotate(CurrentListPointer);
            }
            CurrentListPointer++;
        }

        private void Rotate(int currentListPointer)
        {
            int temp = _theArray[CurrentListPointer];
            int endpoint = _theArray.Count() - 1;

            for(int i = currentListPointer; i < endpoint; i++)
            {
                _theArray[i] = _theArray[i + 1];
                DrawBar(i, _theArray[i]);
            }

            _theArray[endpoint] = temp;
            DrawBar(endpoint, _theArray[endpoint]);
        }

        private void DrawBar(int position, int height)
        {
            G.FillRectangle(BlackBrush, position, 0, 1, _maxVal);
            G.FillRectangle(WhiteBrush, position, _maxVal - _theArray[position], 1, _maxVal);
        }
        public bool IsSorted()
        {
            for (int i = 0; i < _theArray.Count() - 1; i++)
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
