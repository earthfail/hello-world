using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//*windows C:\Users\user\Desktop\prog2\stdline\stdline
//this addition has been edited in ubuntu 25/1/2017
namespace OOP 
{
   class Node<T>
   {
       public T Info { get; set; }
       public Node<T> Next { get; set; }
       public Node() { }
       public Node(T info)
       {
           this.Info = info;
       }
   }
   class Stack<T>
   {
       Node<T> first;
       public Stack() { this.first = null; }
       public T Top() { return first.Info; }
       public T Pop()
       {
           T p = first.Info;
           first = first.Next;
           return p;
       }
       public void Push(T t)
       {
           Node<T> p = new Node<T>(t);
           p.Next = first;
           first = p;
       }
       public override string ToString()
       {
           string str = "Top -->";
           Node<T> tmp = first;
           while (tmp != null)
           {
               str += tmp.Info;
               tmp = tmp.Next;
               if (tmp != null) str += " ,";
           }
           str += "<--bottom";
           return str;
       }
   }
   class Queue<T>
   {
       Node<T> first;
       Node<T> last;
       public Queue() { this.first = null; this.last = null; }
       public void Insert(T t)
       {
           if (this.first == null)
           {
               this.last = new Node<T>(t);
               this.first = last;
           }
           else
           {
               Node<T> tmp = new Node<T>(t);
               last.Next = tmp;
               last = tmp;
           }
       }
       public T Head()
       {
           return this.first.Info;
       }
       public T Remove()
       {
           T t = first.Info;
           first = first.Next;
           if (first == null) last = null;
           return t;
       }
       public bool IsEmpty()
       {
           return first == null;
       }
       public override string ToString()
       {
           string str = "first-->";
           Node<T> tmp = first;
           while (tmp != null)
           {
               str += tmp.Info;
               tmp = tmp.Next;
               if (tmp != null) str += ",";
           }
           str += "<--last";
           return str;
       }
   }
   class List<T>
   {
       Node<T> first;
       public List() { first = null; }
       public Node<T> Insert(Node<T> p, T t) //بعرفش اذا بترجع اشي او لا فخليتها ترجع الحلقة
       {
           Node<T> tmp = new Node<T>(t);
           if (p == null)
           {
               tmp.Next = first;
               first = tmp;
		
           }
           else
           {
               tmp.Next = p.Next;
               p.Next = tmp;
           } return tmp;
       }
       public void Remove(Node<T> p)
       {
           if (p == null)
           {
               first = first.Next;
           }
           else
           {
               p.Next = p.Next.Next;
           }
       }
       public T this[int i]
       {    
           get 
           {
               int tmp = 0;
               Node<T> p = this.first;
               while (p != null && tmp < i) p = p.Next;
               if (p != null) return p.Info;
               return p.Info;
           }
           set 
           {
               int tmp = 0;
               Node<T> p = this.first;
               while (p != null && tmp < i) p = p.Next;
		if(p!=null) p.Info = value;
           }
       }
       public override string ToString()
       {
           string str = "the list is\n";
           int i = 1;
           Node<T> tmp = first;
           while (tmp != null)
           {
               str += "\n" + i + ")" + tmp.Info;
               tmp = tmp.Next;
           }
           return str;
       }
   }
   class BinNode<T>
   {
       public Node<T> Value { get; set; }//سميها شو بدك
       public Node<T> Left { get; set; }
       public Node<T> Right { get; set; }
       public BinNode() { Value = null; Left = null; Right = null; }
       public bool IsLeaf() { return (Left == null) && (Right == null); }
       public bool HasLeft() { return !(Left == null); }
       public bool HasRight() { return !(Right == null); }
   }
}
namespace StdLine
{
   //when making the group that will make the field make sure to override ToString
   //when you need to make and operation define an instance for the Field
   public abstract class Field<T>
   {
       abstract public T zero { get; }//zero is neutral in adding
       abstract public T one { get; } //one is neutral in multipication
       public abstract T add(T a, T b);//a+b
       public abstract T mult(T a, T b);//a*b
       public abstract void equal(ref T output, T input);//output=input;
       public abstract T neg(T a);//-a
       public abstract T inv(T a);//1/a
       public abstract bool isequal(T a, T b);// does a==b
   }
   public class Matrix<T, U> where T : Field<U>, new()
	{
	    public static T t = new T();//operator for all the class
	    public int row;
	    public int col;
	    private bool setted = false;/*to secure that there will be
					 * one matrix */
	    private int maxsp = 0;
	    private U[,] matrix;
	    
