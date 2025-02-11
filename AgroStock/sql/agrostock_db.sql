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
    role ENUM('purchasing_manager', 'stock_manager', 'sales_manager', 'customer') NOT NULL,
    qualification VARCHAR(100) NOT NULL
);

-- Création de la table product_category
CREATE TABLE product_category (
    id INT AUTO_INCREMENT PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL,
    description TEXT
);

-- Création de la table product_subcategory
CREATE TABLE product_subcategory (
    id INT AUTO_INCREMENT PRIMARY KEY,
    subcategory_name VARCHAR(100) NOT NULL,
    description TEXT,
    category_id INT NOT NULL,
    FOREIGN KEY (category_id) REFERENCES product_category(id) ON DELETE CASCADE
);

-- Création de la table product
CREATE TABLE product (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    production_date DATE NOT NULL,
    total_carbon_footprint DECIMAL(10,2) NOT NULL,
    resources_used TEXT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    subcategory_id INT NOT NULL,
    FOREIGN KEY (subcategory_id) REFERENCES product_subcategory(id) ON DELETE CASCADE
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
    product_id INT NOT NULL,
    entryDate DATE NOT NULL,
    exitDate DATE,
    storage_location_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES product(id) ON DELETE CASCADE,
    FOREIGN KEY (storage_location_id) REFERENCES storage_location(id) ON DELETE CASCADE
);


-- Création de la table historical
CREATE TABLE historical (
    id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    action TEXT NOT NULL,
    action_timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE
);



-- Création de la table customer_order (commande client)
CREATE TABLE customer_order (
    id INT AUTO_INCREMENT PRIMARY KEY,
    orderDate DATE NOT NULL,
    customer_id INT NOT NULL,
    totalAmount DECIMAL(10,2) NOT NULL,
    status ENUM('pending', 'completed', 'cancelled') NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES user(id) ON DELETE CASCADE
);

