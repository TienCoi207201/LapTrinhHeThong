#include <iostream>
#include <stdio.h>
#include <winsock2.h>// bat buoc
#include <conio.h>
int main()
{
	/* Khai báo bi?n */
	WSADATA wsaData;
	SOCKADDR_IN serAddr, cliAddr;
	int iRc;
	int adsize;

	/* N?p thu vi?n và kh?i t?o socket */
	iRc = WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET ssk = socket(AF_INET, SOCK_STREAM, 0);

	/* Thi?t l?p c?u trúc d?a ch? cho socket */
	serAddr.sin_family = AF_INET;
	serAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	serAddr.sin_port = htons(100);

	/* G?n d?a ch? cho socket và th?c hi?n ch? thi?t l?p k?t n?i */
	iRc = bind(ssk, (LPSOCKADDR)&serAddr, sizeof(serAddr));
	iRc = listen(ssk, 5);
	adsize = sizeof(cliAddr);
	SOCKET sk = accept(ssk, (LPSOCKADDR)&cliAddr, &adsize);
	while(1)
	{
		int n;
		int a[100];//sua
		int b[100];
		recv(sk,(char*)&n, sizeof(n), 0);
		printf("n=%d\n",n);
		if(n < 0)
			break;
		recv(sk,(char*)a, n*sizeof(int), 0);//sua
		
		int k = 0 ; 		
		for(int i = n-1; i >= 0 ; i--){
			b[k] = a[i]; 
			k++;
		}
		
		for(int i=0; i<n; i++){
			printf("\n%d Doi sang HEXA :%X",b[i],b[i]);
		}
	}// while
	/* Ðóng socket và gi?i phóng tài nguyên */
	closesocket(sk);
	closesocket(ssk);
	WSACleanup();

	getch();
	return 0;
}



