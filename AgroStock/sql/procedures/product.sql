#Procédures stockées

#CRUD table product

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

-- Test
CALL insertProduct('Citron', '2025-01-01', 10.5, 'Eau, Énergie', 1, 1);
SELECT * FROM v_liste_products;
CALL updateProduct(1, 'Orange', '2025-01-02', 9.8, 'Eau, Énergie, Terre', 2, 1);
CALL deleteProduct(1);