#include <stdio.h>
#include <winsock2.h>
#include <conio.h>

int main()
{
	/* Khai báo bi?n */
	WSADATA wsaData;
	SOCKADDR_IN rAddr;
	//char buffer[20]="Hello2";
	int iRc;
	/* N?p thu vi?n và kh?i t?o socket */
	iRc = WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET sk = socket(AF_INET, SOCK_STREAM, 0);
	/* K?t n?i d?n Server */
	rAddr.sin_family = AF_INET;
	rAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	rAddr.sin_port = htons(100);
	iRc = connect(sk, (LPSOCKADDR)&rAddr, sizeof(rAddr));

	while(1)
	{
		int a[100];//sua
		int n;		
		printf("nhap so phan tu:");
		scanf("%d",&n);
		send(sk,(const char*)&n, sizeof(n), 0);// lenh gui n sang server
		
		if(n < 0)
			break;
		
		int b[100]; 	
		for(int i=0;i < n;i++)
			scanf("%d",&a[i]);//int
		
		int k = 0; 
		for(int i = n-1; i >= 0 ; i--){
			b[k] = a[i]; 
			k++;
		} 		
		send(sk,(const char*)b, n*sizeof(int), 0);//sua
	}
	/* Ðóng socket và gi?i phóng tài nguyên */
	closesocket(sk);
	WSACleanup();

	getch();	
    return 0;
}



