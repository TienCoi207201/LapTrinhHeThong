#include<stdio.h>
#include<stdlib.h>
#include<time.h>
#include <ctime>
#include<windows.h>
#include<pthread.h>


void *NhapFile (void *data){
	srand(time(NULL));
	FILE *fp;
	while(true){
	 fp = fopen("vd.dat","w"); 
	 int r = rand(); 
	 fprintf(fp,"%d",r); 
	 printf("\nCac so da ghi la:%d",r);	
	 fclose(fp);
	 Sleep(10);
	}
}


int main (){
	int status;
	pthread_t thrd_1;
	pthread_create(&thrd_1,NULL,NhapFile,NULL);
	pthread_join(thrd_1, (void **)&status);
	system("PAUSE");
	return 1;
}
