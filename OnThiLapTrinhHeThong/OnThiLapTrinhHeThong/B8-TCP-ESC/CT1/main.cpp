#include <stdio.h>
#include <winsock2.h>
#include <conio.h>
#include<stdlib.h> 
#include<time.h>
#include<pthread.h>

void *GuiKiTu(void *data){
		/* Khai b?o bien */
	WSADATA wsaData;
	SOCKADDR_IN rAddr;
	int iRc;
	/* Nap thu vien v? khoi tao socket */
	iRc = WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET sk = socket(AF_INET, SOCK_STREAM, 0);
	/* Ket noi den Server */
	rAddr.sin_family = AF_INET;
	rAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	rAddr.sin_port = htons(100);
	iRc = connect(sk, (LPSOCKADDR)&rAddr, sizeof(rAddr));
	srand(time(NULL));
	while(1)
	{
		char c = getch(); 
		send(sk, (const char*)&c, sizeof(c), 0);// gui so nguyen sang sever
		printf("\nKi tu da nhap: %c",c);
		if(c == 27)
			break;		
	}
	/* ??ng socket v? giai ph?ng t?i nguy?n */
	closesocket(sk);
	WSACleanup();	
    printf("\n");
	getch();
	system("PAUSE");
	return 0;
}


int main()
{
	int status;	
	pthread_t thrd_1;	
	pthread_create(&thrd_1,NULL,GuiKiTu,NULL);	
	pthread_join(thrd_1, (void **)&status);
	system("PAUSE");
	return 0;
}


