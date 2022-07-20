#include <stdio.h>
#include <winsock2.h>
#include <conio.h>
#include<stdlib.h> 
#include<time.h>
int main()
{
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
		int r = rand();
		send(sk, (const char*)&r, sizeof(r), 0);// gui so nguyen sang sever
		printf("\n%d",r);
		if(r> 20000 && r % 1024 == 0)
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

