CREATE TRIGGER on_new_warrning
    ON GAMEWEB.dbo.Ostrzezenia
    FOR DELETE, INSERT
    AS
    BEGIN
    SET NOCOUNT ON
	DECLARE @UserID integer
	DECLARE @WarrningCount integer

	SET @UserID = (SELECT OstrzeganyID FROM inserted)
	SET @WarrningCount = (SELECT count(*)
						  FROM GAMEWEB.dbo.Uzytkownicy
						  WHERE UzytkownikID = @UserID)

	Update GAMEWEB.dbo.Uzytkownicy
	SET BlokadaKonta=1
	WHERE UzytkownikID = @UserID AND @WarrningCount>3
    END
