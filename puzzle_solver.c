#include<stdio.h>
#include<math.h>
#define TEST 1
double nums[4]={2,4,5,6};
char oper[6]={'+','-','*','/','^','&'};// & more a^(1/b) and ^ for power
double operate(char op,double a,double b)
{
switch(op)
  {
   case '+':return a+b;
   case '-':return a-b;
   case '*':return a*b;
   case '/':
   case '\\': return a/b;
   case '^':return pow(a,b);
   case '&':return pow(a,1/b);
   default: printf("there is no such operation\n"); return -1;
  }
}
int possible(char oper[],int target)
{
    double tmp;
for(int i=0;i<4;i++)
  {
      for(int j=0;j<4;j++)
      {
          if(j==i) continue;
          for(int h=0;h<4;h++)
          {
              if(h==i ||h==j) continue;
              for(int k=0;k<4;k++)
              {
                  if(k==i||k==j||k==h)
                  {
                      continue;
                       end: printf("! the Solution is up there\n");
                  return 1;
                  }
                  tmp=operate(oper[0],nums[i],nums[j]);
                  tmp=operate(oper[1],tmp,nums[h]);
                  tmp=operate(oper[2],tmp,nums[k]);
                  if(tmp==target){printf("((%.f%c%.f)%c%.f)%c%.f=%.2f\n",nums[i],oper[0],nums[j],oper[1],nums[h],oper[2],nums[k],tmp);
                   goto end;}
                  tmp=operate(oper[2],nums[h],nums[k]);
                  tmp=operate(oper[1],nums[j],tmp);
                  tmp=operate(oper[0],nums[i],tmp);
                  if(tmp==target) {printf("%.f%c(%.f%c(%.f%c%.f))=%.2f\n",nums[i],oper[0],nums[j],oper[1],nums[h],oper[2],nums[k],tmp);
                   goto end;}
                  tmp=operate(oper[0],nums[i],nums[j]);
                  tmp=operate(oper[1],tmp,operate(oper[2],nums[h],nums[k]));
                  if(tmp==target){printf("(%.f%c%.f)%c(%.f%c%.f)=%.2f\n",nums[i],oper[0],nums[j],oper[1],nums[h],oper[2],nums[k],tmp);
                   goto end;}
                  tmp=operate(oper[1],nums[j],nums[h]);
                  if((tmp=operate(oper[2],operate(oper[0],nums[i],tmp),nums[k]))==target)
                  {
                      printf("(%.f%c(%.f%c%.f))%c%.f=%.2f\n",nums[i],oper[0],nums[j],oper[1],nums[h],oper[2],nums[k],tmp);
                      goto end;
                  }
                  if((tmp=operate(oper[0],nums[i],operate(oper[2],tmp,nums[k])))==target)
                  {
                       printf("%.f%c((%.f%c%.f)%c%.f)=%.2f\n",nums[i],oper[0],nums[j],oper[1],nums[h],oper[2],nums[k],tmp);
                      goto end;
                  }

              }
          }
      }
  }
return 0;
}
void husen(double t)
{
char arrtmp[3];
   int i,j,k;
   for( i=0;i<4;i++)
    {
    arrtmp[0]=oper[i];
    for( j=0;j<4;j++)
    {
        if(j==i) continue;
        arrtmp[1]=oper[j];
        for( k=0;k<4;k++)
        {
            if(k==j||k==i) continue;
            arrtmp[2]=oper[k];
            if(possible(arrtmp,t)) break;
        }
        if(k!=4) break;
    }
    if(j!=4) break;
    }
    if(i==4) printf("NO SOLUTION\n");
}
int main()
{
    for(int i=7;i<=20;i++)
     {
         printf("in base %d\n",i);
         husen(2*i+6);
     }
    return 0;
}