-- Création de la table order_item (lignes de commande)
CREATE TABLE order_item (
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES customer_order(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES product(id) ON DELETE CASCADE
);

-- Création de la table delivery (livraison)
CREATE TABLE delivery (
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    deliveryDate DATE NOT NULL,
    deliveryAddress TEXT NOT NULL,
    deliveryStatus ENUM('pending', 'shipped', 'delivered', 'returned') NOT NULL,
    FOREIGN KEY (order_id) REFERENCES customer_order(id) ON DELETE CASCADE
);


------------------------------------------------------------ PROCÉDURE CATÉGORIE
-- Ajout d'une nouvelle catégorie
DELIMITER $$

CREATE PROCEDURE insertCategory(
    IN p_category_name VARCHAR(100), 
    IN p_category_description TEXT
)
BEGIN
    INSERT INTO product_category (category_name, description)
    VALUES (p_category_name, p_category_description);
END $$

DELIMITER ;

-- Lister les catégories
CREATE VIEW v_liste_categories AS 
SELECT 
    id AS category_id,
    category_name,
    description
FROM product_category;

-- Modification d'une catégorie
DELIMITER $$

CREATE PROCEDURE updateCategory(
    IN p_category_id INT,
    IN p_new_category_name VARCHAR(100),
    IN p_new_category_description TEXT
)
BEGIN
    UPDATE product_category
    SET 
        category_name = p_new_category_name,
        description = p_new_category_description
    WHERE id = p_category_id;
END $$

DELIMITER ;

-- Suppression d'une catégorie
DELIMITER $$

CREATE PROCEDURE deleteCategory(
    IN p_category_id INT
)
BEGIN
    DELETE FROM product_category 
    WHERE id = p_category_id;
END $$

DELIMITER ;

------------------------------------------------------- PROCÉDURE SOUS CATÉGORIE
-- Ajout d'une nouvelle sous-catégorie
DELIMITER $$

CREATE PROCEDURE insertSubcategory(
    IN p_subcategory_name VARCHAR(100), 
    IN p_description TEXT, 
    IN p_category_id INT
)
BEGIN
    INSERT INTO product_subcategory (subcategory_name, description, category_id)
    VALUES (p_subcategory_name, p_description, p_category_id);
END $$

DELIMITER ;

-- Lister les sous-catégories
CREATE VIEW v_liste_subcategories AS 
SELECT 
    ps.id AS subcategory_id,
    ps.subcategory_name,
    ps.description,
    pc.category_name AS parent_category
FROM product_subcategory ps
JOIN product_category pc ON ps.category_id = pc.id;

-- Modification d'une sous-catégorie
DELIMITER $$

CREATE PROCEDURE updateSubcategory(
    IN p_subcategory_id INT,
    IN p_new_subcategory_name VARCHAR(100),
    IN p_new_description TEXT,
    IN p_new_category_id INT
)
BEGIN
    UPDATE product_subcategory
    SET 
        subcategory_name = p_new_subcategory_name,
        description = p_new_description,
        category_id = p_new_category_id
    WHERE id = p_subcategory_id;
END $$

DELIMITER ;

-- Suppression d'une sous-catégorie
DELIMITER $$

CREATE PROCEDURE deleteSubcategory(
    IN p_subcategory_id INT
)
BEGIN
    DELETE FROM product_subcategory
    WHERE id = p_subcategory_id;
END $$

DELIMITER ;

-------------------------------------------------------------- PROCÉDURE PRODUIT
-- Ajout d'un nouveau produit
DELIMITER $$ 

CREATE PROCEDURE insertProduct(
    IN p_product_name VARCHAR(100),
    IN p_production_date DATE,
    IN p_total_carbon_footprint DECIMAL(10,2),
    IN p_resources_used TEXT,
    IN p_price DECIMAL(10,2),
    IN p_subcategory_id INT
)
BEGIN
    INSERT INTO product (
        product_name, 
        production_date, 
        total_carbon_footprint, 
        resources_used, 
        price, 
        subcategory_id
    )
    VALUES (
        p_product_name, 
        p_production_date, 
        p_total_carbon_footprint, 
        p_resources_used, 
        p_price, 
        p_subcategory_id
    );
END $$ 

DELIMITER ;

-- Lister les produits
CREATE VIEW v_liste_products AS 
SELECT 
    p.id AS product_id,
    p.product_name,
    p.production_date,
    p.total_carbon_footprint,
    p.resources_used,
    p.price,
    ps.subcategory_name AS subcategory
FROM product p
JOIN product_subcategory ps ON p.subcategory_id = ps.id;

-- Modification d'un produit
DELIMITER $$ 

CREATE PROCEDURE updateProduct(
    IN p_product_id INT,
    IN p_new_product_name VARCHAR(100),
    IN p_new_production_date DATE,
    IN p_new_total_carbon_footprint DECIMAL(10,2),
    IN p_new_resources_used TEXT,
    IN p_new_price DECIMAL(10,2),
    IN p_new_subcategory_id INT
)
BEGIN
    UPDATE product
    SET 
        product_name = p_new_product_name,
        production_date = p_new_production_date,
        total_carbon_footprint = p_new_total_carbon_footprint,
        resources_used = p_new_resources_used,
        price = p_new_price,
        subcategory_id = p_new_subcategory_id
    WHERE id = p_product_id;
END $$ 

DELIMITER ;

-- Suppression d'un produit
DELIMITER $$ 

CREATE PROCEDURE deleteProduct(IN p_product_id INT)
BEGIN
    DELETE FROM product WHERE id = p_product_id;
END $$ 

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
    INSERT INTO stock (product_id, entryDate, exitDate, storage_location_id, quantity)
    VALUES (p_product_id, p_entry_date, p_exit_date, p_storage_location_id, p_quantity);
END $
DELIMITER ;

-- Lister les stocks
CREATE VIEW v_liste_stocks AS (
    SELECT 
        s.id AS stock_id,
        p.product_name,
        s.entryDate,
        s.exitDate,
        sl.locationName,
        s.quantity
    FROM stock s
    JOIN product p ON s.product_id = p.id
    JOIN storage_location sl ON s.storage_location_id = sl.id
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
        storage_location_id = p_new_storage_location_id,
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


------------------------------------------------------------ PROCÉDURE GESTION DE COMMANDES
-- Ajout d'une nouvelle commande
DELIMITER $
CREATE PROCEDURE insertOrder(
    IN p_orderDate DATE,
    IN p_customerId INT,
    IN p_totalAmount DECIMAL(10,2),
    IN p_status ENUM('pending', 'completed', 'cancelled')
)
BEGIN
    INSERT INTO customer_order (orderDate, customer_id, totalAmount, status)
    VALUES (p_orderDate, p_customerId, p_totalAmount, p_status);
END $
DELIMITER ;

-- Lister les commandes
CREATE VIEW v_liste_orders AS 
SELECT 
    o.id AS order_id,
    o.orderDate,
    u.firstName,
    u.lastName,
    o.totalAmount,
    o.status
FROM customer_order o
JOIN user u ON o.customer_id = u.id;

-- Modification d'une commande
DELIMITER $
CREATE PROCEDURE updateOrder(
    IN p_orderId INT,
    IN p_newOrderDate DATE,
    IN p_newCustomerId INT,
    IN p_newTotalAmount DECIMAL(10,2),
    IN p_newStatus ENUM('pending', 'completed', 'cancelled')
)
BEGIN
    UPDATE customer_order
    SET 
        orderDate = p_newOrderDate,
        customer_id = p_newCustomerId,
        totalAmount = p_newTotalAmount,
        status = p_newStatus
    WHERE id = p_orderId;
END $
DELIMITER ;

-- Suppression d'une commande
DELIMITER $
CREATE PROCEDURE deleteOrder(IN p_orderId INT)
BEGIN
    DELETE FROM customer_order WHERE id = p_orderId;
END $
DELIMITER ;

-------------------------------------------------------- PROCÉDURE LIVRAISON --------------------------------
-- Ajout d'une nouvelle livraison
DELIMITER $
CREATE PROCEDURE insertDelivery(
    IN p_orderId INT,
    IN p_deliveryDate DATE,
    IN p_deliveryAddress TEXT,
    IN p_deliveryStatus ENUM('pending', 'shipped', 'delivered', 'returned')
)
BEGIN
    INSERT INTO delivery (order_id, deliveryDate, deliveryAddress, deliveryStatus)
    VALUES (p_orderId, p_deliveryDate, p_deliveryAddress, p_deliveryStatus);
END $
DELIMITER ;

-- Lister les livraisons
CREATE VIEW v_liste_deliveries AS 
SELECT 
    d.id AS delivery_id,
    o.id AS order_id,
    d.deliveryDate,
    d.deliveryAddress,
    d.deliveryStatus
FROM delivery d
JOIN customer_order o ON d.order_id = o.id;

-- Modification d'une livraison
DELIMITER $
CREATE PROCEDURE updateDelivery(
    IN p_deliveryId INT,
    IN p_newOrderId INT,
    IN p_newDeliveryDate DATE,
    IN p_newDeliveryAddress TEXT,
    IN p_newDeliveryStatus ENUM('pending', 'shipped', 'delivered', 'returned')
)
BEGIN
    UPDATE delivery
    SET 
        order_id = p_newOrderId,
        deliveryDate = p_newDeliveryDate,
        deliveryAddress = p_newDeliveryAddress,
        deliveryStatus = p_newDeliveryStatus
    WHERE id = p_deliveryId;
END $
DELIMITER ;

-- Suppression d'une livraison
DELIMITER $
CREATE PROCEDURE deleteDelivery(IN p_deliveryId INT)
BEGIN
    DELETE FROM delivery WHERE id = p_deliveryId;
END $
DELIMITER ;


-- Insertion des utilisateurs
INSERT INTO user (firstName, lastName, address, phoneNumber, email, password, role, qualification) VALUES
('Alice', 'Martin', '12 rue des Champs, Paris', '0601020304', 'alice.martin@email.com', 'hashedpassword', 'purchasing_manager', 'MBA Supply Chain'),
('Bob', 'Dupont', '45 avenue de la République, Lyon', '0612345678', 'bob.dupont@email.com', 'hashedpassword', 'stock_manager', 'Logistique avancée'),
('Charlie', 'Durand', '78 boulevard Haussmann, Paris', '0623456789', 'charlie.durand@email.com', 'hashedpassword', 'sales_manager', 'Commerce international'),
('David', 'Moreau', '90 quai de la Seine, Bordeaux', '0634567890', 'david.moreau@email.com', 'hashedpassword', 'customer', 'Particulier');

-- Insertion des catégories de produits
INSERT INTO product_category (categoryName, description) VALUES
('fruit', 'Tous types de fruits frais'),
('légume', 'Légumes de saison et de conservation'),
('produit_complementaire', 'Autres produits complémentaires issus de la ferme');

-- Insertion des sous-catégories de produits
INSERT INTO product_subcategory (subcategoryName, description, categoryId) VALUES
('pomme', 'Variétés de pommes bio', 1),
('tomate', 'Tomates bio variées', 2),
('carotte', 'Carottes issues de l`agriculture durable', 2),
('lait', 'Lait frais et pasteurisé', 3),
('fromage', 'Fromages artisanaux', 3),
('confiture', 'Confitures maison', 3);

-- Insertion des produits
INSERT INTO product (productName, productionDate, totalCarbonFootprint, resourcesUsed, price, subcategoryId) VALUES
('Golden Bio', '2025-02-01', 1.2, 'Eau, soleil, engrais naturel', 2.50, 1),
('Tomate Coeur de Boeuf', '2025-02-02', 0.9, 'Irrigation modérée', 3.00, 2),
('Carotte des sables', '2025-02-03', 0.7, 'Terroir sableux riche en minéraux', 1.80, 3),
('Lait de ferme', '2025-02-01', 2.5, 'Alimentation des vaches bio', 1.20, 4),
('Camembert fermier', '2025-02-04', 3.2, 'Lait cru, affinage traditionnel', 4.50, 5),
('Confiture de fraise', '2025-02-05', 1.5, 'Fraises bio, sucre de canne', 5.00, 6);

-- Insertion des emplacements de stockage
INSERT INTO storage_location (locationName, storageType, maxCapacity) VALUES
('Chambre froide', 'Réfrigéré', 1000),
('Stock fruits & légumes', 'Température ambiante', 500),
('Cave affinage', 'Affinage', 300);

-- Insertion des stocks
INSERT INTO stock (productId, entryDate, exitDate, storageLocationId, quantity) VALUES
(1, '2025-02-06', NULL, 2, 150),
(2, '2025-02-06', NULL, 2, 200),
(4, '2025-02-06', NULL, 1, 100),
(5, '2025-02-06', NULL, 3, 50);

-- Insertion de l'historique
INSERT INTO historical (user_id, action) VALUES
(1, 'Ajout d`un nouveau lot de pommes'),
(2, 'Mise à jour des quantités de tomates');

-- Insertion des commandes clients
INSERT INTO customer_order (orderDate, customerId, totalAmount, status) VALUES
('2025-02-07', 4, 25.50, 'pending');

-- Insertion des articles de commande
INSERT INTO order_item (orderId, productId, quantity, price) VALUES
(1, 1, 5, 12.50),
(1, 2, 3, 9.00),
(1, 6, 1, 5.00);

-- Insertion des livraisons
INSERT INTO delivery (orderId, deliveryDate, deliveryAddress, deliveryStatus) VALUES
(1, '2025-02-08', '90 quai de la Seine, Bordeaux', 'pending');


