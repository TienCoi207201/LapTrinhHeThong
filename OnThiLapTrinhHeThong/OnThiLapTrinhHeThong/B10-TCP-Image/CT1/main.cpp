#include <stdio.h>
#include <winsock2.h>
#include <conio.h>

int main(){
	/* Khai báo bien */
	WSADATA wsaData;
	SOCKADDR_IN rAddr;

	int iRc;
	/* Nap thu vien và khoi tao socket */
	iRc = WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET sk = socket(AF_INET, SOCK_STREAM, 0);
	/* Ket noi den Server */
	rAddr.sin_family = AF_INET;
	rAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	rAddr.sin_port = htons(1024);
	iRc = connect(sk, (LPSOCKADDR)&rAddr, sizeof(rAddr));

	FILE *fp=fopen("TEST.JPG","rb");
	
	if(fp!=NULL){
		char st[1024];
		strcpy(st,"TEST.JPG");
		send(sk,(const char*)st, strlen(st)+1, 0);// lenh gui sr sang server, gui ca ki tu ket thuc xau '\0'
		
		// tinh do dai file
		int len;
		fseek(fp,0,SEEK_END);
		len=ftell(fp);
		fseek(fp,0,SEEK_SET);
		// gui di do dai file
		send(sk,(const char*)&len,sizeof(int),0);
		// doc moi lan 512 byte
		unsigned char buff[1024];
		int n;
		
		while(1){
			n=fread(buff,sizeof(unsigned char),1024,fp);
			if(n<=0){
				send(sk,(const char*)&n,sizeof(int), 0);
				break;
			}
			send(sk,(const char*)buff,n*sizeof(unsigned char), 0);
		}
		fclose(fp);
	}
	closesocket(sk);
	WSACleanup();
	
	getch();
	return 0;
}