	    public Matrix() { }//constructor
	    public Matrix(int row, int col) //constructor initilize all the matrix slots to zero
	    {
		if (row > 0 && col > 0)
		{
		    setted = true;
		    this.row = row;
		    this.col = col;
		    matrix = new U[row, col];
		    
		    for (int i = 0; i < row; i++)
			for (int j = 0; j < col; j++)
			    t.equal(ref matrix[i, j], t.zero);
		}
	    }//create the zero matrix
	    
	    public void Set_RC(int row, int col) //semi constructor :)
	    {
		if (!setted && row > 0 && col > 0)
		{
		    setted = true;
		    this.row = row;
		    this.col = col;
		    matrix = new U[row, col];
		    
		    for (int i = 0; i < row; i++)
			for (int j = 0; j < col; j++)
			    t.equal(ref matrix[i, j], t.zero);
		}
	    }//create the zero matrix
	    public U value(int row, int col) //matrix[row,col]
	    {
		if (row >= 0 && col >= 0 && row < this.row && col < this.col)//in the boundry of the matrix
		{ return matrix[row, col]; }
		else { return default(U); }
	    }//return value in the [row,col]
	    
	    public void set(int row, int col, U v)
	    {
		if (col < this.col && row < this.row && row >= 0 && col >= 0 && matrix != null)//check if the input is valid and if matrix has already been constructed
		{ t.equal(ref matrix[row, col], v); }
	    }//input (position,value) output(put u in position [row,col])
	    public void set(U[] elem)
	    {
		if (setted)
		    for (int i = 0; i < elem.Length; i++)
			t.equal(ref matrix[i / col, i % col], elem[i]);
		else Console.WriteLine("MATRIX HAD NOT BEEN DECLARED YET");
	    }
	    
	    private void maxspace()//find the longest elemint in the matrix
	    {
		if (setted)
		{
		    int max = matrix[0, 0].ToString().Length;
		    foreach (U a in matrix)
			if (a.ToString().Length > max) max = a.ToString().Length;
		    maxsp = max;
		}
	    }
	    public void print()
	    { //need work EDIT:fixed
		if (setted)
		{
		    int i;
		    int j;
		    maxspace();
		    for (i = 0; i < row; i++)
		    {
			for (j = 0; j <= col * (maxsp + 1); j++)
			{
			    switch (j % (maxsp + 1))
			    {
				case 0: Console.Write("+"); break;
				default: Console.Write("-"); break;
			    }
			}
			Console.WriteLine();
			for (j = 0; j < col; j++)
			    Console.Write(String.Format("|{0," + maxsp + "}", matrix[i, j].ToString().PadRight((maxsp / 2) + (matrix[i, j].ToString().Length / 2))));
			Console.WriteLine("|");
		    }
		    for (j = 0; j <= col * (maxsp + 1); j++)
		    {
			switch (j % (maxsp + 1))
			{
			    case 0: Console.Write("+"); break;
			    default: Console.Write("-"); break;
			}
		    }
		    Console.WriteLine();
		}
		else Console.WriteLine("MATRIX HAD NOT BEEN DECLARED YET");
	    }//print the matrix but not very cool
	}
   
   public static class Oper<T, U> where T : Field<U>, new()
	{
	    public static T t = new T(); //operator for all the class
	    public static Matrix<T, U> Add(Matrix<T, U> m1, Matrix<T, U> m2) //add m1 and m2 note:this operation is assotiative
	    {
		if (m1.row != m2.row || m1.col != m2.col) { return null; }
		Matrix<T, U> output = new Matrix<T,U>(m1.row, m1.col);
		
		for (int i = 0; i < m2.row; i++)
		{
		    for (int j = 0; j < m2.col; j++)
		    {
			output.set(i, j, t.add(m1.value(i, j), m2.value(i, j)));
		    }
		}
		return output;
	    }
	    public static Matrix<T, U> Mult(Matrix<T, U> m1, Matrix<T, U> m2)
	    {
		if (m1.col != m2.row) { return null; }
		Matrix<T, U> output = new Matrix<T, U>(m1.row, m2.col);
		
		for (int i = 0; i < m1.row; i++)
		{
		    for (int j = 0; j < m2.col; j++)
		    {
			for (int h = 0; h < m1.col; h++)
			{
			    output.set(i, j, t.add(output.value(i, j), t.mult(m1.value(i, h), m2.value(h, j))));
			}
		    }
		}
		return output;
	    }//multiply m1 with m2 note: this operation is not associative
	    public static Matrix<T, U> Mult(U q, Matrix<T, U> m1)
	    {
		
		Matrix<T, U> output = new Matrix<T, U>(m1.row, m1.col);
		for (int i = 0; i < m1.row; i++)
		    for (int j = 0; j < m1.col; j++)
			output.set(i, j, t.mult(m1.value(i, j), q));
		return output;
	    }//scalar multipication
	    
