#include<stdio.h>
#include <conio.h>
#include<pthread.h>
#include<windows.h>
void *Timer_1(void *data){
	FILE *fp; 
	while(true){
		char c = getch();
		fp = fopen("kitu.txt","a"); 
		printf("\nki tu da nhap :%c",c);
		fprintf(fp,"%c",c);
		fclose(fp);	
		if(c == '!')
			break;
		
		Sleep(5);
	}
}
void *Timer_2(void *data){
	FILE *fp; 
	fp = fopen("kitu.txt","r");
	while(true){
		char flag = '*';
		char c; 
		fscanf(fp,"%c",&c);
		if(flag != c){
			flag = c;
			printf("\nki tu da doc :%c\n",flag); 
		}else{
			printf("\nki tu da doc :%c\n",flag); 	
		}
		if(c == '!')
			break;
		
		Sleep(10);
	}
	fclose(fp);	
}

int main (){
	int status;	
	pthread_t thrd_1;
	pthread_t thrd_2;	
	pthread_create(&thrd_1,NULL,Timer_1,NULL);	
	pthread_create(&thrd_1,NULL,Timer_2,NULL);	
	pthread_join(thrd_1, (void **)&status);
	pthread_join(thrd_2, (void **)&status);
	system("PAUSE");
	return 0;
}
