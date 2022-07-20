create database QuanLyNhaHang
use QuanLyNhaHang
go

create table NguoiDung
(
	TK varchar(50) primary key,
	MK varchar(50)
)

create table Hanghoa(
	Mahh varchar(20) primary key,
	Tenhh nvarchar(20),
	DongGia int,
	Nsx varchar(20),
	Hsd date
)

create table KhachHang(
	MaKH varchar(20) primary key,
	TenKH nvarchar(50),
	SDT varchar(20)
)

create table NhanVien(
	MaNV varchar(20) not null,
	TenNV nvarchar(50),
	GioiTinh varchar(20),
	QueQuan nvarchar(50),
	SDT varchar(20),
	ChucVu varchar(20),
	HSL float,
	primary key(MaNV),
)

create table HoaDon(
	MaHD varchar(20) primary key,
	MaNV varchar(20),
	MaKH varchar(20),
	NgayHD date,
	TongTien int,
	TrangThai varchar(20)
	foreign key (MaNV) references NhanVien(MaNV),
	foreign key (MaKH) references KhachHang(MaKH),
)

Create table CTHoaDon
(
	MaHD varchar(20) not null,
	Mahh varchar(20) not null,
	SoLuong int,
	primary key (MaHD, Mahh),
	foreign key (Mahh) references Hanghoa(Mahh),
	foreign key (MaHD) references HoaDon(MaHD),
)

Create Trigger TG_TongTien_HoaDon_insert on CTHoaDon
for insert, update
as
	Update HoaDon
	Set TongTien = (select sum(Hanghoa.DongGia*CTHoaDon.SoLuong)
					from CTHoaDon, Hanghoa, HoaDon
					where CTHoaDon.Mahh = Hanghoa.Mahh and HoaDon.MaHD = CTHoaDon.MaHD and HoaDon.MaHD = (select MaHD from inserted) 
					group by HoaDon.MaHD)
	Where HoaDon.MaHD = (select MaHD from inserted)



