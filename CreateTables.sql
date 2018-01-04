SET ANSI_NULL_DFLT_ON ON /*new columns accept NULL by default*/;
SET ANSI_NULLS ON	/*controls ISO standard for NULL interpretation*/;
SET ANSI_PADDING ON  /*adds trailing zeroes and blank spaces for CHAR and binary*/;
SET ANSI_WARNINGS ON	/*(with ARITHABORT) controls how various operations related to exceptions are handled (divide by zero)*/;
SET ARITHABORT ON	/*(with ANSI_WARNINGS) controls how various operations related to exceptions are handled (divide by zero)*/;
SET CONCAT_NULL_YIELDS_NULL ON	/*concatenating a null value with a string yields a NULL*/;
SET QUOTED_IDENTIFIER ON	/*allows you to use double quotes to imply object names*/;
SET XACT_ABORT ON	/*I make batch-transactions less insane*/;


BEGIN TRANSACTION;
use CapstoneDB

--drop table Users
--drop table Shoppers
--drop table curPlacedOrders
--create TABLE Users 
--(
--  userID INT IDENTITY  NOT NULL  PRIMARY KEY CHECK (GameId > 0),
--  firstName VARCHAR(15) NOT NULL,
--	lastName VARCHAR(15) NOT NULL, 
--	passHash varbinary not null,
--	email varchar(30) NOT NULL,
--	streetAddress VARCHAR(100) NOT NULL,
--	apartmentNumber VARCHAR(6) null, 
--	city VARCHAR(25) NOT NULL,
--	zipCode VARCHAR(7) NOT NULL,
--	stateOfResidence VARCHAR(2) NOT NULL,
--	PRIMARY KEY (userID)
--);

--create TABLE Shoppers 
--(
--	shopperID int identity not null PRIMARY KEY CHECK (GameId > 0), 
--	userID INT  NOT NULL,
--	FOREIGN KEY (userID) REFERENCES Users(userID),
--	Primary key (shopperID)
--);

--CREATE table curPlacedOrders(
--	orderID int IDENTITY PRIMARY KEY CHECK (GameId > 0),
--	customerID int not null,
--	shopperID int not null,
--	storeAddress varchar not null,
--	deliveryAddress varchar not null,
--	placedOrder varbinary not null,
--	missingItems varbinary not null,
--	foundItems varbinary not null,
--	estimatedCost smallmoney not null,
--	actualCost smallmoney not null,
--	orderRating int,
--	FOREIGN KEY (customerID) REFERENCES Users(userID),
--	FOREIGN KEY (shopperID) REFERENCES Shoppers(shopperID),
--	Primary key (orderID)
--);


--CREATE table completedOrders(
--	orderID int IDENTITY PRIMARY KEY CHECK (GameId > 0),
--	customerID int not null,
--	shopperID int not null,
--	storeAddress varchar not null,
--	deliveryAddress varchar not null,
--	placedOrder varbinary not null,
--	missingItems varbinary not null,
--	foundItems varbinary not null,
--	estimatedCost smallmoney not null,
--	actualCost smallmoney not null,
--	orderRating int,
--	FOREIGN KEY (customerID) REFERENCES Users(userID),
--	FOREIGN KEY (shopperID) REFERENCES Shoppers(shopperID),
--	Primary key (orderID)
--);

insert into dbo.Users
values ('john', 'priem', 'jpriemo@gmail.com', '356 s 1200 e', '7', 'salt lake city', '84102', 'UT')  

Print 'COMMIT'COMMIT WORK;