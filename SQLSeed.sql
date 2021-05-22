-- Drop Table OrderDetails
-- Drop TABLE LocationProductInventories
-- DROP TABLE Orders
-- DROP TABLE Customers
-- DROP TABLE Locations
-- Drop Table Products


CREATE TABLE Customers
(
   id   int NOT NULL IDENTITY(1,1) PRIMARY KEY,
   Name     [NVARCHAR](50)  NOT NULL,
   Email     [NVARCHAR](50)  NOT NULL,
   Password  VARCHAR(50) NOT NULL
);

CREATE TABLE Locations
(
    id   int NOT NULL IDENTITY(1,1) PRIMARY KEY,
   Name     VARCHAR(50)  NOT NULL,
   Address  VARCHAR(50)  not NULL 
);

CREATE TABLE Orders
(
   OrderId  int  NOT NULL IDENTITY(1,1) PRIMARY KEY,
   LocationId  int  NOT NULL FOREIGN KEY REFERENCES Locations(id),
   CustomerId     int  NOT NULL FOREIGN KEY REFERENCES Customers(id) ON DELETE CASCADE
);

CREATE TABLE Products
(
    ProductId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR (50) NOT NULL,
    Price FLOAT NOT NULL
)

CREATE TABLE LocationProductInventories
(
    InventoryId int IDENTITY(1,1) PRIMARY KEY,
    Quantity int NOT NULL,
    ProductId int NOT NULL FOREIGN KEY REFERENCES Products(ProductId),
    LocationId int NOT NULL FOREIGN KEY REFERENCES Locations(id) ON DELETE
)

CREATE TABLE OrderDetails
(
    OrderDetailsId int IDENTITY(1,1) PRIMARY KEY,
    Quantity int NOT NULL,
    Delivered  bit DEFAULT 0,
    ProductId int NOT NULL FOREIGN KEY REFERENCES Products(ProductId),
    OrderId int NOT NULL FOREIGN KEY REFERENCES Orders(OrderId) ON DELETE CASCADE
)

INSERT into locations VALUES ('Scranton', '5354 West Pickle St. Scranton, OH 99849')

INSERT into Products VALUES ('Dirt', 5.99), ('Rocks', 4.99), ('Dirts with Rocks in It', 7.99), ('Rocks with Some Dirt in It', 7.99)

INSERT into Customers VALUES ('Mykel', 'valadezmykel@gmail.com', 'password'), ('JoeyBob', 'dirtEmpire@gmail.com', 'fishTaco')

INSERT into Orders VALUES (1, 1)

INSERT into OrderDetails VALUES (10, 0, 1, 1)

INSERT into LocationProductInventories VALUES (44, 1, 1)
