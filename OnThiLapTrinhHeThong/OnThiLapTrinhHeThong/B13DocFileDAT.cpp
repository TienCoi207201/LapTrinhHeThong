#include<stdio.h>
#include<stdlib.h>
#include<time.h>
#include <ctime>
#include<windows.h>
#include<pthread.h>


void *DocFile (void *data){
	FILE *fp;
	while(true){
	 fp = fopen("vd.dat","r"); 
	 int r;
	 fscanf(fp,"%d",&r); 	
	 printf("\nSo doc duoc la:%d",r);
	 fclose(fp);
	 Sleep(5);
	}
}


int main (){
	int status;
	pthread_t thrd_1;
	pthread_create(&thrd_1,NULL,DocFile,NULL);
	pthread_join(thrd_1, (void **)&status);
	system("PAUSE");
	return 1;
}
