using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingAlgorithmsImplementation
{
    public class ClassDynamicSets
    {
        internal int[] dynamicSet;

        public ClassDynamicSets(int n)
        {
            dynamicSet = new int[n];
        }
    }

    public class ClassStack: ClassDynamicSets
    {
        int top;

        public ClassStack(int n) : base(n)
        {
            top = 0;
        }

        public bool STACK_EMPTY()
        {
            if (top == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PUSH(int x)
        {
            if (top == dynamicSet.Length)
            {
                throw new Exception("Stack Overflow");
            }
            else
            {
                //top = top + 1;
                dynamicSet[top] = x;
                top = top + 1;
            }
        }

        public int POP()
        {
            if (STACK_EMPTY())
            {
                throw new Exception("Stack Underflow");
            }
            else
            {
                //top = top - 1;
                //return dynamicSet[top + 1];
                top = top - 1;
                return dynamicSet[top];
            }
        }

        public int Top
        {
            get { return top; }
        }

    }

    public class ClassQueue: ClassDynamicSets
    {
        int tail;
        int head;

        public ClassQueue(int n): base(n)
        {
            tail = 0;
            head = 0;
        }

        public void ENQUEUE(int x)
        {
            dynamicSet[tail] = x;
            if (tail == dynamicSet.Length - 1)
            {
                tail = 1;
            }
            else
            {
                tail = tail + 1;
            }
        }

        public int DEQUEUE()
        {
            int x = dynamicSet[head];
            if (head == dynamicSet.Length - 1)
            {
                //head = 1;
                head = 0;
            }
            else
            {
                head = head + 1;
            }
            return x;
        }

        public int Head
        {
            get { return head; }
        }

        public int Tail
        {
            get { return tail; }
        }
    }

    public class ListObject
    {
        public int? key;
        public ListObject prev;
        public ListObject next;

        public ListObject(int? k)
        {
            prev = null;
            next = null;
            key = k;
        }
    }

    public class LinkedList
    {
        private ListObject head;
        private ListObject nil;

        public ListObject Nil
        {
            get { return nil; }
        }

        public ListObject Head
        {
            get { return head; }
        }

        public LinkedList()
        {
            head = null;
            nil = new ListObject(null);
        }

        //
        public ListObject LIST_SEARCH(int k)
        {
            ListObject x = head;
            while ((x != null) & (x.key != k))
            {
                x = x.next;
            }
            return x;
        }

        public void LIST_INSERT(ListObject x)
        {
            x.next = head;
            if (head != null)
            {
                head.prev = x;
            }
            head = x;
            x.prev = null;
        }

        public void LIST_DELETE(ListObject x)
        {
            if (x.prev != null)
            {
                x.prev.next = x.next;
            }
            else
            {
                head = x.next;
            }
            if (x.next != null)
            {
                x.next.prev = x.prev;
            }
        }

        //sentinel included functions
        public ListObject LIST_SEARCH_(int k)
        {
            ListObject x = nil.next;
            while ((x != nil) & (x.key != k))
            {
                x = x.next;
            }
            return x;
        }

        public void LIST_INSERT_(ListObject x)
        {
            x.next = nil.next;
            nil.next.prev = x;
            nil.next = x;
            x.prev = nil;
        }

        public void LIST_DELETE_(ListObject x)
        {
            x.prev.next = x.next;
            x.next.prev = x.prev;
        }
    }

    public class TreeObject
    {
        public int key;
        public TreeObject parent;
        public TreeObject left;
        public TreeObject right;

        public TreeObject(int k)
        {
            key = k;
            parent = null;
            left = null;
            right = null;
        }

        public int Key
        {
            get { return key; }
        }
    }

    public class ClassTree
    {
        internal TreeObject root;

        public ClassTree()
        {
            root = null;
        }
    }

    public enum BinarySearchTreeOrder { bstoSorted = 0, bstoPreordered, bstoPostordered};

    public class ClassBinarySearchTree : ClassTree
    {
        //INORDER_TREE_WALK
        //public void 
        private ListBox printArea;
        private BinarySearchTreeOrder order;

        public ClassBinarySearchTree(ListBox aListBox)
        {
            printArea = aListBox;
        }

        public BinarySearchTreeOrder Order
        {
            get { return order; }
            set { order = value; }
        }

        public void FillPrint()
        {
            printArea.Items.Clear();
            INORDER_TREE_WALK(root);
        }

        public void Print(TreeObject aKey)
        {
            printArea.Items.Add(aKey);
        }

        public void INORDER_TREE_WALK(TreeObject x)
        {
            if (x != null)
            {
                if (order == BinarySearchTreeOrder.bstoPreordered)
                {
                    Print(x);
                }
                INORDER_TREE_WALK(x.left);
                if (order == BinarySearchTreeOrder.bstoSorted)
                {
                    Print(x);
                }
                INORDER_TREE_WALK(x.right);
                if (order == BinarySearchTreeOrder.bstoPostordered)
                {
                    Print(x);
                }
            }
        }

        public TreeObject TREE_SEARCH(TreeObject x, int k)
        {
            if ((x == null) || (k == x.key))
            {
                return x;
            }
            if (k < x.key)
            {
                return TREE_SEARCH(x.left, k);
            }
            else
            {
                return TREE_SEARCH(x.right, k);
            }
        }

        public TreeObject TREE_MINIMUM(TreeObject x)
        {
            TreeObject y = x;
            while (x.left != null)
            {
                y = x.left;
            }
            return y;
        }

        public TreeObject TREE_MAXIMUM(TreeObject x)
        {
            TreeObject y = x;
            while (y.right != null)
            {
                y = y.right;
            }
            return y;
        }

        public TreeObject TREE_SUCCESSOR(TreeObject x)
        {
            if (x.right != null)
            {
                return TREE_MINIMUM(x.right);
            }
            TreeObject y = x.parent;
            while ((y != null) && (x == y.right))
            {
                x = y;
                y = y.parent;
            }
            return y;
        }

        public void TREE_INSERT(TreeObject z)
        {
            TreeObject y = null;
            TreeObject x = root;
            while (x != null)
            {
                y = x;
                if (z.key < x.key)
                {
                    x = x.left;
                }
                else
                {
                    x = x.right;
                }
            }
            z.parent = y;
            if (y == null)
            {
                root = z;
            }
            else if (z.key < y.key)
            {
                y.left = z;
            }
            else
            {
                y.right = z;
            }
        }

        public void TRANSPLANT(TreeObject u, TreeObject v)
        {
            if (u.parent == null)
            {
                root = v;
            }
            else if (u == u.parent.left)
            {
                u.parent.left = v;
            }
            else
            {
                u.parent.right = v;
            }
            if (v != null)
            {
                v.parent = u.parent;
            }
        }

        public void TREE_DELETE(TreeObject z)
        {
            if (z.left == null)
            {
                TRANSPLANT(z, z.right);
            }
            else if (z.right == null)
            {
                TRANSPLANT(z, z.left);
            }
            else
            {
                TreeObject y = TREE_MINIMUM(z.right);
                if (y.parent != z)
                {
                    TRANSPLANT(y, y.right);
                    y.right = z.right;
                    y.right.parent = y;
                }
                TRANSPLANT(z, y);
                y.left = z.left;
                y.left.parent = y;
            }
        }
    }

    public class ClassRootedTree
    {

    }

}
