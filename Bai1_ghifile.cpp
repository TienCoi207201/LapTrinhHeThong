#include<stdio.h>
#include<math.h>
#include<pthread.h>
#include <ctime>
#include <conio.h>
#include <cstdlib>
void *GhiFile(void *data){
	FILE *fp;
	srand(time(NULL));
	fp = fopen("dulieu.txt","w");
	while(true){
		int r = rand();
		fprintf(fp,"%d ",r);	
		printf("\nSo da ghi %d\n",r);	
		if(r % 1024 == 0)
			break;	
	}
	fclose(fp);	
}


int main () {
	int status;	
	pthread_t thrd_1;	
	pthread_create(&thrd_1,NULL,GhiFile,NULL);	
	pthread_join(thrd_1, (void **)&status);
	system("PAUSE");
	return 0;
}