	    public static void Copy(Matrix<T, U> output, Matrix<T, U> input)
	    {
		if (output.col == input.col && output.row == input.row)
		{
		    for (int i = 0; i < input.row; i++)
			for (int j = 0; j < input.col; j++)
			    output.set(i, j, input.value(i, j));
		}
	    }  //copy the values of m2 to m1
	    
	    public static Matrix<T, U> swap(Matrix<T, U> m, int r1, int r2)
	    {
		
		U temp = t.zero;
		for (int i = 0; i < m.col; i++)
		{
		    t.equal(ref temp, m.value(r1, i));
		    m.set(r1, i, m.value(r2, i));
		    m.set(r2, i, temp);
		}
		return m;
	    } //r1<--->r2
	    public static Matrix<T, U> mult(Matrix<T, U> m, int r, U alpha)
	    {
		
		for (int i = 0; i < m.col; i++)
		{
		    m.set(r, i, t.mult(alpha, m.value(r, i)));
		}
		return m;
	    }// r --> alpha*r
	    public static Matrix<T, U> add(Matrix<T, U> m, int r1, int r2)
	    {
		
		for (int i = 0; i < m.col; i++)
		    m.set(r1, i, t.add(m.value(r1, i), m.value(r2, i)));
		return m;
	    } //r1 --> r1+r2
	    public static Matrix<T, U> add(Matrix<T, U> m, int r1, int r2, U alpha)
	    {
		
		for (int i = 0; i < m.col; i++)
		    m.set(r1, i, t.add(m.value(r1, i), t.mult(alpha, m.value(r2, i))));
		return m;
	    } // r1 --> r1+alpha*r2
	    
	    public static int Rank(Matrix<T, U> m)
	    {
		Matrix<T, U> tmp = new Matrix<T, U>(m.row, m.col);
		Oper<T, U>.Copy(tmp, m);
		return reduced1(tmp);
	    }
	    public static int reduced1(Matrix<T, U> m1)
	    {
		T t=new T();
		int rank=0,row=0,col=0;
		int i;//for moving through the column
		for(;col<m1.col && row<m1.row;col++)
		{
		    for(i=row;i<m1.row;i++)
		    {
			if(!t.isequal(m1.value(i,col),t.zero))//non-zero
			    break;
		    }
		    if(i<m1.row)//found a non-zero element in col
		    {
			rank++;//found another row
			Oper<T,U>.swap(m1,i,row);//move the non-zero to the [row,col]
			Oper<T,U>.mult(m1,row,t.inv(m1.value(row,col)));
			for(i=0;i<m1.row;i++)
			{
			    if(i==row) continue;//ignore the row we are in
			    Oper<T,U>.add(m1,i,row,t.neg(m.value(i,col)));//make it zero
			}
			row++;//result for increasing the rank
		    }
		}//reached the end;
		return rank;
	    }
	    
