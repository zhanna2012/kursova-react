-- DB

/*
sudo docker run -e "ACCEPT_EULA=Y" -e MSSQL_SA_PASSWORD=yourStrong_Password123 -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
*/

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'UniversityDB')
BEGIN
  DROP DATABASE UniversityDB
END

GO 

CREATE DATABASE UniversityDB

GO

USE UniversityDB
GO

-- TABLES
DROP TABLE IF EXISTS Cosmetics

GO

CREATE TABLE Cosmetics
(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    description NVARCHAR(MAX) NOT NULL,
    photo_url NVARCHAR(255),
    weight INT CHECK(weight > 0),
    price FLOAT CHECK(price > 0),
    product_usage NVARCHAR(MAX) NOT NULL,
    color VARCHAR(50) NOT NULL,
    country NVARCHAR(50) NOT NULL,
    ingredients NVARCHAR(255) NOT NULL
);

GO

DROP TABLE IF EXISTS Accessories

GO

CREATE TABLE Accessories
(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    description NVARCHAR(MAX) NOT NULL,
    photo_url NVARCHAR(255),
    price FLOAT CHECK(price > 0)
);

GO

DROP TABLE IF EXISTS ProductTypes

GO

CREATE TABLE ProductTypes
(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    title NVARCHAR(50) NOT NULL
);

GO

DROP TABLE IF EXISTS Brands

GO

CREATE TABLE Brands
(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    title NVARCHAR(50) NOT NULL,
    photo_url NVARCHAR(255)
);

GO


DROP TABLE IF EXISTS Products

GO

CREATE TABLE Products
(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    cosmetics_id INT,
    accessory_id INT,
    product_type_id INT NOT NULL,
    brand_id INT NOT NULL,
    amount INT CHECK(amount >= 0),

    FOREIGN KEY (cosmetics_id) REFERENCES Cosmetics(id),
    FOREIGN KEY (accessory_id) REFERENCES Accessories(id),
    FOREIGN KEY (brand_id) REFERENCES Brands(id),
    FOREIGN KEY (product_type_id) REFERENCES ProductTypes(id)
);

GO

DROP TABLE IF EXISTS Users

GO

CREATE TABLE Users
(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    photo_url NVARCHAR(255)
);

GO

DROP TABLE IF EXISTS AuthorizationTokens

GO

CREATE TABLE AuthorizationTokens
(
    user_id INT NOT NULL PRIMARY KEY,
    access_token NVARCHAR(MAX) NOT NULL,
    refresh_token NVARCHAR(MAX) NOT NULL,
    is_revoked BIT NOT NULL,
    expiration_datetime DATETIME NOT NULL,

    FOREIGN KEY (user_id) REFERENCES Users(id)
);

GO

DROP TABLE IF EXISTS Comments

GO

CREATE TABLE Comments
(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    text NVARCHAR(255) NOT NULL,
    rating TINYINT NOT NULL CHECK(rating >= 1 AND rating <= 5),
    
    FOREIGN KEY (user_id) REFERENCES Users(id),
    FOREIGN KEY (product_id) REFERENCES Products(id)
);

GO

GO

-- INDEXES

DROP INDEX IF EXISTS IDX_Cosmetics ON Cosmetics

GO

CREATE INDEX IDX_Cosmetics
ON Cosmetics(country DESC, name DESC);

GO

DROP INDEX IF EXISTS IDX_Brands ON Brands

GO

CREATE INDEX IDX_Brands
ON Brands(title DESC);

GO

-- TRIGGERS

CREATE TRIGGER UsersTrigger
ON Users
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @UserEmail NVARCHAR(100);
    
    SET @UserEmail = (SELECT TOP 1 email FROM inserted);
    
    IF @UserEmail NOT LIKE '%_@__%.__%'
    BEGIN
        RAISERROR('Invalid email provided', 16, 1)  
        ROLLBACK TRANSACTION
        RETURN
    END
    
    INSERT Users(first_name, last_name, email, password, photo_url)
    SELECT i.first_name, i.last_name, i.email, i.password, i.photo_url
    FROM inserted i
END

GO

-- FUNCTION

DROP FUNCTION IF EXISTS GetProductTypeTitle

GO

CREATE FUNCTION GetProductTypeTitle(@product_type_id INT)
RETURNS NVARCHAR(50)
AS 
BEGIN
    RETURN (SELECT TOP 1 title FROM ProductTypes WHERE id = @product_type_id);
END

GO

-- PROCEDURE
DROP PROCEDURE IF EXISTS SP_RemoveOldTokens

GO

