CREATE PROC usp_SearchForFiles(@fileExtension VARCHAR(40))
AS
	BEGIN
		SELECT Id, [Name], CONCAT(Size, 'KB') AS 'Size'
		FROM Files
		WHERE [Name] LIKE CONCAT('%', @fileExtension)
		ORDER BY Id ASC, [Name] ASC, Size DESC
	END