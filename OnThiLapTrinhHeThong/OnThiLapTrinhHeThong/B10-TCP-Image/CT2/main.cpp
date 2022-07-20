#include <iostream>
#include <stdio.h>
#include <winsock2.h>// bat buoc
#include <conio.h>
int main(){
	/* Khai báo bien */
	WSADATA wsaData;
	SOCKADDR_IN serAddr, cliAddr;
	int iRc;
	int adsize;
	char buffer[256];
	
	/* Nap thu vien và khoi tao socket */
	iRc = WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET ssk = socket(AF_INET, SOCK_STREAM, 0);
	
	/* Thiet lap cau trúc dia cho cho socket */
	serAddr.sin_family = AF_INET;
	serAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	serAddr.sin_port = htons(1024);
	
	/* Gan dia cho cho socket và thuc hien cho thiet lap ket noi */
	iRc = bind(ssk, (LPSOCKADDR)&serAddr, sizeof(serAddr));
	iRc = listen(ssk, 5);
	adsize = sizeof(cliAddr);
	SOCKET sk = accept(ssk, (LPSOCKADDR)&cliAddr, &adsize);
	while(1){
		char st[1024];
		iRc=recv(sk,(char*)st, 256, 0);
		int len;
		iRc=recv(sk,(char*)&len,sizeof(int), 0);
		FILE *fp=fopen(st,"w+b");
		if(fp!=NULL){
			char buff[512];
			int nhan=0;
			while(nhan<len){//nhan du lieu
				iRc=recv(sk,(char*)buff,512, 0);
				if(iRc>0){
					nhan+=iRc;
					fwrite(buff,sizeof(unsigned char),iRc,fp);
				}
			}
			fclose(fp);
			system("TEST.JPG");
			break;
		}
	}
	closesocket(sk);
	closesocket(ssk);
	WSACleanup();	
	getch();
	return 0;
}
