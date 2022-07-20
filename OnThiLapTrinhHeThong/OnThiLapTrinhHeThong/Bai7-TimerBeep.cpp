#include<stdio.h>
#include <conio.h>
#include<pthread.h>
#include<windows.h>

char c = '*';  

void *Timer_1(void *data){
	while(true){
		c = getch();
		printf("\nki tu da nhap :%c",c);
		if(c == '!')
			break;
		
		Sleep(7);
	}
}
void *Timer_2(void *data){
	while(true){
		Beep(700,800);
		if(c == '!')
			break;
		
		Sleep(10);
	}
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