	    public static void reduced2(Matrix<T, U> m1, Matrix<T, U> m2) //here we assume that rank(m1)=m1.row=m1.col and m2=I(m1 in inversable and m2 is I)
	    {  //every operation must be on m2 before m1 so we reduce m1 and make the same for m2
		//output-> m1=I and m2=m1^-1
		T t=new T();
		int row=0,col=0;
		int i;//for moving through the column
		for(;col<m1.col && row<m1.row;col++)
		{
		    for(i=row;i<m1.row;i++)
		    {
			if(!t.isequal(m1.value(i,col),t.zero))//non-zero
			    break;
		    }
		    if(i<m1.row)//found a non-zero element in col
		    {
			Oper<T,U>.swap(m2,i,row);
			Oper<T,U>.swap(m1,i,row);//move the non-zero to the [row,col]

			Oper<T,U>.mult(m2,row,t.inv(m1.value(row,col)));
			Oper<T,U>.mult(m1,row,t.inv(m1.value(row,col)));
			for(i=0;i<m1.row;i++)
			{
			    if(i==row) continue;//ignore the row we are in
			    Oper<T,U>.add(m2,i,row,t.neg(m1.value(i,col)));
			    Oper<T,U>.add(m1,i,row,t.neg(m1.value(i,col)));//make it zero
			}
			row++;//result for increasing the rank
		    }
		}
	    }
	    
	    // gives a matrix without the row numbered row and column numbered col
	    public static Matrix<T, U> Minor(Matrix<T, U> m, int row, int col)//this matrix in needed to compute adj
	    {
		Matrix<T, U> output = new Matrix<T, U>(m.row, m.col);
		for (int i = 0; i < m.row; i++)
		{
		    if (i == row) continue;
		    for (int j = 0; j < m.col; j++)
		    {
			if (j == col) continue;
			output.set(i, j, m.value(i, j));
		    }
		}
		return output;
	    }
	    public static U Det(Matrix<T, U> m)
	    {
		if (m.row != m.col) return default(U);
		if (m.row == 2) return (t.add(t.mult(m.value(0, 0), m.value(1, 1)), t.neg(t.mult(m.value(0, 1), m.value(1, 0)))));//|m|=a*d-b*c
		U output = t.zero;
		for (int i = 0; i < m.col; i++)
		{
		    if (i % 2 == 0) { t.equal(ref output, t.add(output, t.mult(m.value(0, i), Det(Minor(m, 0, i))))); }
		    else { t.equal(ref output, t.neg(t.add(output, t.mult(m.value(0, i), Det(Minor(m, 0, i)))))); }
		    /*summary : m[0,0]*Minor(0,0)-m[0,1]*Minor(0,1)+ ..... (-1)^(n+1)*m[0,n]*Minor(0,n) */
		}
		return output;
	    }
	    public static U Det(Matrix<T, U> m, int r, int c)//must choose column or row and set the unneeded to -1
	    {
		if (m.row != m.col) return default(U);
		if (m.row == 2) return (t.add(t.mult(m.value(0, 0), m.value(1, 1)), t.neg(t.mult(m.value(0, 1), m.value(1, 0)))));//|m|=a*d-b*c
		U output = t.zero;
		if (r == -1)
		{ //calculate by column
		    for (int i = 0; i < m.row; i++)
		    {
			if ((i + c) % 2 == 0) { t.equal(ref output, t.add(output, t.mult(m.value(i, c), Det(Minor(m, i, c))))); }
			else { t.equal(ref output, t.neg(t.add(output, t.mult(m.value(i, c), Det(Minor(m, i, c)))))); }
			/*summary : m[0,c]*Minor(0,c)-m[1,c]*Minor(1,c)+ ..... (-1)^(n+c)*m[n,c]*Minor(n,c) */
		    }
		    
		}
		else
		{ //calculate by Row
		    for (int i = 0; i < m.col; i++)
		    {
			if ((i + r) % 2 == 0) { t.equal(ref output, t.add(output, t.mult(m.value(r, i), Det(Minor(m, r, i))))); }
			else { t.equal(ref output, t.neg(t.add(output, t.mult(m.value(r, i), Det(Minor(m, r, i)))))); }
			/*summary : m[r,0]*Minor(r,0)-m[r,1]*Minor(r,1)+ ..... (-1)^(n+r)*m[r,n]*Minor(r,n) */
		    }
		}
		return output;
	    }
	}
   
   
   public static class Tool<T, U> where T : Field<U>, new()
	{
	    public static T t = new T();
	    public static Matrix<T, U> Zero(int row, int col)
	    {
		Matrix<T, U> temp = new Matrix<T, U>(row, col);
		return temp;
	    }
	    public static Matrix<T, U> Zero(int n)
	    {
		Matrix<T, U> temp = new Matrix<T, U>(n, n);
		return temp;
	    }
	    public static Matrix<T, U> Diag(int n)
	    {
		Matrix<T, U> m = new Matrix<T, U>(n, n);
		
		for (int i = 0; i < n; ++i)
		    m.set(i, i, t.one);
		return m;
	    } //matrix I
	    public static Matrix<T, U> Diag(int n, U q)
	    {
		Matrix<T, U> m = new Matrix<T, U>(n, n);
		
		for (int i = 0; i < n; ++i)
		    m.set(i, i, t.mult(t.one, q));
		return m;
	    }// alpha I
	    public static Matrix<T, U> cVect(int n)//colomn vector
	    {
		if (n > 0)
		    return new Matrix<T, U>(n, 1);
		else
		    return null;
	    }
	    public static Matrix<T, U> rVect(int n)//row vector
	    {
		if (n > 0)
		    return new Matrix<T, U>(1, n);
		else
		    return null;
	    }
	    static Matrix<T, U> Reduced(Matrix<T, U> m) //THE ONE AAAAAAND ONLY RRRRANKED M
	    {
		Matrix<T, U> output = new Matrix<T, U>(m.row, m.col);
		Oper<T, U>.Copy(output, m);
		Oper<T, U>.reduced1(output);
		return output;
	    }
	    public static Matrix<T, U> inv(Matrix<T, U> m)//M^-1 
	    {
		if (m.row != m.col) return default(Matrix<T, U>);
		
		Matrix<T, U> tmp = new Matrix<T, U>(m.row, m.col);
		Oper<T, U>.Copy(tmp, m);
		
		Matrix<T, U> output = Diag(m.row);
		Oper<T, U>.reduced2(tmp, output);
		
		return output;
	    }
	    
