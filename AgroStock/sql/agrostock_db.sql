-- Création de la base de données
CREATE DATABASE agrostock_db;
USE agrostock_db;

-- Création de la table user
CREATE TABLE user (
    id INT AUTO_INCREMENT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    address TEXT NOT NULL,
    phoneNumber VARCHAR(15) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role ENUM('purchasing_manager', 'stock_manager', 'sales_manager') NOT NULL,
    qualification VARCHAR(100) NOT NULL
);

-- Création de la table product_category
CREATE TABLE product_category (
    id INT AUTO_INCREMENT PRIMARY KEY,
    categoryName VARCHAR(100) NOT NULL,
    description TEXT
);

-- Création de la table product_subcategory
CREATE TABLE product_subcategory (
    id INT AUTO_INCREMENT PRIMARY KEY,
    subcategoryName VARCHAR(100) NOT NULL,
    description TEXT,
    categoryId INT NOT NULL,
    FOREIGN KEY (categoryId) REFERENCES product_category(id) ON DELETE CASCADE
);

-- Création de la table product
CREATE TABLE product (
    id INT AUTO_INCREMENT PRIMARY KEY,
    productName VARCHAR(100) NOT NULL,
    productionDate DATE NOT NULL,
    totalCarbonFootprint FLOAT NOT NULL,
    resourcesUsed TEXT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    subcategoryId INT NOT NULL,
    FOREIGN KEY (subcategoryId) REFERENCES product_subcategory(id) ON DELETE CASCADE
);

-- Création de la table storage_location
CREATE TABLE storage_location (
    id INT AUTO_INCREMENT PRIMARY KEY,
    locationName VARCHAR(100) NOT NULL,
    storageType VARCHAR(50) NOT NULL,
    maxCapacity INT NOT NULL
);

-- Création de la table stock
CREATE TABLE stock (
    id INT AUTO_INCREMENT PRIMARY KEY,
    productId INT NOT NULL,
    entryDate DATE NOT NULL,
    exitDate DATE,
    storageLocationId INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (productId) REFERENCES product(id) ON DELETE CASCADE,
    FOREIGN KEY (storageLocationId) REFERENCES storage_location(id) ON DELETE CASCADE
);


-- Création de la table historical
CREATE TABLE historical (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    action TEXT NOT NULL,
    action_timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE
);





------------------------------------------------------------ PROCÉDURE CATÉGORIE
-- Ajout d'une nouvelle catégorie
DELIMITER $
CREATE PROCEDURE InsertCategory(IN categoryName VARCHAR(100), IN categoryDescription TEXT)
BEGIN
    INSERT INTO product_category (CategoryName, Description)
    VALUES (categoryName, categoryDescription);
END $
DELIMITER ;

-- Lister les catégories
CREATE VIEW V_ListeCategories AS 
SELECT 
    Id AS CategoryId,
    CategoryName,
    Description
FROM product_category;

-- Modification d'une catégorie
DELIMITER $
CREATE PROCEDURE UpdateCategory(
    IN categoryId INT,
    IN newCategoryName VARCHAR(100),
    IN newCategoryDescription TEXT
)
BEGIN
    UPDATE product_category
    SET 
        CategoryName = newCategoryName,
        Description = newCategoryDescription
    WHERE Id = categoryId;
END $
DELIMITER ;

-- Suppression d'une catégorie
DELIMITER $
CREATE PROCEDURE DeleteCategory(
    IN categoryId INT
)
BEGIN
    DELETE FROM product_category 
    WHERE Id = categoryId;
END $
DELIMITER ;

------------------------------------------------------- PROCÉDURE SOUS CATÉGORIE
-- Ajout d'une nouvelle sous-catégorie
DELIMITER $
CREATE PROCEDURE InsertSubcategory(IN subcategoryName VARCHAR(100), IN description TEXT, IN categoryId INT)
BEGIN
    INSERT INTO product_subcategory (SubcategoryName, Description, CategoryId)
    VALUES (subcategoryName, description, categoryId);
END $
DELIMITER ;

-- Lister les sous-catégories
CREATE VIEW V_ListeSubcategories AS 
SELECT 
    ps.Id AS SubcategoryId,
    ps.SubcategoryName,
    ps.Description,
    pc.CategoryName AS ParentCategory
FROM product_subcategory ps
JOIN product_category pc ON ps.CategoryId = pc.Id;

-- Modification d'une sous-catégorie
DELIMITER $
CREATE PROCEDURE UpdateSubcategory(
    IN subcategoryId INT,
    IN newSubcategoryName VARCHAR(100),
    IN newDescription TEXT,
    IN newCategoryId INT
)
BEGIN
    UPDATE product_subcategory
    SET 
        SubcategoryName = newSubcategoryName,
        Description = newDescription,
        CategoryId = newCategoryId
    WHERE Id = subcategoryId;
END $
DELIMITER ;

-- Suppression d'une sous-catégorie
DELIMITER $
CREATE PROCEDURE DeleteSubcategory(
    IN subcategoryId INT
)
BEGIN
    DELETE FROM product_subcategory
    WHERE Id = subcategoryId;
END $
DELIMITER ;

