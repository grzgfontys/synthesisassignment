-- ENUMERATIONS

-- Category Table
CREATE TABLE Category
(
	id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[name] nvarchar(20) NOT NULL UNIQUE,
	parentCategory int FOREIGN KEY REFERENCES [Category]([id]),
);

-- OrderStatus Table
CREATE TABLE OrderStatus
(
	id int NOT NULL PRIMARY KEY IDENTITY (0, 1),
	orderStatus nvarchar(20) NOT NULL UNIQUE,
);

-- PaymentMethod Table
CREATE TABLE PaymentMethod
(
	id int NOT NULL PRIMARY KEY IDENTITY (0, 1),
	paymentMethod nvarchar(20) NOT NULL UNIQUE,
);

-- CLASSES

-- Item Table
CREATE TABLE Item
(
	id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[name] nvarchar(20) NOT NULL,
	subCategory int NOT NULL FOREIGN KEY REFERENCES [Category]([id]),
	price decimal(19,4) NOT NULL,
	[unitType] nvarchar(20) NOT NULL,
	available bit NOT NULL,
	stockAmount int NOT NULL,
);

-- Address Table
CREATE TABLE Address
(
	id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
	country nvarchar(20) NOT NULL,
	city nvarchar(20) NOT NULL,
	street nvarchar(20) NOT NULL,
	postalCode nvarchar(20) NOT NULL,
);

-- Account Table
CREATE TABLE Account
(
	id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[firstname] nvarchar(20) NOT NULL,
	[lastname] nvarchar(20) NOT NULL,
	email nvarchar(50) NOT NULL UNIQUE,
	[password] nvarchar(60) NOT NULL,
	[salt] nvarchar(100) NOT NULL,
);

-- Client Table
CREATE TABLE Client
(
	id int NOT NULL FOREIGN KEY REFERENCES [Account]([id]) ON DELETE CASCADE UNIQUE,
	[username] nvarchar(20) NOT NULL UNIQUE,
	amountOfPoints int,
	addressId int FOREIGN KEY REFERENCES [Address]([id]) ON DELETE SET NULL UNIQUE,
);

-- Employee Table
CREATE TABLE Employee
(
	id int NOT NULL FOREIGN KEY REFERENCES [Account]([id]) ON DELETE CASCADE UNIQUE,
	[role] nvarchar(20) NOT NULL,
);

-- Order Table
CREATE TABLE [Order]
(
	id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
	clientId int NOT NULL FOREIGN KEY REFERENCES [Client]([id]) ON DELETE CASCADE,
	totalBonusPointsBeforeOrder int,
	orderBonusPoints int,
	orderSpentBonusPoints int,
	orderDate Date NOT NULL,
	deliveryDate Date,
	orderStatus int NOT NULL FOREIGN KEY REFERENCES [OrderStatus]([id]),
	paymentMethodId int NOT NULL FOREIGN KEY REFERENCES [PaymentMethod]([id]),
	addressId int NOT NULL FOREIGN KEY REFERENCES [Address]([id]),
);

-- LineItem Table
CREATE TABLE LineItem
(
	id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
	itemId int FOREIGN KEY REFERENCES [Item]([id]) ON DELETE SET NULL,
	orderId int FOREIGN KEY REFERENCES [Order]([id]) ON DELETE CASCADE,
	purchasePrice decimal(19,4) NOT NULL,
	amount int NOT NULL,
);

-- ShoppingCart Table
CREATE TABLE ShoppingCart
(
	clientId int NOT NULL FOREIGN KEY REFERENCES [Client]([id]),
	lineItemId int FOREIGN KEY REFERENCES [LineItem]([id]) ON DELETE CASCADE UNIQUE,
);

-- Discount Table
CREATE TABLE Discount
(
	id int NOT NULL PRIMARY KEY IDENTITY (1, 1),
	itemId int FOREIGN KEY REFERENCES [Item]([id]) ON DELETE CASCADE,
	discountTypeId int NOT NULL,
	startOfDiscount DATETIME2 NOT NULL,
	endOfDiscount DATETIME2 NOT NULL,
	amountForDiscount int NOT NULL,
	discountValue decimal(19,4) NOT NULL,
	discountMessage nvarchar(30) NOT NULL,
	--unique constrain for startdate enddate and itemId
);


-- Mock Data
INSERT INTO Category VALUES ('fruit', NULL);
INSERT INTO Category VALUES ('meat', NULL);
INSERT INTO Category VALUES ('fruit1', 1);
INSERT INTO Category VALUES ('fruit2', 1);
INSERT INTO Category VALUES ('meat1', 2);
INSERT INTO Category VALUES ('Vegetable', NULL);
INSERT INTO Category VALUES ('Household goods', NULL);
INSERT INTO Category VALUES ('Onions', 6);
INSERT INTO Category VALUES ('Chair', 7);
INSERT INTO Category VALUES ('Chicken', 2);

