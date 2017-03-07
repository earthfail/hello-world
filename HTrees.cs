using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace experiments
{
    public class Node<T>
    {
        public T Info { get; set; }
        public Node<T> Next { get; set; }
    }
    public class Stack<T>
    {
        public Node<T> First { get; set; }
        public T Top() { return this.First.Info; }
        public T Pop() { Node<T> tmp = this.First; this.First = this.First.Next; return tmp.Info; }
        public void Push(T info)
        {
            Node<T> tmp = new Node<T>();
            tmp.Info = info;
            tmp.Next = this.First;
            this.First = tmp;
        }
        public bool IsEmpty()
        {
            return this.First == null;
        }
    }
    public class List<T>
    {
        public Node<T> First { get; set; }

    }
    public class Tree<T>
    {
        public T Father { get; set; }
        public List<Tree<T>> Sons { get; set;}
        //some features
        enum code { PIPE, TAP, TIP };

        
        public static void prtbranch(Stack<int> s)
        {
            Stack<int> tmp = new Stack<int>();
            while (!s.IsEmpty())
                tmp.Push(s.Pop());//reverse the order of the code
            while (!tmp.IsEmpty())
            {
                switch (tmp.Top())
                {
                    case (int)code.PIPE: Console.Write("|"); break;
                    case (int)code.TAP: Console.Write("\t"); break;
                    case (int)code.TIP: Console.Write("\\______>"); break;
                }
                s.Push(tmp.Pop());
            }
        }
        void prttree(Tree<T> t, Stack<int> branch, int len)
        {
            if (len > 0 && t!=null)
            {
                Node<Tree<T>> p;
                prtbranch(branch);
                Console.WriteLine(t.Father.ToString());
                if (!branch.IsEmpty())
                {
                    branch.Pop();
                    branch.Push((int)code.TAP);
                }
                if (t.Sons == null)
                    p = null;
                else
                    p = t.Sons.First;
                while (p!=null)
                {
                    if (p.Next!=null)//not the last one in the group
                    {
                        branch.Push((int)code.PIPE);//there is another sub tree
                        branch.Push((int)code.TIP);//for the father node
                        prttree(p.Info, branch, len - 1);
                        //clear after it
                        branch.Pop();
                        branch.Pop();
                    }
                    else
                    {
                        branch.Push((int)code.TIP);
                        prttree(p.Info, branch, len - 1);
                        branch.Pop();
                    }
                    p = p.Next;
                }
            }
        }
        public  void prttree(int len)
        {
            Stack<int> branch = new Stack<int>();
            prttree(this, branch, len);

        }
        public delegate T ReadFather(string f);
        public void insertTree(Tree<T> t, int layer,ReadFather R)
        {
            
            Console.Write("insert value for father{0}:", layer);
            t.Father = R(Console.ReadLine());
            Console.Write("want sons for this father{0}(y/n)? ", layer);
            if (char.Parse(Console.ReadLine()).CompareTo('n') == 0)
                return; //no more insertion
            else
            {
                Node<Tree<T>> p;//to move through list and make sons
                char ans;//saves the answer from user
                t.Sons = new List<Tree<T>>();
                t.Sons.First = new Node<Tree<T>>();
                t.Sons.First.Info = new Tree<T>();
                p = t.Sons.First;
                insertTree(t.Sons.First.Info, layer + 1,R);
                do
                {
                    Console.Write("another son for father{0}(y/n)? ", layer);
                    ans = char.Parse(Console.ReadLine());
                    if (ans.CompareTo('y') == 0)
                    {
                        p.Next = new Node<Tree<T>>();
                        p.Next.Info = new Tree<T>();
                        insertTree(p.Next.Info, layer + 1,R);
                        p = p.Next;
                    }
                } while (ans.CompareTo('y') == 0);

            }
        }
    }
    class Program
    {
       static int intem(string s) { return int.Parse(s); }
        static void Main(string[] args)
        {
            Tree<int> a0 = new Tree<int>();
            Tree<int>.ReadFather func = new Tree<int>.ReadFather(intem);
            a0.insertTree(a0, 0,func);
            Console.WriteLine();
            a0.prttree(3);
            Console.ReadLine();
        }
    }
}
