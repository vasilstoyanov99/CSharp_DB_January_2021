CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX))
RETURNS BIT 
AS
	BEGIN
		DECLARE @wordLenght INT = LEN(@word)
		DECLARE @substringStartPosition INT = 1
		WHILE (@substringStartPosition <=  @wordLenght)
		 BEGIN
		 DECLARE @char VARCHAR(2) = SUBSTRING(@word, @substringStartPosition, 1) 
			IF (CHARINDEX(@char, @setOfLetters) > 0)
				SET @substringStartPosition += 1
			ELSE
			RETURN 0 
		 END
        RETURN 1
	END
GO

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')
SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves')
SELECT dbo.ufn_IsWordComprised('bobr', 'Rob')
SELECT dbo.ufn_IsWordComprised('pppp', 'Guy')
GO
