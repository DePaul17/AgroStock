-- Procédures stockées

#CRUD table product_categrory

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

-- Test
CALL InsertCategory('Fruit', 'Ce sont des fruits mdr!');
SELECT * FROM V_ListeCategories;
CALL UpdateCategory(1, 'Légume', 'Désormais des légumes');
CALL DeleteCategory(1);
