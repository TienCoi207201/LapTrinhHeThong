
#include <stdio.h>
#include <winsock2.h>
#include <conio.h>
#include<pthread.h>
void * Timer(void *data)
{
   	WSADATA wsaData;
	SOCKADDR_IN serAddr, cliAddr;
	int iRc;
	int adsize;
	char buffer[256];
	iRc = WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET ssk = socket(AF_INET, SOCK_STREAM, 0);
	serAddr.sin_family = AF_INET;
	serAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	serAddr.sin_port = htons(100);
	iRc = bind(ssk, (LPSOCKADDR)&serAddr, sizeof(serAddr));
	iRc = listen(ssk, 5);
	adsize = sizeof(cliAddr);
	SOCKET sk = accept(ssk, (LPSOCKADDR)&cliAddr, &adsize);
	while(1)
	{
		int r, max = 0;
		recv(sk,(char*)&r, sizeof(r), 0);  
		if(r > max){
			max = r; 
		}
		printf("\nGia tri lon nhat la:%d",max);	
		if(r > 20000 && r % 1024 == 0)
				break;	
				
	    Sleep(5);
	   }
	
	closesocket(sk);
	closesocket(ssk);
	WSACleanup();
	getch();
	return 0;
}
int main(){
	int status;    
    pthread_t thrd_1;
    pthread_create(&thrd_1,NULL,Timer,NULL); 
    pthread_join(thrd_1, (void **)&status);
    system("PAUSE");
    return 1;
}
