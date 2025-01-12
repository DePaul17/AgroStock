#Procédures stockées

-- Ajout d'une nouvelle historique
DELIMITER $
CREATE PROCEDURE insertHistorical(IN p_user_id INT, IN p_action TEXT)
BEGIN
    INSERT INTO historical (user_id, action)
    VALUES (p_user_id, p_action);
END $
DELIMITER ;

-- Test
CALL insertHistorical(1, 'a ajouté un nouveau stock.');