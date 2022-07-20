#include<stdio.h>
#include<windows.h>
#include<pthread.h>
#include<time.h>
char c; 
char flag = '*'; 
int kt = 0; 
void *Thread_1(void *data){
	FILE *fp;
	fp = fopen("vd.txt","r"); 
	while(true){
		fscanf(fp,"%c",&c); 
		if(flag != c){
			flag = c; 
		}else{
			kt = 1; 
			break;
		}
		printf("\n%c chuyen sang Hexa:%X\n",flag,flag);
	}
		fclose(fp);
}


void *Thread_2(void *data){
	while(true){
		printf("\nKi tu doc duoc o thread_1 la:%c\n",c);
		if(kt == 1)
			break; 
	}
}

int main (){
	int status;
	pthread_t thrd_1;
	pthread_t thrd_2;
	pthread_create(&thrd_1,NULL,Thread_1,NULL);
	pthread_create(&thrd_1,NULL,Thread_2,NULL);
	pthread_join(thrd_1, (void **)&status);
	pthread_join(thrd_2, (void **)&status);
	system("PAUSE");
	return 1;
}


