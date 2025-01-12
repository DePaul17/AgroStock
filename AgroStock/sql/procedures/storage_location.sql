#Procédures stockées

#CRUD table storage_location

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

-- Test
CALL insertStorageLocation('Entrepôt Saint Quentin', 'Climatisé', 10000);
SELECT * FROM v_liste_storage_locations;
CALL updateStorageLocation(1, 'Entrepôt Paris', 'climatisé', 10500);
CALL deleteStorageLocation(1);
