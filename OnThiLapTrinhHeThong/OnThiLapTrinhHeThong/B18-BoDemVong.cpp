#include<stdio.h>
#include<pthread.h>
#include<windows.h>
int a[100]; 
int len = 0; 
int flag = 0;
int length =0; 
void *NhapMangSoNguyen(void *data){
	int n; 
	int i = 0;
	while(1){
		printf("\nNhap so nguyen:");
		scanf("%d",&n);
		if(n < 0){
			flag = 1;
			break; 
		}
		a[i] = n;
		i++;
		len++;
		length ++; 
		Sleep(20);
	}
}

void TBC(){
	int sum = 0; 
	for(int i=0 ; i< length; i++){
		sum += a[i]; 
	}
    float tbc = float(sum / length); 
    printf("\nTrung binh cong cacs phan tu trong mang la:%f",tbc);
}

void *HienThiSoNguyen(void *data){
	  int i = 0; 
	  while(true){
	  	if(len > 0){
	  	 	printf("\nGia tri so nguyen da nhap trong mang:%d\n",a[i]);
			i++;
			len--;
		  }
		if(flag == 1)
			break;
	  }
	  TBC();
}



int main (){
	int status;
	pthread_t thrd_1;
	pthread_t thrd_2;
	pthread_create(&thrd_1,NULL,NhapMangSoNguyen,NULL);
	pthread_create(&thrd_2,NULL,HienThiSoNguyen,NULL);
	pthread_join(thrd_1, (void **)&status);
	pthread_join(thrd_2, (void **)&status);
	printf("\n");
	system("PAUSE");
	return 1;
}
