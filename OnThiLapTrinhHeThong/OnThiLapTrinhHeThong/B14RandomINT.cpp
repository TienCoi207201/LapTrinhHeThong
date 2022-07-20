#include<iostream>
#include<windows.h>
#include<pthread.h>
#include<time.h>
using namespace std;
int stop=0;
void *func1(void *data){
	int *num = (int *)data;
	srand(time(0));
	while(true){
		*num = rand();
		cout<<"\nSo da random:"<<*num;
		if(*num % 10011 == 0){
			stop = 1;
			break;
		}
		Sleep(10);
	}
}

void *func2(void *data){
	int *num = (int *)data;
	while(true){
		if(*num % 3 == 0) cout <<"\nSo chia het cho 3:"<< *num << endl;
		if(stop) break;
		Sleep(10);
	}
}

int main(){
	int status;
	int num;
	pthread_t thrd1;
	pthread_t thrd2;
	pthread_create(&thrd1, NULL, func1, &num);
	pthread_create(&thrd2, NULL, func2, &num);
	pthread_join(thrd1, (void**)&status);
	pthread_join(thrd2, (void**)&status);
	system("pause");
	return 0;
}
