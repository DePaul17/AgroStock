#Procédures stockées

#CRUD table product_subcategory

-- Ajout d'une nouvelle sous-catégorie
DELIMITER $
CREATE PROCEDURE insertSubcategory(IN subcategoryName VARCHAR(100), IN description TEXT, IN categoryId INT)
BEGIN
    INSERT INTO product_subcategory (SubcategoryName, Description, CategoryId)
    VALUES (subcategoryName, description, categoryId);
END $
DELIMITER ;

-- Lister les sous-catégories
CREATE VIEW v_ListeSubcategories AS 
SELECT 
    ps.Id AS SubcategoryId,
    ps.SubcategoryName,
    ps.Description,
    pc.CategoryName AS ParentCategory
FROM product_subcategory ps
JOIN product_category pc ON ps.CategoryId = pc.Id;

-- Modification d'une sous-catégorie
DELIMITER $
CREATE PROCEDURE updateSubcategory(
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
CREATE PROCEDURE deleteSubcategory(
    IN subcategoryId INT
)
BEGIN
    DELETE FROM product_subcategory
    WHERE Id = subcategoryId;
END $
DELIMITER ;

-- Test
CALL InsertSubcategory('Agrumes', 'Les agrumes sont des fruits', 1);
SELECT * FROM V_ListeSubcategories;
CALL UpdateSubcategory(1, 'Légumes fruits', 'Les concombres sont des légumes-fruits', 2);
CALL DeleteSubcategory(1);