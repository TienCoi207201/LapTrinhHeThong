#include <iostream>
#include <pthread.h>
#include <queue>
#include <conio.h>

using namespace std;
queue<char> Q;
int stop = 0;
char c;

void *fun_thread1(void *data){
	//char c;
	while(1){
		c = getch();
		if(c==27){
			stop = 1;
			break;
		}
		//cout << "Thread 1: " << c <<endl;
		Q.push(c);
	}
}

void *fun_thread2(void *data){
//	char c;
	while(!stop){
		while(!Q.empty()){
			c = Q.front();
			cout << "In thread 2: " << c <<endl;
			Q.pop();
		}
	}
}

int main(int argc, char *argv[]){
	int status;
	pthread_t thrd_1,thrd_2;// thread objects
	// create thread
	cout << "In put a char to push to queue: " << endl;
	pthread_create(&thrd_1,NULL,fun_thread1,NULL);
	pthread_create(&thrd_2,NULL,fun_thread2,NULL);
	pthread_join(thrd_1, (void **)&status);
	pthread_join(thrd_2, (void **)&status);
	system("PAUSE");
	return 1;
}
