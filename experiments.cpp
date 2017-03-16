#include <iostream>
//define the width and hight of the board
#define HEIGHT 10
#define WIDTH 10
#define ALIVE true
#define DEAD false
namespace board
{
void copy(bool* b1,const bool* b2,int heigth=HEIGHT,int width=WIDTH);
//copy values of b2 to b1
void show(const bool* b1,int height=HEIGHT,int width=WIDTH);
//show the values of b1
int count_neighbors(const bool* b1,int row,int col,int height=HEIGHT,int width=WIDTH);
//count alive neighbors to a cell
void update(const bool* b1,bool* b2,int height=HEIGHT,int width=WIDTH);
//update b2 according to the rules in wiki
}
// ---------MAIN---------
int main( )
{
  bool *mainboard= new bool[HEIGHT*WIDTH],*secboard=new bool[HEIGHT*WIDTH];

  for(int i=0;i<HEIGHT;i++)
    for(int j=0;j<WIDTH;j++)
        mainboard[i*WIDTH+j]=DEAD;
  mainboard[5]=ALIVE;
  mainboard[6]=ALIVE;
  mainboard[7]=ALIVE;
      board::show(mainboard);

      board::update(mainboard,secboard);
      board::copy(mainboard,secboard);
    board::show(mainboard);
  delete[] mainboard;
  delete[] secboard;
  return 0;
}
void board::copy(bool* b1,const bool* b2,int height,int width)
{
  for(int i=0;i<height;i++)
    for(int j=0;j<width;j++)
      b1[i*width+j]=b2[i*width+j];
}
void board::show(const bool* b1,int height,int width)
{
  for(int i=0;i<height;i++)
    {
      for(int j=0;j<width-1;j++)
        if(b1[i*width+j]==ALIVE)
          std::cout<<"|*";
        else
          std::cout<<"| ";

        if(b1[i*width+width-1]==ALIVE)
          std::cout<<"*|";
        else
          std::cout<<" |";
      std::cout<<std::endl;//moving to a new line
    }
}
int board::count_neighbors(const bool* b1,int row,int col,int height,int width)
{
  int count=0;
  for(int i=row-1;i<=row+1;i++)
    for(int j=col-1;j<=col+1;j++)//check a 3 by 3 square around the cell
      if(i>=0 && i<height && j>=0 && j<width)//the index is in range
        {
          if(i==row && j==col)//we can't count the cell we got
            continue;
          if(b1[i*width+j]==ALIVE)
            count++;
        }
  return count;
}
void board::update(const bool* b1,bool* b2,int height,int width)
{
  int neighbors;//saving the number of alive
  for(int i=0;i<height;i++)
    for(int j=0;j<width;j++)
      {
        neighbors=board::count_neighbors(b1,i,j,height,width);
        switch(neighbors)
          {//rules:
          case 0:
          case 1: b2[i*width+j]=DEAD; break;
            //less than two neighbors must die
          case 2: if(b1[i*width+j]==DEAD)
                    {b2[i*width+j]=DEAD;
                    break;}
          case 3: b2[i*width+j]=ALIVE; break;
//if the cell if alive and has neighbors of two to three or its dead and has exactly three neighbors
          case 4:
          case 5:
          case 6:
          case 7:// more than three neighbors -> dies
          case 8: b2[i*width+j]=DEAD; break;
          }
      }
}
