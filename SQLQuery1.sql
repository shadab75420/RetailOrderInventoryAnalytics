use RetailOrderInventoryAnalyticsDb;
GO

INSERT INTO Categories (CategoryName)
VALUES
('Electronics'),
('Groceries'),
('Clothing'),
('Furniture'),
('Books'),
('Sports'),
('Beauty'),
('Home Appliances'),
('Stationery'),
('Toys');

INSERT INTO Suppliers
(
    SupplierName,
    ContactPerson,
    PhoneNumber,
    Email
)
VALUES
('Tech Distributors Pvt Ltd','Rajesh Kumar','9876543210','sales@techdist.com'),
('FreshMart Wholesale','Ankit Sharma','9876543211','contact@freshmart.com'),
('Fashion Hub Suppliers','Priya Singh','9876543212','orders@fashionhub.com'),
('WoodCraft Furniture','Vikram Mehta','9876543213','sales@woodcraft.com'),
('Book World Distribution','Neha Verma','9876543214','support@bookworld.com'),
('SportX Supply','Arjun Patel','9876543215','sales@sportx.com'),
('Beauty Essentials','Sneha Kapoor','9876543216','support@beautyessentials.com'),
('Appliance Hub','Rohit Sharma','9876543217','sales@appliancehub.com'),
('Stationery Planet','Pooja Gupta','9876543218','info@stationeryplanet.com'),
('Toy Kingdom','Amit Jain','9876543219','contact@toykingdom.com');

INSERT INTO Products
(
    ProductName,
    CategoryId,
    SupplierId,
    UnitPrice,
    StockQuantity,
    ReorderLevel
)
VALUES
('Dell Inspiron Laptop',1,1,55000,25,5),
('Samsung Galaxy A55',1,1,32000,30,5),
('Basmati Rice 25kg',2,2,2200,50,10),
('Cooking Oil 5L',2,2,850,40,10),
('Men Formal Shirt',3,3,1200,20,5),
('Women Denim Jacket',3,3,1800,15,5),
('Office Chair',4,4,6500,10,3),
('Study Table',4,4,8500,8,2),
('Clean Code',5,5,650,35,5),
('Design Patterns',5,5,900,25,5);

INSERT INTO InventoryTransactions
(
    ProductId,
    TransactionType,
    Quantity,
    TransactionDate
)
VALUES
(1,'IN',25,GETDATE()),
(2,'IN',30,GETDATE()),
(3,'IN',50,GETDATE()),
(4,'IN',40,GETDATE()),
(5,'IN',20,GETDATE()),
(6,'IN',15,GETDATE()),
(7,'IN',10,GETDATE()),
(8,'IN',8,GETDATE()),
(9,'IN',35,GETDATE()),
(10,'IN',25,GETDATE());

INSERT INTO Orders
(
    OrderNumber,
    OrderDate,
    TotalAmount,
    Status,
    UserId
)
VALUES
('ORD-1001','2026-06-01',55000,'Completed',1),
('ORD-1002','2026-06-02',32000,'Completed',1),
('ORD-1003','2026-06-03',2200,'Shipped',1),
('ORD-1004','2026-06-04',850,'Pending',1),
('ORD-1005','2026-06-05',1200,'Completed',1),
('ORD-1006','2026-06-06',1800,'Completed',1),
('ORD-1007','2026-06-07',6500,'Shipped',1),
('ORD-1008','2026-06-08',8500,'Completed',1),
('ORD-1009','2026-06-09',650,'Pending',1),
('ORD-1010','2026-06-10',900,'Completed',1);

INSERT INTO OrderItems
(
    OrderId,
    ProductId,
    Quantity,
    UnitPrice,
    TotalPrice
)
VALUES
(1,1,1,55000,55000),
(2,2,1,32000,32000),
(3,3,1,2200,2200),
(4,4,1,850,850),
(5,5,1,1200,1200),
(6,6,1,1800,1800),
(7,7,1,6500,6500),
(8,8,1,8500,8500),
(9,9,1,650,650),
(10,10,1,900,900);

INSERT INTO SalesForecasts
(
    ProductId,
    ForecastDate,
    PredictedSales,
    CreatedAt
)
VALUES
(1,'2026-07-01',15,GETDATE()),
(2,'2026-07-01',20,GETDATE()),
(3,'2026-07-01',35,GETDATE()),
(4,'2026-07-01',28,GETDATE()),
(5,'2026-07-01',18,GETDATE()),
(6,'2026-07-01',12,GETDATE()),
(7,'2026-07-01',6,GETDATE()),
(8,'2026-07-01',5,GETDATE()),
(9,'2026-07-01',22,GETDATE()),
(10,'2026-07-01',17,GETDATE());

INSERT INTO AuditLogs
(
    Action,
    EntityName,
    UserName,
    ActionDate,
    Details
)
VALUES
('Seed','Category','Admin',GETDATE(),'Inserted sample categories'),
('Seed','Supplier','Admin',GETDATE(),'Inserted sample suppliers'),
('Seed','Product','Admin',GETDATE(),'Inserted sample products'),
('Seed','Order','Admin',GETDATE(),'Inserted sample orders');

SELECT COUNT(*) Categories FROM Categories;
SELECT COUNT(*) Suppliers FROM Suppliers;
SELECT COUNT(*) Products FROM Products;
SELECT COUNT(*) InventoryTransactions FROM InventoryTransactions;
SELECT COUNT(*) Orders FROM Orders;
SELECT COUNT(*) OrderItems FROM OrderItems;
SELECT COUNT(*) SalesForecasts FROM SalesForecasts;
SELECT COUNT(*) AuditLogs FROM AuditLogs;