-------------------------------------------------------------- PROCÉDURE PRODUIT
-- Ajout d'un nouveau produit
DELIMITER $ 
CREATE PROCEDURE insertProduct(
    IN p_productName VARCHAR(100),
    IN p_productionDate DATE,
    IN p_totalCarbonFootprint FLOAT,
    IN p_resourcesUsed TEXT,
    IN p_price DECIMAL(10, 2),
    IN p_subcategoryId INT
)
BEGIN
    -- Insertion dans la table product
    INSERT INTO product (productName, productionDate, totalCarbonFootprint, resourcesUsed, price, subcategoryId)
    VALUES (p_productName, p_productionDate, p_totalCarbonFootprint, p_resourcesUsed, p_price, p_subcategoryId);
END $ 
DELIMITER ;

-- Lister les produits
CREATE VIEW v_liste_products AS (
    SELECT 
        id AS product_id,
        productName,
        productionDate,
        totalCarbonFootprint,
        resourcesUsed,
        price,
        subcategoryId
    FROM product
);

-- Modification d'un produit
DELIMITER $ 
CREATE PROCEDURE updateProduct(
    IN p_productId INT,
    IN p_newProductName VARCHAR(100),
    IN p_newProductionDate DATE,
    IN p_newTotalCarbonFootprint FLOAT,
    IN p_newResourcesUsed TEXT,
    IN p_newPrice DECIMAL(10, 2),
    IN p_newSubcategoryId INT
)
BEGIN
    -- Mise à jour d'un produit
    UPDATE product
    SET 
        productName = p_newProductName,
        productionDate = p_newProductionDate,
        totalCarbonFootprint = p_newTotalCarbonFootprint,
        resourcesUsed = p_newResourcesUsed,
        price = p_newPrice,
        subcategoryId = p_newSubcategoryId
    WHERE id = p_productId;
END $ 
DELIMITER ;

-- Suppréssion d'un produit
DELIMITER $ 
CREATE PROCEDURE deleteProduct(IN p_productId INT)
BEGIN
    DELETE FROM product WHERE id = p_productId;
END $ 
DELIMITER ;

------------------------------------------------- PROCÉDURE EMPLACEMENT DU STOCK
-- Ajout d'un nouvel emplacement
DELIMITER $
CREATE PROCEDURE insertStorageLocation(
    IN p_location_name VARCHAR(100),
    IN p_storage_type VARCHAR(50),
    IN p_max_capacity INT
)
BEGIN
    INSERT INTO storage_location (locationName, storageType, maxCapacity)
    VALUES (p_location_name, p_storage_type, p_max_capacity);
END $
DELIMITER ;

-- Lister les emplacements
CREATE VIEW v_liste_storage_locations AS (
    SELECT 
        id AS location_id,
        locationName AS location_name,
        storageType AS storage_type,
        maxCapacity AS max_capacity
    FROM storage_location
);

-- Modification d'un emplacement
DELIMITER $
CREATE PROCEDURE updateStorageLocation(
    IN p_location_id INT,
    IN p_new_location_name VARCHAR(100),
    IN p_new_storage_type VARCHAR(50),
    IN p_new_max_capacity INT
)
BEGIN
    UPDATE storage_location
    SET 
        locationName = p_new_location_name,
        storageType = p_new_storage_type,
        maxCapacity = p_new_max_capacity
    WHERE id = p_location_id;
END $
DELIMITER ;

-- Suppréssion d'un emplacement
DELIMITER $
CREATE PROCEDURE deleteStorageLocation(IN p_location_id INT)
BEGIN
    DELETE FROM storage_location WHERE id = p_location_id;
END $
DELIMITER ;

---------------------------------------------------------------- PROCÉDURE STOCK
-- Ajout d'un nouveau stock
DELIMITER $
CREATE PROCEDURE insertStock(
    IN p_product_id INT,
    IN p_entry_date DATE,
    IN p_exit_date DATE,
    IN p_storage_location_id INT,
    IN p_quantity INT
)
BEGIN
    INSERT INTO stock (productId, entryDate, exitDate, storageLocationId, quantity)
    VALUES (p_product_id, p_entry_date, p_exit_date, p_storage_location_id, p_quantity);
END $
DELIMITER ;

-- Lister les stocks
CREATE VIEW v_liste_stocks AS (
    SELECT 
        s.id AS stock_id,
        p.productName,
        s.entryDate,
        s.exitDate,
        sl.locationName,
        s.quantity
    FROM stock s
    JOIN product p ON s.productId = p.id
    JOIN storage_location sl ON s.storageLocationId = sl.id
);

-- Modification d'un stock
DELIMITER $
CREATE PROCEDURE updateStock(
    IN p_stock_id INT,
    IN p_new_entry_date DATE,
    IN p_new_exit_date DATE,
    IN p_new_storage_location_id INT,
    IN p_new_quantity INT
)
BEGIN
    UPDATE stock
    SET 
        entryDate = p_new_entry_date,
        exitDate = p_new_exit_date,
        storageLocationId = p_new_storage_location_id,
        quantity = p_new_quantity
    WHERE id = p_stock_id;
END $
DELIMITER ;

-- Suppression d'un stock
DELIMITER $
CREATE PROCEDURE deleteStock(IN p_stock_id INT)
BEGIN
    DELETE FROM stock WHERE id = p_stock_id;
END $
DELIMITER ;

----------------------------------------------------------- PROCÉDURE HISTORICAL
-- Ajout d'une nouvelle historique
DELIMITER $
CREATE PROCEDURE insertHistorical(IN p_user_id INT, IN p_action TEXT)
BEGIN
    INSERT INTO historical (user_id, action)
    VALUES (p_user_id, p_action);
END $
DELIMITER ;