CREATE PROCEDURE SP_RemoveOldTokens
AS
BEGIN

    BEGIN TRAN

    BEGIN TRY
        DECLARE @UserId INT;
        DECLARE @RefreshToken NVARCHAR(255);
        DECLARE @RemovedTokens TINYINT = 0;
        
        DECLARE tokens_cursor CURSOR FOR   
        SELECT aut.user_id, aut.refresh_token
        FROM AuthorizationTokens aut
        WHERE aut.is_revoked = 1 OR aut.expiration_datetime < GETUTCDATE() 
  
        OPEN tokens_cursor

        FETCH NEXT FROM tokens_cursor INTO @UserId, @RefreshToken

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @RemovedTokens = @RemovedTokens + 1;
            DELETE FROM AuthorizationTokens
            WHERE user_id = @UserId AND refresh_token = @RefreshToken; 

            FETCH NEXT FROM tokens_cursor INTO @UserId, @RefreshToken;
        END;

        COMMIT TRAN

        RETURN @RemovedTokens;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
        RETURN -1;
    END CATCH
END

GO

-- DATA SEEDING

INSERT INTO ProductTypes(title) 
VALUES
('Cosmetics'),
('Accessories');

GO

INSERT INTO Brands(title, photo_url)
VALUES
('L`Oreal Paris', 'loreal.jpeg'),
('Maybelline New York', 'Maybelline.png'),
('NYX Professional Makeup', 'Nyx.jpg');

GO

INSERT INTO Cosmetics(name, description, photo_url, color, country, weight, price, product_usage, ingredients)
VALUES
('Laboratoire Professionnel 3D Volume Mascara', 'Volume Ultra Black by Laboratoire Professionnel is a practical addition to every makeup bag, suitable for casual, classic and formal looks', 'Laboratoire.jpg', 'black', 'Italy', 5, 249, '- For best results, start application from the roots of the lashes, swirling the brush up and down along the lashes. Let the mascara dry. Apply a second or third coat.', 'Water, glyceryl stearate, stearic acid, eicosan'),
('O`BAYS Satin Lip Liner', 'A clear contour and a rich shade of lips is something that will never go out of style. In today`s beauty world, lip liner is a versatile product that can be used to reshape, contour and give lips a beautiful rich color', 'O_BAYS.jpg', 'pink', 'Ukraine', 3, 89, 'Read documentation', 'Paraffin, Glyceryl palmitate/stearate, Cera alba, Stearic acid'),
('Laboratoire Professional Eyeshadow Duo', 'Long lasting colors and silky texture. Thanks to the components included in the composition, they smooth out fine wrinkles and slow down the aging process, the skin of the eyelids looks fresh and supple', 'Laboratoire_Professional_Eyeshadow_Duo.jpg', 'black/pink', 'Italy', 15, 99, 'Apply to the upper part of the eyelids and, if necessary, emphasize the lower part of the eye', 'Lanolin oil, talc, paraffin, silica');

GO

INSERT INTO Accessories(name, description, photo_url, price)
VALUES
('Hair band velor, gray "Velour Classic"', 'Velor accessories, whether it be headbands, bags, backpacks, gloves or even hair bands, are stylish, fashionable and relevant', 'Hair_band_velor_gray.jpg', 80),
('Hair comb', 'Tangle Teezer is confidently gaining more and more fans around the world. The original design of combs, combined with functionality and excellent results in the process of application contribute to the growing popularity of this hair accessory', 'Hair_comb.jpg', 461),
('Sponge for makeup, drop', 'Droplet sponges are a hit that all fashionistas strive to acquire. Such a success of the accessory is not surprising: it allows you to apply makeup evenly, having worked through all zones. It does not leave streaks on the face and covers it with a light veil, completely eliminating the likelihood of a mask effect', 'Sponge_for_makeup_drop.jpg', 178);

GO

INSERT INTO Users(first_name, last_name, email, password, photo_url)
VALUES
-- password: root
('Sam', 'Acre', 'sam@gmail.com', '$2a$12$vtgovd4J7usdOlhLlZ3HEeiW3g2vL1UjYNpSFVRhvbtK.k.ztIlpO', '2facbe69-b1a5-475d-b61b-c635ad2657ca.png'),
-- password: admin
('John', 'Adaway', 'john@gmail.com', '$2a$12$GKa12OXu3B90DZ8ReUclMubrbIkLXeEc.Ai9teRnEDxO0TYb/CMVq', '9a64a280-02a2-4c05-90b3-7024c48d7d97.png');

GO

INSERT INTO Products(cosmetics_id, accessory_id, product_type_id, brand_id, amount)
VALUES
(1, NULL, 1, 1, 5),
(2, NULL, 1, 1, 10),
(3, NULL, 1, 2, 20),
(NULL, 1, 2, 2, 50),
(NULL, 2, 2, 3, 100),
(NULL, 3, 2, 3, 0);

GO

INSERT INTO Comments(user_id, product_id, text, rating)
VALUES
(1, 1, 'Very good', 5),
(2, 2, '50/50', 3);

GO
