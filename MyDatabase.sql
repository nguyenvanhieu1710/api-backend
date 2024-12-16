-- ==========================> create database <======================================= --
CREATE DATABASE MyDatabase;
go
USE MyDatabase;
go
-- ==========================> create table <======================================= --
-- ==========================> Account Table <======================================= --
CREATE TABLE Account (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    AccountName NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    DayCreated DATE NOT NULL,
    RememberPassword BIT DEFAULT 0 not null,
    Email NVARCHAR(100) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Deleted BIT DEFAULT 0  not null
);
go
-- ==========================> Users Table <=======================================
CREATE TABLE Users (
    UserId INT references Account(AccountId),
    UserName NVARCHAR(100) NOT NULL,
    Birthday DATE NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Image NVARCHAR(MAX) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Address NVARCHAR(100) NOT NULL,
	Ranking nvarchar(20) not null,
    Deleted BIT DEFAULT 0 not null,
	primary key(UserId)
);
go
-- ==========================> Staff Table <=======================================
create table Staff(
	StaffId int references Account(AccountId),
	StaffName nvarchar(100) not null,
	Birthday date not null,
	PhoneNumber nvarchar(20) not null,
	Image nvarchar(max) not null,
	Gender nvarchar(10) not null,
	Address nvarchar(100) not null,
	Position nvarchar(50) not null,
	Deleted BIT DEFAULT 0 not null,
	primary key(StaffId)
);
go
-- ==========================> Category Table <=======================================
CREATE TABLE Category (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
	CategoryImage nvarchar(max) not null,
    DadCategoryId INT NOT NULL,
	Deleted BIT DEFAULT 0  not null
);
go
CREATE TABLE Product (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Brand NVARCHAR(50) NOT NULL,
    ProductImage NVARCHAR(MAX) NOT NULL,
	Star int not null,
    ProductDetail NVARCHAR(MAX) NOT NULL,
    CategoryId INT FOREIGN KEY REFERENCES Category(CategoryId),
    Deleted BIT DEFAULT 0  not null
);
go
-- ==========================> Cart Table <=======================================
create table Cart(
	ProductId int references Product(ProductId),
	UserId int references Users(UserId),
	primary key(ProductId, UserId)
);
go
-- ==========================> Voucher Table <=======================================
CREATE TABLE Voucher (
    VoucherId INT IDENTITY(1,1) PRIMARY KEY,
    VoucherName NVARCHAR(100) NOT NULL,
    Price DECIMAL NOT NULL,
    MinimumPrice DECIMAL NOT NULL,
    Quantity INT NOT NULL,
    StartDay DATETime NOT NULL,
    EndDate DATETime NOT NULL,
    Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> Orders Table <=======================================
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    StaffId INT FOREIGN KEY REFERENCES Staff(StaffId),
    OrderStatus NVARCHAR(50) NOT NULL,
    DayBuy DATE NOT NULL,
	--DateOfReceipt date not null,
    DeliveryAddress NVARCHAR(100) NOT NULL,
	Evaluate int not null,
    Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> OrderDetail Table <=======================================
CREATE TABLE OrderDetail (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT FOREIGN KEY REFERENCES Orders(OrderId),
    ProductId INT FOREIGN KEY REFERENCES Product(ProductId),
    Quantity INT NOT NULL,
    Price DECIMAL NOT NULL,
    DiscountAmount DECIMAL NOT NULL,
    VoucherId INT FOREIGN KEY REFERENCES Voucher(VoucherId)
);
go
-- ==========================> Supplier Table <=======================================
CREATE TABLE Supplier (
    SupplierId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(100) NOT NULL,
    Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> ImportBill Table <=======================================
CREATE TABLE ImportBill (
    ImportBillId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierId INT FOREIGN KEY REFERENCES Supplier(SupplierId),
    StaffId INT FOREIGN KEY REFERENCES Staff(StaffId),
    InputDay DATE NOT NULL,
	Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> ImportBill Table <=======================================
CREATE TABLE ImportBillDetail (
    ImportBillDetailId INT IDENTITY(1,1) PRIMARY KEY,
    ImportBillId INT FOREIGN KEY REFERENCES ImportBill(ImportBillId),
    ProductId INT FOREIGN KEY REFERENCES Product(ProductId),
    ImportPrice DECIMAL NOT NULL,
    ImportQuantity INT NOT NULL
);
go
-- ==========================> Comment Table <=======================================
create table Comment(
	CommentId int identity(1,1) primary key,
	Content nvarchar(max) not null,
	Time datetime not null,
	SenderId INT FOREIGN KEY REFERENCES Users(UserId),
	ProductId int REFERENCES Product(ProductId),
	Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> Message Table <=======================================
create table Message(
	MessageId int identity(1,1) primary key,
	Content nvarchar(max) not null,
	Time datetime not null,
	SenderId INT REFERENCES Users(UserId),
	ReceiverId INT REFERENCES Staff(StaffId),
	Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> News Table <=======================================
create table News(
	NewsId int identity(1,1) primary key,
	NewsName nvarchar(100) not null,
	Content nvarchar(max) not null,
	NewsImage nvarchar(max) not null,
	PostingDate date not null,
	PersonPostingId INT REFERENCES Users(UserId),
	--Rate int not null,
	Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> Advertisement Table <=======================================
create table Advertisement(
	AdvertisementId int identity(1,1) primary key,
	AdvertisementName nvarchar(100) not null,
	AdvertisementImage nvarchar(max) not null,
	Location nvarchar(max) not null,
	AdvertiserId INT FOREIGN KEY REFERENCES Staff(StaffId),
	Deleted BIT DEFAULT 0 not null
);
go
-- ==========================> Stored Procedure <=======================================
-- stored procedures of Account table --
create procedure sp_account_get_data_by_id (@account_Id int)
as
	begin
		select * from Account where AccountId = @account_Id and Deleted = 0
	end;
go
create procedure sp_account_get_data_by_accountName_and_password(
@account_AccountName nvarchar(100), @account_Password NVARCHAR(100))
as 
	begin
		select * from Account
		where AccountName = @account_AccountName and Password = @account_Password and Deleted = 0
	end
go
create procedure sp_account_create (@account_AccountName NVARCHAR(100), 
@account_Password NVARCHAR(100))
as
	begin
		-- phải thêm 3 số ngẫu nhiên ở cuối để tránh trùng lặp tên tài khoản --
		--declare @ramdomNumber nvarchar(3)
		--declare @acccount_Name nvarchar(100)
		--set @ramdomNumber = cast(cast(rand() * 1000 as int) as nvarchar)
		--set @acccount_Name = CONCAT(@account_UserName, @ramdomNumber)
		insert into Account values(@account_AccountName, @account_Password, N'User', GETDATE(), 
			0, N'defaut@gmail.com', N'Offline', 0)
		DECLARE @account_Id INT;
		SET @account_Id = SCOPE_IDENTITY ();
		insert into Users
		values(@account_Id, @account_AccountName, '1985-12-20', '0123456789',
		'image_b.jpg', 'Female', '456 High Street', 'Gold', 0);
	end;
go
-- ==========================> stored procedures of Voucher table <=======================================
create procedure sp_voucher_create (@voucher_Name NVARCHAR(100), 
@voucher_Price DECIMAL, @voucher_MinimumPrice DECIMAL, @voucher_Quantity INT,
@voucher_StartDay DATETime, @voucher_EndDate DATETime)
as
	begin
		insert into Voucher
		values(@voucher_Name, @voucher_Price, @voucher_MinimumPrice, @voucher_Quantity,
			@voucher_StartDay, @voucher_EndDate, 0);
	end;
go
create procedure sp_voucher_update (@voucher_Id int, @voucher_Name NVARCHAR(100), 
@voucher_Price DECIMAL, @voucher_MinimumPrice DECIMAL, @voucher_Quantity INT,
@voucher_StartDay DATETime, @voucher_EndDate DATETime, @voucher_Deleted bit)
as
	begin
		update Voucher
		set VoucherName = @voucher_Name, 
			Price = @voucher_Price, MinimumPrice = @voucher_MinimumPrice,
			Quantity = @voucher_Quantity, StartDay = @voucher_StartDay,
			EndDate = @voucher_EndDate, Deleted = @voucher_Deleted
		where VoucherId = @voucher_Id
	end;
go
create procedure sp_voucher_delete (@voucher_Id int)
as
	begin
		Delete from Voucher
		where VoucherId = @voucher_Id
	end;
go
create procedure sp_voucher_all
as
	begin
		select * from Voucher where Deleted = 0
	end;
go
create procedure sp_voucher_get_data_by_id (@voucher_Id int)
as
	begin
		select * from Voucher where VoucherId = @voucher_Id and Deleted = 0
	end;
go
create procedure sp_voucher_search (@voucher_Name nvarchar(100))
as
	begin
		select * from Voucher 
		where (LOWER(VoucherName) like '%' + LOWER(@voucher_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_voucher_pagination (@voucher_pageNumber int, @voucher_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@voucher_pageNumber - 1) * @voucher_pageSize;
		select * from Voucher 
		where Deleted = 0
		ORDER BY VoucherId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @voucher_pageSize ROWS ONLY;
	end
go
create procedure sp_voucher_deleted_pagination (@voucher_pageNumber int, @voucher_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@voucher_pageNumber - 1) * @voucher_pageSize;
		select * from Voucher 
		where Deleted = 1
		ORDER BY VoucherId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @voucher_pageSize ROWS ONLY;
	end
go
CREATE PROCEDURE sp_voucher_search_pagination 
(
    @voucher_Name NVARCHAR(100),
    @voucher_pageNumber INT,
    @voucher_pageSize INT
)
AS
BEGIN
    DECLARE @NumberOfIgnoredRecords INT;
    SET @NumberOfIgnoredRecords = (@voucher_pageNumber - 1) * @voucher_pageSize;

    SELECT *
    FROM Voucher
    WHERE (LOWER(VoucherName) LIKE '%' + LOWER(@voucher_Name) + '%') and Deleted = 0
    ORDER BY VoucherId
    OFFSET @NumberOfIgnoredRecords ROWS
    FETCH NEXT @voucher_pageSize ROWS ONLY;
END;
go
-- ==========================> stored procedures of Category table <=======================================
create procedure sp_category_create (@category_Name NVARCHAR(100), 
@category_Image nvarchar(max), @category_DadCategoryId INT)
as
	begin
		insert into Category
		values(@category_Name, @category_Image, @category_DadCategoryId, 0);
	end;
go
create procedure sp_category_update (@category_Id int, @category_Name NVARCHAR(100), 
@category_Image nvarchar(max), @category_DadCategoryId INT, @category_Deleted bit)
as
	begin
		update Category
		set CategoryName = @category_Name, CategoryImage = @category_Image, 
		DadCategoryId = @category_DadCategoryId, Deleted = @category_Deleted
		where CategoryId = @category_Id
	end;
go
create procedure sp_category_delete (@category_Id int)
as
	begin
		Delete from Category
		where CategoryId = @category_Id
	end;
go
create procedure sp_category_delete_virtual(@category_Id int)
as
	begin
		update Product
		set Deleted = 1
		where CategoryId = @category_Id

		update Category
		set Deleted = 1
		where CategoryId = @category_Id
	end
go
create procedure sp_category_all
as
	begin
		select * from Category where Deleted = 0
	end;
go
create procedure sp_category_get_data_by_id (@category_Id int)
as
	begin
		select * from Category where CategoryId = @category_Id and Deleted = 0
	end;
go
create procedure sp_category_search (@category_Name nvarchar(100))
as
	begin
		select * from Category 
		where (LOWER(CategoryName) like '%' + LOWER(@category_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_category_pagination (@category_pageNumber int, @category_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@category_pageNumber - 1) * @category_pageSize;
		select * from Category 
		where Deleted = 0
		ORDER BY CategoryId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @category_pageSize ROWS ONLY;
	end
go
create procedure sp_category_deleted_pagination (@category_pageNumber int, @category_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@category_pageNumber - 1) * @category_pageSize;
		select * from Category 
		where Deleted = 1
		ORDER BY CategoryId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @category_pageSize ROWS ONLY;
	end
go
create procedure sp_category_search_pagination (@category_pageNumber int, @category_pageSize int,
@category_Name nvarchar(100))
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@category_pageNumber - 1) * @category_pageSize;
		select * from Category 
		where (LOWER(CategoryName) like '%' + LOWER(@category_Name) + '%') and Deleted = 0
		ORDER BY CategoryId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @category_pageSize ROWS ONLY;
	end
go
-- ====================> stored procedures of Product table <=========================== --
create procedure sp_product_create (@product_Name NVARCHAR(100), 
@product_Quantity int, @product_Price decimal, @product_Description nvarchar(max),
@product_Brand nvarchar(50), @product_Image nvarchar(max), @product_Star int,
@product_ProductDetail NVARCHAR(MAX), @product_CategoryId INT)
as
	begin
		insert into Product
		values(@product_Name, @product_Quantity,
			@product_Price, @product_Description, @product_Brand,
			@product_Image, @product_Star, @product_ProductDetail,
			@product_CategoryId, 0);
	end;
go
create procedure sp_product_update (@product_Id int, @product_Name NVARCHAR(100), 
@product_Quantity int, @product_Price decimal, @product_Description nvarchar(max),
@product_Brand nvarchar(50), @product_Image nvarchar(max), @product_Star int,
@product_ProductDetail NVARCHAR(MAX), @product_CategoryId INT, @product_Deleted bit)
as
	begin
		update Product
		set ProductName = @product_Name, 
			Quantity = @product_Quantity, Price = @product_Price,
			Description = @product_Description, Brand = @product_Brand ,
			ProductImage = @product_Image , Star = @product_Star ,
			ProductDetail = @product_ProductDetail , CategoryId = @product_CategoryId,
			Deleted = @product_Deleted
		where ProductId = @product_Id
	end;
go
create procedure sp_product_delete (@product_Id int)
as
	begin
		Delete from Product
		where ProductId = @product_Id
	end;
go
create procedure sp_product_all
as
	begin
		select * from Product where Deleted = 0
	end;
go
create procedure sp_product_get_data_by_id (@product_Id int)
as
	begin
		select * from Product where ProductId = @product_Id and Deleted = 0
	end;
go
create procedure sp_product_search (@product_Name nvarchar(100))
as
	begin
		select * from Product 
		where (LOWER(ProductName) like '%' + LOWER(@product_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_product_pagination (@product_pageNumber int, @product_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@product_pageNumber - 1) * @product_pageSize;
		select * from Product
		where Deleted = 0
		order by ProductId
		offset @NumberOfRecordsToIgnore rows
		fetch next @product_pageSize rows only;
	end
go
create procedure sp_product_deleted_pagination (@product_pageNumber int, @product_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@product_pageNumber - 1) * @product_pageSize;
		select * from Product
		where Deleted = 1
		order by ProductId
		offset @NumberOfRecordsToIgnore rows
		fetch next @product_pageSize rows only;
	end
go
CREATE PROCEDURE sp_product_search_pagination 
(
    @product_Name NVARCHAR(100),
    @product_pageNumber INT,
    @product_pageSize INT
)
AS
BEGIN
    DECLARE @NumberOfIgnoredRecords INT;
    SET @NumberOfIgnoredRecords = (@product_pageNumber - 1) * @product_pageSize;

    SELECT *
    FROM Product
    WHERE (LOWER(ProductName) LIKE '%' + LOWER(@product_Name) + '%') and Deleted = 0
    ORDER BY ProductId
    OFFSET @NumberOfIgnoredRecords ROWS
    FETCH NEXT @product_pageSize ROWS ONLY;
END;
go
create procedure sp_product_get_best_selling
as
	begin
		select * from Product
		where ProductId in (
			select top(5) ProductId from OrderDetail
			order by Quantity desc) and Deleted = 0
	end
go
-- ==========================> stored procedures of orders table <================================ --
create procedure sp_orders_create (@orders_UserId INT, 
@orders_StaffId INT, @orders_OrderStatus NVARCHAR(50), @orders_DayBuy DATE,
@orders_DeliveryAddress NVARCHAR(100), @orders_Evaluate int, 
@listjson_orderDetail NVARCHAR(MAX))
as
	begin
		insert into Orders
		values(@orders_UserId, @orders_StaffId, @orders_OrderStatus, @orders_DayBuy,
			@orders_DeliveryAddress, @orders_Evaluate, 0);
		declare @orders_Id int
		set @orders_Id = SCOPE_IDENTITY();
		if(@listjson_orderDetail is not null)
			begin
				insert into OrderDetail(OrderId, ProductId, Quantity, Price, DiscountAmount, VoucherId)
				select @orders_Id,
					JSON_VALUE(p.value, '$.productId') AS ProductId,
					JSON_VALUE(p.value, '$.quantity') AS Quantity,
					JSON_VALUE(p.value, '$.price') AS Price,
					JSON_VALUE(p.value, '$.discountAmount') AS DiscountAmount,
					JSON_VALUE(p.value, '$.voucherId') AS VoucherId
				from openjson(@listjson_orderDetail) as p;
		 	end;
	end;
go
create procedure sp_orders_update (@orders_Id int, @orders_UserId INT, 
@orders_StaffId INT, @orders_OrderStatus NVARCHAR(50), @orders_DayBuy DATE,
@orders_DeliveryAddress NVARCHAR(100), @orders_Evaluate int, @orders_Deleted bit,
@listjson_orderDetail NVARCHAR(MAX))
as
	begin
		update Orders
		set UserId = @orders_UserId, StaffId = @orders_StaffId, 
			OrderStatus = @orders_OrderStatus, DayBuy = @orders_DayBuy, 
			DeliveryAddress = @orders_DeliveryAddress, Evaluate = @orders_Evaluate, 
			Deleted = @orders_Deleted
		where OrderId = @orders_Id
		if(@listjson_orderDetail is not null)
			begin
				-- Insert data to temp table 
				SELECT
					JSON_VALUE(p.value, '$.orderDetailId') as OrderDetailId,
					JSON_VALUE(p.value, '$.productId') as ProductId,
					JSON_VALUE(p.value, '$.quantity') as Quantity,
					JSON_VALUE(p.value, '$.price') as Price,
					JSON_VALUE(p.value, '$.discountAmount') as DiscountAmount,
					JSON_VALUE(p.value, '$.voucherId') as VoucherId,
					JSON_VALUE(p.value, '$.orderDetailStatus') AS OrderDetailStatus 
				INTO #Results 
				FROM OPENJSON(@listjson_orderDetail) AS p;

				-- Insert data to table with STATUS = 1;
				INSERT INTO OrderDetail(OrderId, ProductId, Quantity, Price, DiscountAmount, VoucherId)
				SELECT
					@orders_Id,
					#Results.ProductId,
					#Results.Quantity,
					#Results.Price,
					#Results.DiscountAmount,
					#Results.VoucherId
				FROM  #Results 
				WHERE #Results.OrderDetailStatus = '1' 
			
				-- Update data to table with STATUS = 2
				UPDATE OrderDetail 
				SET
					ProductId = #Results.ProductId,
					Quantity = #Results.Quantity,
					Price = #Results.Price,
					DiscountAmount = #Results.DiscountAmount,
					VoucherId = #Results.VoucherId
				FROM #Results 
				WHERE OrderDetail.OrderDetailId = #Results.OrderDetailId AND #Results.OrderDetailStatus = '2';
			
			-- Delete data to table with STATUS = 3
			DELETE OrderDetail
			FROM OrderDetail INNER JOIN #Results
				ON OrderDetail.OrderDetailId = #Results.OrderDetailId
			WHERE #Results.OrderDetailStatus = '3';
			DROP TABLE #Results;
			end;
	end;
go
create procedure sp_orders_delete (@orders_Id int)
as
	begin
		delete from OrderDetail
		where OrderId = @orders_Id
		Delete from Orders
		where OrderId = @orders_Id
	end;
go
CREATE PROCEDURE sp_orders_all
AS
BEGIN
    SELECT 
        o.OrderId,
        o.UserId,
        o.StaffId,
        o.OrderStatus,
        o.DayBuy,
        o.DeliveryAddress,
        o.Evaluate,
        o.Deleted,
        od.OrderDetailId,
        od.ProductId,
        od.Quantity,
        od.Price,
        od.DiscountAmount,
        od.VoucherId
    FROM 
        Orders o
    LEFT JOIN 
        OrderDetail od ON o.OrderId = od.OrderId
    WHERE 
        o.Deleted = 0
END;
go
create procedure sp_orders_get_data_by_id (@orders_Id int)
as
	begin
		SELECT 
        o.OrderId,
        o.UserId,
        o.StaffId,
        o.OrderStatus,
        o.DayBuy,
        o.DeliveryAddress,
        o.Evaluate,
        o.Deleted,
        od.OrderDetailId,
        od.ProductId,
        od.Quantity,
        od.Price,
        od.DiscountAmount,
        od.VoucherId
    FROM 
        Orders o
    LEFT JOIN 
        OrderDetail od ON o.OrderId = od.OrderId
    WHERE o.OrderId = @orders_Id
	end;
go
create procedure sp_orders_get_data_by_userid_and_pagination (@userId int, 
@orders_pageNumber int, @orders_pageSize int)
as
begin
    declare @NumberOfRecordsToIgnore int;
    set @NumberOfRecordsToIgnore = (@orders_pageNumber - 1) * @orders_pageSize;

    SELECT 
        o.OrderId,
        o.UserId,
        o.StaffId,
        o.OrderStatus,
        o.DayBuy,
        o.DeliveryAddress,
        o.Evaluate,
        o.Deleted,
        od.OrderDetailId,
        od.ProductId,
        od.Quantity,
        od.Price,
        od.DiscountAmount,
        od.VoucherId
    FROM 
        Orders o
    LEFT JOIN 
        OrderDetail od ON o.OrderId = od.OrderId
    WHERE 
        o.Deleted = 0 and o.UserId = @userId
    ORDER BY 
        o.OrderId
    OFFSET @NumberOfRecordsToIgnore ROWS
    FETCH NEXT @orders_pageSize ROWS ONLY;
end;
go
create procedure sp_orders_search_user (@userName nvarchar(100))
as
begin
    SELECT 
        o.OrderId,
        o.UserId,
        o.StaffId,
        o.OrderStatus,
        o.DayBuy,
        o.DeliveryAddress,
        o.Evaluate,
        o.Deleted,
        od.OrderDetailId,
        od.ProductId,
        od.Quantity,
        od.Price,
        od.DiscountAmount,
        od.VoucherId
    FROM 
        Orders o
    LEFT JOIN 
        OrderDetail od ON o.OrderId = od.OrderId
    WHERE 
        o.UserId in (
            SELECT UserId 
            FROM Users 
            WHERE LOWER(UserName) LIKE '%' + LOWER(@userName) + '%'
        ) 
        AND o.Deleted = 0
end;
go
create procedure sp_orders_search_product (@productName nvarchar(100))
as
begin
    SELECT 
        o.OrderId,
        o.UserId,
        o.StaffId,
        o.OrderStatus,
        o.DayBuy,
        o.DeliveryAddress,
        o.Evaluate,
        o.Deleted,
        od.OrderDetailId,
        od.ProductId,
        od.Quantity,
        od.Price,
        od.DiscountAmount,
        od.VoucherId
    FROM 
        Orders o
    LEFT JOIN 
        OrderDetail od ON o.OrderId = od.OrderId
    WHERE 
        od.ProductId in (
            SELECT ProductId 
            FROM Product 
            WHERE LOWER(ProductName) LIKE '%' + LOWER(@productName) + '%'
        ) 
        AND o.Deleted = 0
end;
go
create procedure sp_orders_pagination (@orders_pageNumber int, @orders_pageSize int)
as
begin
    declare @NumberOfRecordsToIgnore int;
    set @NumberOfRecordsToIgnore = (@orders_pageNumber - 1) * @orders_pageSize;

    SELECT 
        o.OrderId,
        o.UserId,
        o.StaffId,
        o.OrderStatus,
        o.DayBuy,
        o.DeliveryAddress,
        o.Evaluate,
        o.Deleted,
        od.OrderDetailId,
        od.ProductId,
        od.Quantity,
        od.Price,
        od.DiscountAmount,
        od.VoucherId
    FROM 
        Orders o
    LEFT JOIN 
        OrderDetail od ON o.OrderId = od.OrderId
    WHERE 
        o.Deleted = 0
    ORDER BY 
        o.OrderId
    OFFSET @NumberOfRecordsToIgnore ROWS
    FETCH NEXT @orders_pageSize ROWS ONLY;
end;
go
create procedure sp_orders_deleted_pagination (@orders_pageNumber int, @orders_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@orders_pageNumber - 1) * @orders_pageSize;

		SELECT 
			o.OrderId,
			o.UserId,
			o.StaffId,
			o.OrderStatus,
			o.DayBuy,
			o.DeliveryAddress,
			o.Evaluate,
			o.Deleted,
			od.OrderDetailId,
			od.ProductId,
			od.Quantity,
			od.Price,
			od.DiscountAmount,
			od.VoucherId
		FROM 
			Orders o
		LEFT JOIN 
			OrderDetail od ON o.OrderId = od.OrderId
		WHERE 
			o.Deleted = 1
		ORDER BY 
			o.OrderId
		offset @NumberOfRecordsToIgnore rows
		fetch next @orders_pageSize rows only;
	end
go
create procedure sp_orders_search_pagination (@orders_pageNumber int, @orders_pageSize int,
@userName nvarchar(100))
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@orders_pageNumber - 1) * @orders_pageSize;
		SELECT 
			o.OrderId,
			o.UserId,
			o.StaffId,
			o.OrderStatus,
			o.DayBuy,
			o.DeliveryAddress,
			o.Evaluate,
			o.Deleted,
			od.OrderDetailId,
			od.ProductId,
			od.Quantity,
			od.Price,
			od.DiscountAmount,
			od.VoucherId
		FROM 
			Orders o
		LEFT JOIN 
			OrderDetail od ON o.OrderId = od.OrderId
		WHERE 
			o.UserId in (
				SELECT UserId 
				FROM Users 
				WHERE LOWER(UserName) LIKE '%' + LOWER(@userName) + '%'
			) 
			AND o.Deleted = 0
		ORDER BY 
			o.OrderId		
		offset @NumberOfRecordsToIgnore rows
		fetch next @orders_pageSize rows only;
	end
go
-- ==========================> stored procedures of users table <=======================================
create procedure sp_users_create (@users_UserName NVARCHAR(100), 
@users_Birthday DATE, @users_PhoneNumber NVARCHAR(20), @users_Image NVARCHAR(MAX),
@users_Gender NVARCHAR(10), @users_Address NVARCHAR(100), @users_Ranking nvarchar(20))
as
	begin
		-- phải thêm 3 số ngẫu nhiên ở cuối để tránh trùng lặp tên tài khoản --
		declare @ramdomNumber nvarchar(3)
		declare @acccount_Name nvarchar(100)
		set @ramdomNumber = cast(cast(rand() * 1000 as int) as nvarchar)
		set @acccount_Name = CONCAT(@users_UserName, @ramdomNumber)
		insert into Account values(@acccount_Name, N'123', N'User', GETDATE(), 
			0, N'defaut@gmail.com', N'Offline', 0)
		DECLARE @account_Id INT;
		SET @account_Id = SCOPE_IDENTITY ();
		insert into Users
		values(@account_Id, @users_UserName, @users_Birthday, @users_PhoneNumber, @users_Image,
			@users_Gender, @users_Address, @users_Ranking, 0);
	end;
go
create procedure sp_users_update (@users_Id int, @users_UserName NVARCHAR(100), 
@users_Birthday DATE, @users_PhoneNumber NVARCHAR(20), @users_Image NVARCHAR(MAX),
@users_Gender NVARCHAR(10), @users_Address NVARCHAR(100), @users_Ranking nvarchar(20),
@users_Deleted bit)
as
	begin
		IF @users_Deleted = 1
		begin
			update Account
			set Deleted = @users_Deleted
			where AccountId = @users_Id
		end
		update Users
		set UserName = @users_UserName, Birthday = @users_Birthday, 
			PhoneNumber = @users_PhoneNumber, Image = @users_Image, 
			Gender = @users_Gender, Address = @users_Address, 
			Ranking = @users_Ranking, Deleted = @users_Deleted		
		where UserId = @users_Id
	end;
go
create procedure sp_users_delete (@users_Id int)
as
	begin
		Delete from Users
		where UserId = @users_Id
		Delete from Account
		where AccountId = @users_Id
	end;
go
create procedure sp_users_all
as
	begin
		select * from Users where Deleted = 0
	end;
go
create procedure sp_users_get_data_by_id (@users_Id int)
as
	begin
		select * from Users where UserId = @users_Id and Deleted = 0
	end;
go
create procedure sp_users_get_data_by_username_and_password 
(@account_UserName NVARCHAR(100), @account_Password NVARCHAR(100))
as
	begin 
		select * from Users where UserId = (
			select AccountId from Account 
			where AccountName = @account_UserName and Password = @account_Password)
			and Deleted = 0
	end;
go
create procedure sp_users_search (@users_Name nvarchar(100))
as
	begin
		select * from Users 
		where (LOWER(UserName) like '%' + LOWER(@users_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_users_pagination (@users_pageNumber int, @users_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@users_pageNumber - 1) * @users_pageSize;
		select * from Users
		where Deleted = 0
		order by UserId
		offset @NumberOfRecordsToIgnore rows
		fetch next @users_pageSize rows only;
	end
go
create procedure sp_users_deleted_pagination (@users_pageNumber int, @users_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@users_pageNumber - 1) * @users_pageSize;
		select * from Users
		where Deleted = 1
		order by UserId
		offset @NumberOfRecordsToIgnore rows
		fetch next @users_pageSize rows only;
	end
go
create procedure sp_users_search_pagination (@users_pageNumber int, @users_pageSize int,
@users_Name nvarchar(100))
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@users_pageNumber - 1) * @users_pageSize;
		select * from Users
		where (LOWER(UserName) like '%' + LOWER(@users_Name) + '%') and Deleted = 0
		order by UserId
		offset @NumberOfRecordsToIgnore rows
		fetch next @users_pageSize rows only;
	end
go
-- ==========================> stored procedures of staff table <=======================================
create procedure sp_staff_create (@staff_StaffName NVARCHAR(100), 
@staff_Birthday DATE, @staff_PhoneNumber NVARCHAR(20), @staff_Image NVARCHAR(MAX),
@staff_Gender NVARCHAR(10), @staff_Address NVARCHAR(100), @staff_Position nvarchar(50))
as
	begin
		-- phải thêm 3 số ngẫu nhiên ở cuối để tránh trùng lặp tên tài khoản --
		declare @ramdomNumber nvarchar(3)
		declare @acccount_Name nvarchar(100)
		set @ramdomNumber = cast(cast(rand() * 1000 as int) as nvarchar)
		set @acccount_Name = CONCAT(@staff_StaffName, @ramdomNumber)
		insert into Account values(@acccount_Name, N'123', N'Staff', GETDATE(), 
			0, N'defaut@gmail.com', N'Offline', 0)
		DECLARE @account_Id INT;
		SET @account_Id = SCOPE_IDENTITY ();
		insert into Staff
		values(@account_Id, @staff_StaffName, @staff_Birthday, @staff_PhoneNumber, @staff_Image,
			@staff_Gender, @staff_Address, @staff_Position, 0);
	end;
go
create procedure sp_staff_update (@staff_Id int, @staff_StaffName NVARCHAR(100), 
@staff_Birthday DATE, @staff_PhoneNumber NVARCHAR(20), @staff_Image NVARCHAR(MAX),
@staff_Gender NVARCHAR(10), @staff_Address NVARCHAR(100), @staff_Position nvarchar(50),
@staff_Deleted bit)
as
	begin
		IF @staff_Deleted = 1
		begin
			update Account
			set Deleted = @staff_Deleted
			where AccountId = @staff_Id
		end
		update Staff
		set StaffName = @staff_StaffName, Birthday = @staff_Birthday, 
			PhoneNumber = @staff_PhoneNumber, Image = @staff_Image, 
			Gender = @staff_Gender, Address = @staff_Address, 
			Position = @staff_Position, Deleted = @staff_Deleted		
		where StaffId = @staff_Id
	end;
go
create procedure sp_staff_delete (@staff_Id int)
as
	begin
		Delete from Staff
		where StaffId = @staff_Id
		Delete from Account
		where AccountId = @staff_Id
	end;
go
create procedure sp_staff_all
as
	begin
		select * from Staff where Deleted = 0
	end;
go
create procedure sp_staff_get_data_by_id (@staff_Id int)
as
	begin
		select * from Staff where StaffId = @staff_Id and Deleted = 0
	end;
go
create procedure sp_staff_search (@staff_Name nvarchar(100))
as
	begin
		select * from Staff 
		where (LOWER(StaffName) like '%' + LOWER(@staff_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_staff_pagination (@staff_pageNumber int, @staff_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@staff_pageNumber - 1) * @staff_pageSize;
		select * from Staff
		where Deleted = 0
		order by StaffId
		offset @NumberOfRecordsToIgnore rows
		fetch next @staff_pageSize rows only;
	end
go
create procedure sp_staff_deleted_pagination (@staff_pageNumber int, @staff_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@staff_pageNumber - 1) * @staff_pageSize;
		select * from Staff
		where Deleted = 1
		order by StaffId
		offset @NumberOfRecordsToIgnore rows
		fetch next @staff_pageSize rows only;
	end
go
create procedure sp_staff_search_pagination (@staff_pageNumber int, @staff_pageSize int,
@staff_Name nvarchar(100))
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@staff_pageNumber - 1) * @staff_pageSize;
		select * from Staff
		where (LOWER(StaffName) like '%' + LOWER(@staff_Name) + '%') and Deleted = 0
		order by StaffId
		offset @NumberOfRecordsToIgnore rows
		fetch next @staff_pageSize rows only;
	end
go
-- ==========================> stored procedures of advertisement table <=======================================
create procedure sp_advertisement_create (@advertisement_Name NVARCHAR(100), 
@advertisement_AdvertisementImage nvarchar(max), @advertisement_Location nvarchar(max), 
@advertisement_AdvertiserId INT)
as
	begin
		insert into Advertisement
		values(@advertisement_Name, @advertisement_AdvertisementImage, 
		@advertisement_Location, @advertisement_AdvertiserId, 0);
	end;
go
create procedure sp_advertisement_update (@advertisement_Id int, @advertisement_Name NVARCHAR(100), 
@advertisement_AdvertisementImage nvarchar(max), @advertisement_Location nvarchar(max), 
@advertisement_AdvertiserId INT, @advertisement_Deleted bit)
as
	begin
		update Advertisement
		set AdvertisementName = @advertisement_Name, 
			AdvertisementImage = @advertisement_AdvertisementImage,
			Location = @advertisement_Location,
			AdvertiserId = @advertisement_AdvertiserId,
			Deleted = @advertisement_Deleted
		where AdvertisementId = @advertisement_Id
	end;
go
create procedure sp_advertisement_delete (@advertisement_Id int)
as
	begin
		Delete from Advertisement
		where AdvertisementId = @advertisement_Id
	end;
go
create procedure sp_advertisement_all
as
	begin
		select * from Advertisement where Deleted = 0
	end;
go
create procedure sp_advertisement_get_data_by_id (@advertisement_Id int)
as
	begin
		select * from Advertisement 
		where AdvertisementId = @advertisement_Id and Deleted = 0
	end;
go
create procedure sp_advertisement_search (@advertisement_Name nvarchar(100))
as
	begin
		select * from Advertisement 
		where (LOWER(AdvertisementName) like '%' + LOWER(@advertisement_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_advertisement_pagination (@advertisement_pageNumber int, @advertisement_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@advertisement_pageNumber - 1) * @advertisement_pageSize;
		select * from Advertisement 
		where Deleted = 0
		ORDER BY AdvertisementId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @advertisement_pageSize ROWS ONLY;
	end
go
create procedure sp_advertisement_deleted_pagination (@advertisement_pageNumber int, @advertisement_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@advertisement_pageNumber - 1) * @advertisement_pageSize;
		select * from Advertisement 
		where Deleted = 1
		ORDER BY AdvertisementId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @advertisement_pageSize ROWS ONLY;
	end
go
create procedure sp_advertisement_search_pagination (@advertisement_pageNumber int,
@advertisement_pageSize int, @advertisement_Name nvarchar(100))
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@advertisement_pageNumber - 1) * @advertisement_pageSize;
		select * from Advertisement 
		where (LOWER(AdvertisementName) like '%' + LOWER(@advertisement_Name) + '%') and Deleted = 0
		ORDER BY AdvertisementId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @advertisement_pageSize ROWS ONLY;
	end
go
-- ==========================> stored procedures of news table <=======================================
create procedure sp_news_create (@news_Name NVARCHAR(100), @news_Content nvarchar(max), 
@news_NewsImage nvarchar(max), @news_PostingDate date, 
@news_PersonPostingId INT)
as
	begin
		insert into News
		values(@news_Name, @news_Content, @news_NewsImage, 
		@news_PostingDate, @news_PersonPostingId, 0);
	end;
go
create procedure sp_news_update (@news_Id int, @news_Name NVARCHAR(100), 
@news_Content nvarchar(max), @news_NewsImage nvarchar(max), @news_PostingDate date, 
@news_PersonPostingId INT, @news_Deleted bit)
as
	begin
		update News
		set NewsName = @news_Name, Content = @news_Content,
			NewsImage = @news_NewsImage,
			PostingDate = @news_PostingDate,
			PersonPostingId = @news_PersonPostingId, Deleted = @news_Deleted
		where NewsId = @news_Id
	end;
go
create procedure sp_news_delete (@news_Id int)
as
	begin
		Delete from News
		where NewsId = @news_Id
	end;
go
create procedure sp_news_all
as
	begin
		select * from News where Deleted = 0
	end;
go
create procedure sp_news_get_data_by_id (@news_Id int)
as
	begin
		select * from News where NewsId = @news_Id and Deleted = 0
	end;
go
create procedure sp_news_search (@news_Name nvarchar(100))
as
	begin
		select * from News 
		where (LOWER(NewsName) like '%' + LOWER(@news_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_news_pagination (@news_pageNumber int, @news_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@news_pageNumber - 1) * @news_pageSize;
		select * from News 
		where Deleted = 0
		ORDER BY NewsId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @news_pageSize ROWS ONLY;
	end
go
create procedure sp_news_deleted_pagination (@news_pageNumber int, @news_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@news_pageNumber - 1) * @news_pageSize;
		select * from News 
		where Deleted = 1
		ORDER BY NewsId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @news_pageSize ROWS ONLY;
	end
go
create procedure sp_news_search_pagination (@news_pageNumber int, @news_pageSize int,
@news_Name nvarchar(100))
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@news_pageNumber - 1) * @news_pageSize;
		select * from News 
		where (LOWER(NewsName) like '%' + LOWER(@news_Name) + '%') and Deleted = 0
		ORDER BY NewsId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @news_pageSize ROWS ONLY;
	end
go
-- ==========================> stored procedures of comment table <=======================================
create procedure sp_comment_create (@comment_Content nvarchar(max), 
@comment_Time datetime, @comment_SenderId INT, @comment_ProductId int)
as
	begin
		insert into Comment
		values(@comment_Content, @comment_Time, @comment_SenderId, @comment_ProductId, 0);
	end;
go
create procedure sp_comment_update (@comment_Id int, @comment_Content nvarchar(max), 
@comment_Time datetime, @comment_SenderId INT, @comment_ProductId int)
as
	begin
		update Comment
		set Content = @comment_Content,
			Time = @comment_Time,
			SenderId = @comment_SenderId,
			ProductId = @comment_ProductId
		where CommentId = @comment_Id
	end;
go
create procedure sp_comment_delete (@comment_Id int)
as
	begin
		Delete from Comment
		where CommentId = @comment_Id
	end;
go
create procedure sp_comment_all
as
	begin
		select * from Comment where Deleted = 0
	end;
go
create procedure sp_comment_get_data_by_id (@comment_Id int)
as
	begin
		select * from Comment where CommentId = @comment_Id and Deleted = 0
	end;
go
-- ==========================> stored procedures of cart table <=======================================
create procedure sp_cart_create (@cart_ProductId int, @cart_UserId int)
as
	begin
		insert into Cart
		values(@cart_ProductId, @cart_UserId);
	end;
go
create procedure sp_cart_delete (@cart_ProductId int, @cart_UserId int)
as
	begin
		Delete from Cart
		where ProductId = @cart_ProductId and UserId = @cart_UserId
	end;
go
create procedure sp_cart_search (@product_name nvarchar(100))
as
	begin
		select * FROM Cart c
		INNER JOIN Product p ON c.ProductId = p.ProductId
		WHERE (LOWER(p.ProductName) LIKE '%' + LOWER(@product_name) + '%')
	end;
go
-- ==========================> stored procedures of supplier table <=======================================
create procedure sp_supplier_create (@supplier_Name NVARCHAR(100), 
@supplier_PhoneNumber NVARCHAR(20), @supplier_Address NVARCHAR(100))
as
	begin
		insert into Supplier
		values(@supplier_Name, @supplier_PhoneNumber, @supplier_Address, 0);
	end;
go
create procedure sp_supplier_update (@supplier_Id int, @supplier_Name NVARCHAR(100), 
@supplier_PhoneNumber NVARCHAR(20), @supplier_Address NVARCHAR(100), @supplier_Deleted bit)
as
	begin
		update Supplier
		set SupplierName = @supplier_Name, 
			PhoneNumber = @supplier_PhoneNumber, Address = @supplier_Address,
			Deleted = @supplier_Deleted
		where SupplierId = @supplier_Id
	end;
go
create procedure sp_supplier_delete (@supplier_Id int)
as
	begin
		Delete from Supplier
		where SupplierId = @supplier_Id
	end;
go
create procedure sp_supplier_all
as
	begin
		select * from Supplier where Deleted = 0
	end;
go
create procedure sp_supplier_get_data_by_id (@supplier_Id int)
as
	begin
		select * from Supplier where SupplierId = @supplier_Id and Deleted = 0
	end;
go
create procedure sp_supplier_search (@supplier_Name nvarchar(100))
as
	begin
		select * from Supplier 
		where (LOWER(SupplierName) like '%' + LOWER(@supplier_Name) + '%') and Deleted = 0
	end;
go
create procedure sp_supplier_pagination (@supplier_pageNumber int, @supplier_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@supplier_pageNumber - 1) * @supplier_pageSize;
		select * from Supplier 
		where Deleted = 0
		ORDER BY SupplierId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @supplier_pageSize ROWS ONLY;
	end
go
create procedure sp_supplier_deleted_pagination (@supplier_pageNumber int, @supplier_pageSize int)
as
	begin
		declare @NumberOfIgnoredRecords int
		SET @NumberOfIgnoredRecords = (@supplier_pageNumber - 1) * @supplier_pageSize;
		select * from Supplier 
		where Deleted = 1
		ORDER BY SupplierId
		OFFSET @NumberOfIgnoredRecords ROWS
		FETCH NEXT @supplier_pageSize ROWS ONLY;
	end
go
CREATE PROCEDURE sp_supplier_search_pagination 
(
    @supplier_Name NVARCHAR(100),
    @supplier_pageNumber INT,
    @supplier_pageSize INT
)
AS
BEGIN
    DECLARE @NumberOfIgnoredRecords INT;
    SET @NumberOfIgnoredRecords = (@supplier_pageNumber - 1) * @supplier_pageSize;

    SELECT *
    FROM Supplier
    WHERE (LOWER(SupplierName) LIKE '%' + LOWER(@supplier_Name) + '%') and Deleted = 0
    ORDER BY SupplierId
    OFFSET @NumberOfIgnoredRecords ROWS
    FETCH NEXT @supplier_pageSize ROWS ONLY;
END;
go
-- ==========================> stored procedures of message table <=======================================
create procedure sp_message_create (@message_Content NVARCHAR(max), 
@message_Time datetime, @message_SenderId INT, @message_ReceiverId INT)
as
	begin
		insert into Message
		values(@message_Content, @message_Time, @message_SenderId, @message_ReceiverId, 0);
	end;
go
create procedure sp_message_delete (@message_Id int)
as
	begin
		Delete from Message
		where MessageId = @message_Id
	end;
go
create procedure sp_message_all
as
	begin
		select * from Message where Deleted = 0
	end;
go
create procedure sp_message_search (@message_Content nvarchar(100))
as
	begin
		select * from Message 
		where (LOWER(Content) like '%' + LOWER(@message_Content) + '%') and Deleted = 0
	end;
go
-- ==========================> stored procedures of importBill table <=======================================
create procedure sp_importBill_create (@importBill_SupplierId INT, 
@importBill_StaffId INT, @importBill_InputDay DATE,
@listjson_importBillDetail NVARCHAR(MAX))
as
	begin
		insert into ImportBill
		values(@importBill_SupplierId, @importBill_StaffId, @importBill_InputDay, 0);
		declare @importBill_Id int
		set @importBill_Id = SCOPE_IDENTITY();
		if(@listjson_importBillDetail is not null)
			begin
				insert into ImportBillDetail(ImportBillId, ProductId, ImportPrice, ImportQuantity)
				select @importBill_Id,
					JSON_VALUE(p.value, '$.productId') AS ProductId,
					JSON_VALUE(p.value, '$.importPrice') AS ImportPrice,
					JSON_VALUE(p.value, '$.importQuantity') AS ImportQuantity
				from openjson(@listjson_importBillDetail) as p;
		 	end;
	end;
go
create procedure sp_importBill_update (@importBill_Id int, @importBill_SupplierId INT, 
@importBill_StaffId INT, @importBill_InputDay DATE, @importBill_Deleted bit,
@listjson_importBillDetail NVARCHAR(MAX))
as
	begin
		update ImportBill
		set SupplierId = @importBill_SupplierId, StaffId = @importBill_StaffId, 
			InputDay = @importBill_InputDay, Deleted = @importBill_Deleted
		where ImportBillId = @importBill_Id
		if(@listjson_importBillDetail is not null)
			begin
				-- Insert data to temp table 
				SELECT
					JSON_VALUE(p.value, '$.importBillDetailId') as ImportBillDetailId,
					JSON_VALUE(p.value, '$.productId') as ProductId,
					JSON_VALUE(p.value, '$.importPrice') AS ImportPrice,
					JSON_VALUE(p.value, '$.importQuantity') AS ImportQuantity,
					JSON_VALUE(p.value, '$.importBillDetailStatus') AS ImportBillDetailStatus 										
				INTO #Results 
				FROM OPENJSON(@listjson_importBillDetail) AS p;

				-- Insert data to table with STATUS = 1;
				INSERT INTO ImportBillDetail(ImportBillId, ProductId, ImportPrice, ImportQuantity)
				SELECT
					@importBill_Id,
					#Results.ProductId,
					#Results.ImportPrice,
					#Results.ImportQuantity
				FROM  #Results 
				WHERE #Results.ImportBillDetailStatus = '1' 
			
				-- Update data to table with STATUS = 2
				UPDATE ImportBillDetail 
				SET
					ProductId = #Results.ProductId,
					ImportPrice = #Results.ImportPrice,
					ImportQuantity = #Results.ImportQuantity
				FROM #Results 
				WHERE ImportBillDetail.ImportBillDetailId = #Results.ImportBillDetailId AND #Results.ImportBillDetailStatus = '2';
			
			-- Delete data to table with STATUS = 3
			DELETE ImportBillDetail
			FROM ImportBillDetail INNER JOIN #Results
				ON ImportBillDetail.ImportBillDetailId = #Results.ImportBillDetailId
			WHERE #Results.ImportBillDetailStatus = '3';
			DROP TABLE #Results;
			end;
	end;
go
create procedure sp_importBill_delete (@importBill_Id int)
as
	begin
		delete from ImportBillDetail
		where ImportBillId = @importBill_Id
		Delete from ImportBill
		where ImportBillId = @importBill_Id
	end;
go
create procedure sp_importBill_all
as
	begin
		SELECT 
			ib.ImportBillId, 
			ib.SupplierId, 
			ib.StaffId, 
			ib.InputDay, 
			ib.Deleted,
			ibd.ImportBillDetailId, 
			ibd.ProductId, 
			ibd.ImportPrice, 
			ibd.ImportQuantity
		FROM 
			ImportBill ib LEFT JOIN 
			ImportBillDetail ibd ON ib.ImportBillId = ibd.ImportBillId
		WHERE ib.Deleted = 0
	end;
go
create procedure sp_importBill_get_data_by_id (@importBill_Id int)
as
	begin
		SELECT 
			ib.ImportBillId, 
			ib.SupplierId, 
			ib.StaffId, 
			ib.InputDay, 
			ib.Deleted,
			ibd.ImportBillDetailId, 
			ibd.ProductId, 
			ibd.ImportPrice, 
			ibd.ImportQuantity
		FROM 
			ImportBill ib LEFT JOIN 
			ImportBillDetail ibd ON ib.ImportBillId = ibd.ImportBillId
		WHERE ib.ImportBillId = @importBill_Id		
	end;
go
create procedure sp_importBill_search (@supplierName nvarchar(100))
as
	begin
		select * from ImportBill 
		where SupplierId = (
			select SupplierName from Supplier 
			where (LOWER(SupplierName) like '%' + LOWER(@supplierName) + '%'))
			and Deleted = 0
	end;
go
-- ==========================> sp_importBill_pagination <=======================================
create procedure sp_importBill_pagination (@importBill_pageNumber int, @importBill_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@importBill_pageNumber - 1) * @importBill_pageSize;
		SELECT 
			ib.ImportBillId, 
			ib.SupplierId, 
			ib.StaffId, 
			ib.InputDay, 
			ib.Deleted,
			ibd.ImportBillDetailId, 
			ibd.ProductId, 
			ibd.ImportPrice, 
			ibd.ImportQuantity
		FROM 
			ImportBill ib LEFT JOIN 
			ImportBillDetail ibd ON ib.ImportBillId = ibd.ImportBillId
		WHERE ib.Deleted = 0
		order by ImportBillId
		offset @NumberOfRecordsToIgnore rows
		fetch next @importBill_pageSize rows only;
	end
go
-- ==========================> sp_importBill_deleted_pagination <=======================================
create procedure sp_importBill_deleted_pagination (@importBill_pageNumber int, @importBill_pageSize int)
as
	begin
		declare @NumberOfRecordsToIgnore int
		set @NumberOfRecordsToIgnore = (@importBill_pageNumber - 1) * @importBill_pageSize;
		SELECT 
			ib.ImportBillId, 
			ib.SupplierId, 
			ib.StaffId, 
			ib.InputDay, 
			ib.Deleted,
			ibd.ImportBillDetailId, 
			ibd.ProductId, 
			ibd.ImportPrice, 
			ibd.ImportQuantity
		FROM 
			ImportBill ib LEFT JOIN 
			ImportBillDetail ibd ON ib.ImportBillId = ibd.ImportBillId
		WHERE ib.Deleted = 1
		order by ImportBillId
		offset @NumberOfRecordsToIgnore rows
		fetch next @importBill_pageSize rows only;
	end
go
-- ==========================> sp_importBill_search_pagination <=======================================
CREATE PROCEDURE sp_importBill_search_pagination 
(
    @product_Name NVARCHAR(100),
    @importBill_pageNumber INT,
    @importBill_pageSize INT
)
AS
BEGIN
    DECLARE @NumberOfIgnoredRecords INT;
    SET @NumberOfIgnoredRecords = (@importBill_pageNumber - 1) * @importBill_pageSize;

    SELECT 
        ib.ImportBillId, 
        ib.SupplierId, 
        ib.StaffId, 
        ib.InputDay, 
        ib.Deleted,
        ibd.ImportBillDetailId, 
        ibd.ProductId, 
        ibd.ImportPrice, 
        ibd.ImportQuantity
    FROM 
        ImportBill ib
    INNER JOIN 
        ImportBillDetail ibd ON ib.ImportBillId = ibd.ImportBillId
    INNER JOIN
        Product p ON ibd.ProductId = p.ProductId
    WHERE 
        ib.Deleted = 0
        AND LOWER(p.ProductName) LIKE '%' + LOWER(@product_Name) + '%'
    ORDER BY 
        ib.ImportBillId
    OFFSET @NumberOfIgnoredRecords ROWS
    FETCH NEXT @importBill_pageSize ROWS ONLY;
END;
go