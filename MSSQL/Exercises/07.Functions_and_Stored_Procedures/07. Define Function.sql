CREATE OR ALTER FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX))
RETURNS BIT 
AS
	BEGIN
		DECLARE @wordLowerCase VARCHAR(MAX) = LOWER(@word);
		DECLARE @wordLenght INT = LEN(@word);
		DECLARE @substringStartPosition INT = 1;
		WHILE (@substringStartPosition <=  @wordLenght)
		 BEGIN
		 DECLARE @char VARCHAR(1) = SUBSTRING(@word, @substringStartPosition, 1) 
			IF (CHARINDEX(@char, @wordLowerCase) = 0)
				RETURN 0
			ELSE
			SET @substringStartPosition += 1
		 END
        RETURN 0;
	END
GO

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')
SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves')
SELECT dbo.ufn_IsWordComprised('bobr', 'Rob')
SELECT dbo.ufn_IsWordComprised('pppp', 'Guy')
GO
