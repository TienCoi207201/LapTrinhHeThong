#include <stdio.h>
#include <winsock2.h>
#include <conio.h>

int main()
{
	/* Khai báo bien */
	WSADATA wsaData;
	SOCKADDR_IN rAddr;
	//char buffer[20]="Hello2";
	int iRc;
	/* Nap thu vien và khoi tao socket */
	iRc = WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET sk = socket(AF_INET, SOCK_STREAM, 0);
	/* Ket noi den Server */
	rAddr.sin_family = AF_INET;
	rAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	rAddr.sin_port = htons(1024);
	iRc = connect(sk, (LPSOCKADDR)&rAddr, sizeof(rAddr));
	
	while(1)
	{
	char st[256];
	printf("nhap xau:");
	gets(st);
	send(sk, st, strlen(st), 0);// lenh gui xau ki tu sang server
	if(strcmp(st,"bye")==0)
	break;

	}
	/* Ðóng socket và giai phóng tài nguyên */
	closesocket(sk);
	WSACleanup();
	
	getch();
	return 0;
}