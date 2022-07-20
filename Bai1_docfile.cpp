#include<stdio.h>
#include<math.h>
#include<pthread.h>
#include <ctime>
#include <conio.h>
#include <cstdlib>
void *DocFile(void *data){
	FILE *fp;
	srand(time(NULL));
	fp = fopen("dulieu.txt","r");
	while(true){
 		 int r;
		 fscanf(fp,"%d",&r);
		 printf("\nSo da doc %d\n",r);	
		 if(r % 1024 == 0)
			break;		
	}
	fclose(fp);
}


int main () {
	int status;	
	pthread_t thrd_1;	
	pthread_create(&thrd_1,NULL,DocFile,NULL);	
	pthread_join(thrd_1, (void **)&status);
	system("PAUSE");
	return 0;
}

