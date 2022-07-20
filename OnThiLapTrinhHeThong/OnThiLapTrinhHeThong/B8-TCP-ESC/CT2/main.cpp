
#include <stdio.h>
#include <winsock2.h>
#include <conio.h>
#include<pthread.h>
void * NhanKiTu(void *data)
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
		char c;
		recv(sk,(char*)&c, sizeof(c), 0);  
		if(c  == 27) break;
		printf("\nKi tu da nhan la:%c",c);	
		
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
    pthread_create(&thrd_1,NULL,NhanKiTu,NULL); 
    pthread_join(thrd_1, (void **)&status);
    system("PAUSE");
    return 1;
}