	    public static Matrix<T, U> Elem(int n, int r1, int r2)
	    {
		return Oper<T, U>.swap(Diag(n), r1, r2);
	    }//the elemintary matrix that swaps rows
	    public static Matrix<T, U> Elem(int n, int r, U alpha)
	    {
		return Oper<T, U>.mult(Diag(n), r, alpha);
	    }//the elemintary matrix that multiply row by alpha
	    public static Matrix<T, U> Elem(int n, int r1, int r2, U alpha)
	    {
		return Oper<T, U>.add(Diag(n), r1, r2, alpha);
	    }//the elemintary : r1=r1+alpha*r2
	    
	    public static Matrix<T, U> Adj(Matrix<T, U> m)
	    {
		if (m.row != m.col) return default(Matrix<T, U>);
		Matrix<T, U> output = new Matrix<T, U>(m.row, m.col);
		for (int i = 0; i < m.row; i++)
		{
		    for (int j = 0; j < m.col; j++)
		    {
			if (i + j % 2 == 0)
			    output.set(i, j, Oper<T, U>.Det(Oper<T, U>.Minor(m, j, i)));//i and j flipped because we want m*Adj(m)=Det(m)*I
			else
			    output.set(i, j, t.neg(Oper<T, U>.Det(Oper<T, U>.Minor(m, j, i))));
		    }
		}
		return output;
	    }
	}
   public class Real : Field<double> //an example for field 
   {
       public override double zero
       {
           get { return 0; }
       }
       public override double one
       {
           get { return 1; }
       }
       public override double add(double a, double b)
       {
           return a + b;
       }
       public override double mult(double a, double b)
       {
           return a * b;
       }
       public override void equal(ref double output, double input)
       {
           output = input;
       }//value in b heads to a
       public override double inv(double a)
       {
           return ((double)1 / a);
       }
       public override double neg(double a)
       {
           return -a;
       }
       public override bool isequal(double a, double b)
       {
           return a == b;
       }
	
   }
   /*might need more work 
    * for example this program need more clarity
    * when dealing with the matrix and it's members*/
    class Program
    {
	
        static void Main(string[] args)
        {
	    
           //example
           Matrix<Real, double> A;
           int r, c;
           Console.Write("enter row:");
           r = int.Parse(Console.ReadLine());
           Console.Write("enter col:");
           c = int.Parse(Console.ReadLine());
           A = new Matrix<Real, double>(r, c);
           for(int i=0;i<r;i++)
               for(int j=0;j<c;j++)
               {
                   Console.Write("value in {0},{1}:", i + 1, j + 1);
                   A.set(i, j, double.Parse(Console.ReadLine()));
               }
           Console.WriteLine("reduced A:");
           Tool<Real, double>.Reduced(A).print();
           Console.ReadLine();
        }
    }
}