INSERT INTO OrderStatus VALUES ('OrderPlaced');
INSERT INTO OrderStatus VALUES ('Packed');
INSERT INTO OrderStatus VALUES ('Shipped');
INSERT INTO OrderStatus VALUES ('Delivered');

INSERT INTO PaymentMethod VALUES ('PayPal');
INSERT INTO PaymentMethod VALUES ('iDeal');
INSERT INTO PaymentMethod VALUES ('Bank Transfer');

INSERT INTO Item VALUES ('apple', 3, 10, 'kg', 1, 100);
INSERT INTO Item VALUES ('Banana', 4, 10, 'kg', 0, 1000);
INSERT INTO Item VALUES ('pear', 3, 10, 'kg', 1, 100);
INSERT INTO Item VALUES ('pork', 5, 10, 'g', 1, 1000);

INSERT INTO [Address] VALUES ('1', 'adressnumber', 'j@j', 'aa');
INSERT INTO [Address] VALUES ('2', 'adressnumber', 'j@j', 'aa');
INSERT INTO [Address] VALUES ('3', 'adressnumber', 'j@j', 'aa');
INSERT INTO [Address] VALUES ('4', 'adressnumber', 'j@j', 'aa');
INSERT INTO [Address] VALUES ('5', 'adressnumber', 'j@j', 'aa');

INSERT INTO Account VALUES ('jan', 'jan', 'j@j', '$2a$10$iMKnRelMY5LMGsU5a6lAGOC4gwHBq4FaKCVN//YDFQBI7tO9QHQji', '$2a$10$iMKnRelMY5LMGsU5a6lAGO');
INSERT INTO Account VALUES ('martin', 'sth', 'm@m', '$2a$10$iMKnRelMY5LMGsU5a6lAGOC4gwHBq4FaKCVN//YDFQBI7tO9QHQji', '$2a$10$iMKnRelMY5LMGsU5a6lAGO');
INSERT INTO Account VALUES ('employee', 'sth', 'e@e', '$2a$10$iMKnRelMY5LMGsU5a6lAGOC4gwHBq4FaKCVN//YDFQBI7tO9QHQji', '$2a$10$iMKnRelMY5LMGsU5a6lAGO');

INSERT INTO Client VALUES (1, 'jan', 100, 4);
INSERT INTO Client VALUES (2, 'martin2', NULL, 5);

INSERT INTO Employee VALUES (3, 'role');

INSERT INTO [Order] VALUES (1, 1, 10, 0, '2022-12-02', '2022-12-04', 1, 1, 1);
INSERT INTO [Order] VALUES (1, 1, 10, 0, '2022-12-03', '2022-12-05', 1, 2, 4);
INSERT INTO [Order] VALUES (1, 1, 10, 0, '2022-12-01', '2022-12-04', 1, 1, 2);
INSERT INTO [Order] VALUES (2, NULL, NULL, NULL, '2022-12-01', '2022-12-05', 1, 1, 3);
INSERT INTO [Order] VALUES (2, NULL, NULL, NULL, '2022-12-03', '2022-12-05', 1, 0, 5);

INSERT INTO LineItem VALUES (1, 1, 10, 10);
INSERT INTO LineItem VALUES (2, 1, 11, 1);
INSERT INTO LineItem VALUES (3, 1, 10, 8);
INSERT INTO LineItem VALUES (3, 2, 1, 5);
INSERT INTO LineItem VALUES (3, 2, 1, 5);
INSERT INTO LineItem VALUES (3, 3, 1, 5);
INSERT INTO LineItem VALUES (3, 3, 1, 5);
INSERT INTO LineItem VALUES (3, 3, 1, 5);
INSERT INTO LineItem VALUES (4, 4, 7, 10);
INSERT INTO LineItem VALUES (4, 4, 7, 10);
INSERT INTO LineItem VALUES (4, 5, 7, 10);
INSERT INTO LineItem VALUES (4, NULL, 7, 10);

INSERT INTO Discount VALUES (1, 1,'2022-12-10 00:00:00.0000000', '2023-02-23 00:00:00.0000000', 5, 10, 'buy 5 and -10 euro');
INSERT INTO Discount VALUES (1, 2,'2022-12-09 00:00:00.0000000', '2023-02-23 00:00:00.0000000', 3, 5, 'buy 3 and get -5%');
INSERT INTO Discount VALUES (4, 1,'2023-01-07 00:00:00.0000000', '2023-02-23 00:00:00.0000000', 6, 20, 'buy 6 and -20 euro');
INSERT INTO Discount VALUES (4, 1,'2022-12-09 00:00:00.0000000', '2023-12-23 00:00:00.0000000', 6, 20, 'buy 6 and -20 euro');
