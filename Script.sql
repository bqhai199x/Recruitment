﻿INSERT INTO Position VALUES ('C#'),('Java'),('PHP'),('NodeJS')
GO

INSERT INTO [Level] VALUES ('Internship'),('Fresher'),('Junior'),('Senior')
GO

INSERT INTO Candidate(PositionId,LevelId,FullName,Birthday,[Address],Phone,Email,IsPDF,CV,IntroduceName,IsApplied,IsContacted,[Status]) VALUES
(1,1,N'Bùi Quang Hải','10/11/1999',N'Đông Anh - Hà Nội','0976445870','haibq@saisystem.vn',0,'CV_BuiQuangHai.pdf',NULL,0,NULL,0),
(1,2,N'Nguyễn Duy Tín','12/01/1999',N'Đan Phượng - Hà Nội','0972493540','tinnd@saisystem.vn',0,'CV_NguyenDuyTin.pdf',NULL,2,0,0),
(2,3,N'Lương Đình Nam','14/09/1999',N'Cầu Giấy - Hà Nội','0968254196','namld@saisystem.vn',0,'CV_LuongDinhNam.pdf',N'Bùi Quang Hải',NULL,0,0),
(2,4,N'Trần Thiên Điệp','23/07/1999',N'Ba Vì - Hà Nội','0927863541','dieptt@saisystem.vn',0,'CV_TranThienDiep.pdf',N'Nguyễn Duy Tín',0,1,0),
(3,4,N'Nguyễn Văn Luân','09/12/1999',N'Cổ Nhuế - Hà Nội','0971269852','luannv@saisystem.vn',0,'CV_NguyenVanLuan.pdf',N'Bùi Quang Hải',NULL,1,0),
(4,1,N'Cao Đức Anh Vũ','26/01/1999',N'Phạm Văn Đồng - Hà Nội','0945896217','vucda@saisystem.vn',0,'CV_CaoDucAnhVu.pdf',N'Nguyễn Văn Luân',4,0,0)

Select * FROM Candidate