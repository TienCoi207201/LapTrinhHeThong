#include<stdio.h>
#include<conio.h>
#include<pthread.h>
void *NhapFile(void *data){
	FILE *fp; 
	while(true){
		char c = getch();
		fp = fopen("kitu.txt","a"); 
		printf("\nki tu da nhap :%c",c);
		fprintf(fp,"%c",c);
		fclose(fp);	
		if(c == '!')
			break;
	}
}

int main (){
	int status;	
	pthread_t thrd_1;	
	pthread_create(&thrd_1,NULL,NhapFile,NULL);	
	pthread_join(thrd_1, (void **)&status);
	system("PAUSE");
	return 0;
}
