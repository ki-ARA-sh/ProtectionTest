using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SortingAlgorithmsImplementation
{


    public abstract class ClassSortingAlgorithms
    {

        public abstract void Sort(ref int[] array);

        public static bool IsSorted(int[] array)
        {
            bool result = true;
            for (int i = 0; i < (array.Length - 1); i++)
            {
                result = result && (array[i] <= array[i + 1]);
            }
            return result;
        }

        public double Run(ref int[] array)
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Sort(ref array);

            watch.Stop();

            if (!IsSorted(array))
            {
                throw new Exception("Failed to sort");
            }


            //long x = watch.ElapsedMilliseconds;
            long y = watch.ElapsedTicks;
            //TimeSpan z = watch.Elapsed;
            return y;
        }
    }

    public class ClassInsertionSort: ClassSortingAlgorithms
    {

        public override void Sort(ref int[] array)
        {
            for (int j = 0; j < array.Length; j++)
            {
                int key = array[j];
                int i = j - 1;
                while ((i >= 0) && (array[i] > key))
                {
                    array[i + 1] = array[i];
                    i = i - 1;
                }
                array[i + 1] = key;
            }
        }
    }

    public class ClassMergeSort: ClassSortingAlgorithms
    {
        public override void Sort(ref int[] array)
        {
            MergeSort(ref array, 0, array.Length - 1);
        }

        private void Merge(ref int[] array, int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] L = new int[n1 + 1];
            int[] R = new int[n2 + 1];
            int i;
            int j;
            for (i = 0; i < n1; i++)
            {
                //L[i] = array[p + i - 1];
                L[i] = array[p + i];
            }
            for (j = 0; j < n2; j++)
            {
                //R[j] = array[q + j];
                R[j] = array[q + j + 1];
            }
            L[n1] = int.MaxValue;
            R[n2] = int.MaxValue;
            i = 0;
            j = 0;
            for (int k = p; k <= r; k++)
            {
                if (L[i] <= R[j])
                {
                    array[k] = L[i];
                    i++;
                }
                else
                {
                    array[k] = R[j];
                    j++;
                }
            }
        }

        private void MergeSort(ref int[] array, int p, int r)
        {
            if (p < r)
            {
                int q = (int) Math.Floor(0.5 * (p + r));
                MergeSort(ref array, p, q);
                MergeSort(ref array, q + 1, r);
                Merge(ref array, p, q, r);
            }
        }
    }

    public class ClassHeapSort: ClassSortingAlgorithms
    {
        private int heapSize;

        private int Parent(int i)
        {
            return (int)Math.Floor(0.5 * (i + 1)) - 1;
        }

        private int Left(int i)
        {
            return 2 * (i + 1) - 1;
        }

        private int Right(int i )
        {
            return 2 * (i + 1);
        }

        private void MaxHeapify(ref int[] array, int i)
        {
            int l = Left(i);
            int r = Right(i);
            int largest;
            if ((l <= (heapSize - 1)) && (array[l] > array[i]))
            {
                largest = l;
            }
            else
            {
                largest = i;
            }
            if ((r <= (heapSize- 1)) && (array[r] > array[largest]))
            {
                largest = r;
            }
            if (largest != i)
            {
                int med = array[i];
                array[i] = array[largest];
                array[largest] = med;
                MaxHeapify(ref array, largest);
            }
        }

        private void BuildMaxHeap(ref int[] array)
        {
            heapSize = array.Length;
            for (int i = Parent(array.Length - 1); i >= 0; i--)
            {
                MaxHeapify(ref array, i);
            }
        }

        private void HeapSort(ref int[] array)
        {
            BuildMaxHeap(ref array);
            for (int i = array.Length - 1; i > 0; i--)
            {
                int med = array[0];
                array[0] = array[i];
                array[i] = med;
                heapSize = heapSize - 1;
                MaxHeapify(ref array, 0);
            }
        }

        public override void Sort(ref int[] array)
        {
            HeapSort(ref array);
        }
    }

    public class ClassQuickSort: ClassSortingAlgorithms
    {
        Random rand;
        public ClassQuickSort()
        {
            rand = new Random();
        }
        private int Partition(ref int[] array, int p, int r)
        {
            int x = array[r];
            int i = p - 1;
            int med;
            for (int j = p; j <= r - 1; j++)
            {
                if (array[j] <= x)
                {
                    i++;
                    med = array[i];
                    array[i] = array[j];
                    array[j] = med;
                }
            }
            med = array[i + 1];
            array[i + 1] = array[r];
            array[r] = med;
            return i + 1;
        }

        private int RandomizedPartition(ref int[] array, int p, int r)
        {
            int i = rand.Next(p, r);
            int med = array[r];
            array[r] = array[i];
            array[i] = med;
            return Partition(ref array, p, r);
        }

        private void QuickSort(ref int[] array, int p, int r)
        {
            if (p < r)
            {
                int q = RandomizedPartition(ref array, p, r);
                QuickSort(ref array, p, q - 1);
                QuickSort(ref array, q + 1, r);
            }
        }

        public override void Sort(ref int[] array)
        {
            QuickSort(ref array, 0, array.Length - 1);
        }
    }

    public class ClassCountingSort: ClassSortingAlgorithms
    {

        private void CountingSort(int[] A, ref int[] B, int k)
        {
            int[] C = new int[k + 1];
            for (int i = 0; i <= k; i++)
            {
                C[i] = 0;
            }
            for (int i = 0; i < A.Length; i++)
            {
                C[A[i]] = C[A[i]] + 1;
            }
            for (int i = 1; i <= k; i++)
            {
                C[i] = C[i] + C[i - 1];
            }
            for (int i = A.Length - 1; i >= 0; i--)
            {
                B[C[A[i]] - 1] = A[i];
                C[A[i]] = C[A[i]] - 1;
            }
        }

        public override void Sort(ref int[] array)
        {
            int k = 0;
            for (int i = 0; i < array.Length; i++)
            {
                k = Math.Max(k, array[i]);
            }
            int[] B = new int[array.Length];
            CountingSort(array, ref B, k);
            array = B;
        }
    }

    public class ClassRadixSort: ClassSortingAlgorithms
    {
        private int GetSpecifiedDigit(int n, int digit)
        {
            //double result = 0;
            //result = n / Math.Pow(10.0, digit) - Math.Floor(n / Math.Pow(10.0, digit));
            //result = Math.Round(10.0 * result);
            //int num = n;
            //while (num >= Math.Pow(10, digit))
            //{
            //    num /= 10;
            //}
            //return num % 10;
            return (n / (int)Math.Pow(10, digit - 1)) % 10;
        }

        private void StableSort(int[] A, ref int[] B, int digit)
        {
            int[] C = new int[10];
            for (int i = 0; i < C.Length; i++)
            {
                C[i] = 0;
            }
            for (int i = 0; i < A.Length; i++)
            {
                int n = GetSpecifiedDigit(A[i], digit);
                //C[A[i]] = C[A[i]] + 1;
                C[n] = C[n] + 1;
            }
            for (int i = 1; i < C.Length; i++)
            {
                C[i] = C[i] + C[i - 1];
            }
            for (int i = A.Length - 1; i >= 0; i--)
            {
                int n = GetSpecifiedDigit(A[i], digit);
                //B[C[A[i]] - 1] = A[i];
                //C[A[i]] = C[A[i]] - 1;
                B[C[n] - 1] = A[i];
                C[n] = C[n] - 1;
            }
        }

        public override void Sort(ref int[] array)
        {
            int k = 0;
            for (int i = 0; i < array.Length; i++)
            {
                k = Math.Max(k, array[i]);
            }

            int d = (int)Math.Floor(Math.Log10(k) + 1);

            for (int i = 1; i <= d; i++)
            {
                int[] B = new int[array.Length];
                StableSort(array, ref B, i);
                array = B;
            }
        }
    }

    public class ClassBucketSort: ClassSortingAlgorithms
    {
        public override void Sort(ref int[] array)
        {
            int k = 0;
            for (int i = 0; i < array.Length; i++)
            {
                k = Math.Max(k, array[i]);
            }

            int n = array.Length;
            List<int>[] B = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                B[i] = new List<int>();
            }

            for (int i = 0; i < n; i++)
            {
                B[(int)Math.Floor(1.0 * n * array[i] / (k + 1))].Add(array[i]);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < B[i].Count; j++)
                {
                    int key = B[i][j];
                    int l = j - 1;
                    while ((l >= 0) && (B[i][l] > key))
                    {
                        B[i][l + 1] = B[i][l];
                        l = l - 1;
                    }
                    B[i][l + 1] = key;
                }
            }

            int counter = 0;
            for (int i = 0; i < B.Length; i++)
            {
                for (int j = 0; j < B[i].Count; j++)
                {
                    array[counter] = B[i][j];
                    counter++;
                }
            }

        }
    }
}
