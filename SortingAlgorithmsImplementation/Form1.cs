using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SortingAlgorithmsImplementation
{
    enum SortingAlgorithms {saInsertion = 0, saMerge, saHeap, saQuick, saCounting, saRadix, saBucket};



    public partial class FormMain : Form
    {
        private static readonly string[] SortAlgorithmsString = {
            "Insertion Sort",
            "Merge Sort",
            "Heap Sort",
            "Quick Sort",
            "Counting Sort",
            "Radix Sort",
            "Bucket Sort"
        };

        Random rand;
        ClassInsertionSort insertSort;
        ClassMergeSort mergeSort;
        ClassHeapSort heapSort;
        ClassQuickSort quickSort;
        ClassCountingSort countingSort;
        ClassRadixSort radixSort;
        ClassBucketSort bucketSort;
        ClassBinarySearchTree binarySearchTree;

        ClassStack stack;
        ClassQueue queue;
        LinkedList linkedlist;

        public FormMain()
        {
            InitializeComponent();
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Title = "Arrays Length";
            chart1.ChartAreas[0].AxisY.Title = "Running Rime (Ticks)";
            checkedListBoxSortAlgorithms.Items.Clear();
            for (int i = 0; i < SortAlgorithmsString.Length; i++)
            {
                chart1.Series.Add(SortAlgorithmsString[i]);
                chart1.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                checkedListBoxSortAlgorithms.Items.Add(SortAlgorithmsString[i]);
                checkedListBoxSortAlgorithms.SetItemChecked(i, true);

            }
            rand = new Random();
            insertSort = new ClassInsertionSort();
            mergeSort = new ClassMergeSort();
            heapSort = new ClassHeapSort();
            quickSort = new ClassQuickSort();
            countingSort = new ClassCountingSort();
            radixSort = new ClassRadixSort();
            bucketSort = new ClassBucketSort();

            linkedlist = new LinkedList();

            binarySearchTree = new ClassBinarySearchTree(listBoxBinarySearchTree);
            radioButtonSorted.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButtonPreorder.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButtonPostorder.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            radioButtonSorted.Checked = true;
            //listBoxBinarySearchTree.DataSource = new BindingSource();
            listBoxBinarySearchTree.DisplayMember = "Key";


            numericUpDownStart.Value = 200;
            numericUpDownStep.Value = 200;
            numericUpDownNumber.Value = 25;
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButtonSorted.Checked)
            {
                binarySearchTree.Order = BinarySearchTreeOrder.bstoSorted;
            }
            else if (radioButtonPreorder.Checked)
            {
                binarySearchTree.Order = BinarySearchTreeOrder.bstoPreordered;
            }
            else if (radioButtonPostorder.Checked)
            {
                binarySearchTree.Order = BinarySearchTreeOrder.bstoPostordered;
            }

            try
            {
                binarySearchTree.FillPrint();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        //[DllImport("C:\\Users\\ARA\\Documents\\Visual Studio 2015\\Projects\\SortingAlgorithmsImplementation\\Debug\\SortingLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        ////[DllImport("SortingLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int Add(int a, int b);

        private int[] GenerateRandomArray(int length)
        {
            int[] result = new int[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = rand.Next(10 * length);
            }

            return result;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SortAlgorithmsString.Length; i++)
            {
                chart1.Series[SortAlgorithmsString[i]].Points.Clear();
            }

            chart1.ChartAreas[0].AxisX.Minimum = 0;

            int arrayLength = (int)numericUpDownStart.Value;
            for (int i = 0; i < numericUpDownNumber.Value; i++)
            {
                if (i > 0)
                {
                    //arrayLength = (int)(arrayLength * numericUpDownStep.Value);
                    arrayLength = (int)(arrayLength + numericUpDownStep.Value);
                }

                chart1.ChartAreas[0].AxisX.Maximum = arrayLength;

                int[] array = GenerateRandomArray(arrayLength);

                foreach (SortingAlgorithms type in Enum.GetValues(typeof(SortingAlgorithms)))
                {
                    int[] newArray = new int[arrayLength];
                    Array.Copy(array, newArray, arrayLength);
                    double runningTime = 0;
                    switch (type)
                    {
                        case SortingAlgorithms.saInsertion:
                            {
                                runningTime = insertSort.Run(ref newArray);
                                break;
                            }
                        case SortingAlgorithms.saMerge:
                            {
                                runningTime = mergeSort.Run(ref newArray);
                                break;
                            }
                        case SortingAlgorithms.saHeap:
                            {
                                runningTime = heapSort.Run(ref newArray);
                                break;
                            }
                        case SortingAlgorithms.saQuick:
                            {
                                runningTime = quickSort.Run(ref newArray);
                                break;
                            }
                        case SortingAlgorithms.saCounting:
                            {
                                runningTime = countingSort.Run(ref newArray);
                                break;
                            }
                        case SortingAlgorithms.saRadix:
                            {
                                runningTime = radixSort.Run(ref newArray);
                                break;
                            }
                        case SortingAlgorithms.saBucket:
                            {
                                runningTime = bucketSort.Run(ref newArray);
                                break;
                            }
                    }
                    chart1.Series[SortAlgorithmsString[(int)type]].Points.AddXY(arrayLength, runningTime);
                }

            }
        }

        private void checkedListBoxSortAlgorithms_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            chart1.Series[SortAlgorithmsString[e.Index]].Enabled = (e.NewValue == CheckState.Checked);
            chart1.ChartAreas[0].RecalculateAxesScale();

        }

        private void buttonCreateStack_Click(object sender, EventArgs e)
        {
            stack = new ClassStack((int)numericUpDownStackLength.Value);
            UpdateStack();
        }

        private void buttonCreateQueue_Click(object sender, EventArgs e)
        {
            queue = new ClassQueue((int)numericUpDownQueueLength.Value);
            UpdateQueue();
        }

        private void buttonPush_Click(object sender, EventArgs e)
        {
            try
            {
                stack.PUSH((int)numericUpDownStack.Value);
                UpdateStack();
                numericUpDownStack.Value = numericUpDownStack.Value + 1;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void buttonPop_Click(object sender, EventArgs e)
        {
            try
            {
                stack.POP();
                UpdateStack();
                //numericUpDownStack.Value = numericUpDownStack.Value - 1;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void UpdateStack()
        {
            listBoxStack.Items.Clear();
            for (int i = 0; i < stack.Top; i++)
            {
                listBoxStack.Items.Add(stack.dynamicSet[i]);
            }
        }

        private void UpdateQueue()
        {
            listBoxQueue.Items.Clear();
            for (int i = 0; i < queue.dynamicSet.Length; i++)
            {
                listBoxQueue.Items.Add(0);
            }
            int counter = queue.Head;
            while (counter != queue.Tail)
            {
                if (counter == queue.dynamicSet.Length)
                {
                    counter = 0;
                }
                listBoxQueue.Items[counter] = queue.dynamicSet[counter];
                counter++;
            }
        }

        private void buttonEnqueue_Click(object sender, EventArgs e)
        {
            queue.ENQUEUE((int)numericUpDownQueue.Value);
            UpdateQueue();
            numericUpDownQueue.Value = numericUpDownQueue.Value + 1;
        }

        private void buttonDequeue_Click(object sender, EventArgs e)
        {
            queue.DEQUEUE();
            UpdateQueue();
            //numericUpDownQueue.Value = numericUpDownQueue.Value - 1;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            ListObject x = new ListObject((int)numericUpDownLinkedList.Value);
            //linkedlist.LIST_INSERT_(x);
            linkedlist.LIST_INSERT(x);
            numericUpDownLinkedList.Value = numericUpDownLinkedList.Value + 1;
            UpdateLinkedList();
        }

        private void UpdateLinkedList()
        {
            listBoxLinkedList.Items.Clear();
            comboBoxKeys.Items.Clear();
            //ListObject x = linkedlist.Nil.next;
            //while (x != linkedlist.Nil)
            //{
            //    listBoxLinkedList.Items.Add(x.key);
            //    comboBoxKeys.Items.Add(x.key);
            //    x = x.next;
            //}
            ListObject x = linkedlist.Head;
            while (x != null)
            {
                listBoxLinkedList.Items.Add(x.key);
                comboBoxKeys.Items.Add(x.key);
                x = x.next;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //linkedlist.LIST_DELETE_(linkedlist.LIST_SEARCH_((int)comboBoxKeys.SelectedItem));
            linkedlist.LIST_DELETE(linkedlist.LIST_SEARCH((int)comboBoxKeys.SelectedItem));
            UpdateLinkedList();
            if (comboBoxKeys.Items.Count > 0)
            {
                comboBoxKeys.SelectedItem = comboBoxKeys.Items[comboBoxKeys.Items.Count - 1];
            }
            
        }


        /////////// binary search tree functions
        private void buttonInsertBST_Click(object sender, EventArgs e)
        {
            try
            {
                TreeObject z = new TreeObject((int)numericUpDownBST.Value);
                binarySearchTree.TREE_INSERT(z);
                binarySearchTree.FillPrint();
                numericUpDownBST.Value = numericUpDownBST.Value + 1;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                TreeObject node = binarySearchTree.TREE_SEARCH(
                    binarySearchTree.root, (int)numericUpDownBST.Value);
                listBoxBinarySearchTree.SelectedItem = node;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void buttonMinimum_Click(object sender, EventArgs e)
        {
            try
            {
                TreeObject node = binarySearchTree.TREE_MINIMUM(
                    binarySearchTree.root);
                listBoxBinarySearchTree.SelectedItem = node;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void buttonMaximum_Click(object sender, EventArgs e)
        {
            try
            {
                TreeObject node = binarySearchTree.TREE_MAXIMUM(
                    binarySearchTree.root);
                listBoxBinarySearchTree.SelectedItem = node;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void buttonSuccessor_Click(object sender, EventArgs e)
        {
            try
            {
                TreeObject node = binarySearchTree.TREE_SUCCESSOR(
                    binarySearchTree.root);
                listBoxBinarySearchTree.SelectedItem = node;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void buttonDeleteBST_Click(object sender, EventArgs e)
        {
            try
            {
                binarySearchTree.TREE_DELETE((
                    TreeObject)listBoxBinarySearchTree.SelectedItem);
                binarySearchTree.FillPrint();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
