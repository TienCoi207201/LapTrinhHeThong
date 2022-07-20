#include<stdio.h>
#include<conio.h>
#include<pthread.h>
void *DocFile(void *data){
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
	}
	fclose(fp);	
}

int main (){
	int status;	
	pthread_t thrd_1;	
	pthread_create(&thrd_1,NULL,DocFile,NULL);	
	pthread_join(thrd_1, (void **)&status);
	system("PAUSE");
	return 0;
}
