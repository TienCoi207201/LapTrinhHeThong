#include <iostream>
#include <windows.h> 
#include <pthread.h>
using namespace std;

string st;
void * task1(void *data)
{   
	while (1)
    {
          
		cin>>st;
		//xoa ki tu rong o dau
		while (st[0] == ' '){
			st.erase(st.begin() + 0);
		}
		//xoa ki tu rong o duoi
		while (st[st.length() - 1] == ' '){
			st.erase(st.begin() + st.length() - 1);
		}
		if(st=="exit")
			break;
	}   
}

void* task2(void* data)
{
	while(1){
		cout<<"Xau ki tu vua nhap la: ";
		cout<<st<<endl;
		while (st[0] == ' '){
			st.erase(st.begin() + 0);
		}
		while (st[st.length() - 1] == ' '){
			st.erase(st.begin() + st.length() - 1);
		}
		if(st=="exit")
			break;
	}
}


int main()
{
    int status;
	pthread_t thrd1,thrd2;
	pthread_create(&thrd1,NULL,task1,NULL);
	pthread_create(&thrd2,NULL,task2,NULL);
	pthread_join(thrd1,(void**)&status);
	pthread_join(thrd2,(void**)&status);
	return 1;
}


