using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public class QuickSort : ISortEngine
    {
        private int[] _theArray;
        private Graphics G;
        private int _maxVal;
        Brush WhiteBrush = new SolidBrush(Color.White);
        Brush BlackBrush = new SolidBrush(Color.Black);

        public QuickSort(int[] theArray, Graphics g, int maxVal)
        {
            _theArray = theArray;
            G = g;
            _maxVal = maxVal;
        }

        public void NextStep()
        {
            int low = 0;
            int high = _theArray.Count() - 1;

            quickSort(_theArray, low, high);
        }
        private void quickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {

                // pi is partitioning index, arr[p]
                // is now at right place
                int pi = partition(arr, low, high);

                // Separately sort elements before
                // partition and after partition
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }

        private void Swap(int[] _theArray, int i, int j)
        {
            int temp = _theArray[i];
            _theArray[i] = _theArray[j];
            _theArray[j] = temp;

            DrawBar(i, _theArray[i]);
            DrawBar(j, _theArray[j]);
        }
        private int partition(int[] arr, int low, int high)
        {

            int pivot = arr[high];

            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {

                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return (i + 1);
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
