#include <iostream>
#include <pthread.h>
#include<stdlib.h>
#include <cstdlib>
#include <ctime>
void * fun_thread1(void *data){
	int *pa=(int*)data;
	
	while(true)
	{
		int n;
		printf("\nnhap n=");
		scanf("%d",&n);
		if(n<=0 || n> 10)
		break;
		for(int i=0; i <n;i++)
		{
			printf("phan tu thu %d:",i);
			scanf("%d",&(pa[i]));
		}//for
	}//while
}
void * fun_thread2(void *data){
	int *pb=(int*)data;
	srand(time(NULL));
	while(true)
	{
		int n=rand()%100 + (int)rand()/(int)RAND_MAX;;
		printf("\nSo luong so random la:%d",n);
		if(n > 10)
		break;
		for(int i=0; i <n;i++){
			pb[i]=rand();
			printf("\nCac so ngau nhien dc sinh ra:%d",pb[i]);
		}
		}//while
	}
int main(int argc, char *argv[]){
	int a[10];
	int b[10];
	int status;
	pthread_t thrd_1,thrd_2;// thread objects
	// create thread
	pthread_create(&thrd_1,NULL,fun_thread1,&a);
	pthread_create(&thrd_2,NULL,fun_thread2,&b);
	pthread_join(thrd_1, (void **)&status);
	pthread_join(thrd_2, (void **)&status);
	system("PAUSE");
	return 1;
}
