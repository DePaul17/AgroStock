#Procédures stockées

#CRUD table stock

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

-- Test
CALL insertStock(1, '2025-01-12', '2025-01-20', 1, 1000);
SELECT * FROM v_liste_stocks;
CALL updateStock(1, '2025-01-13', '2025-01-21', 2, 1500);
CALL deleteStock(1);
