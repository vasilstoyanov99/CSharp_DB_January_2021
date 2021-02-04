CREATE FUNCTION ufn_CalculateFutureValue 
				(@sum DECIMAL, 
				@yearlyInterestRate FLOAT, 
				@numberOfYears INT)
RETURNS DECIMAL(8, 4)
AS
	BEGIN
		DECLARE @Result DECIMAL(8, 4);
		SET @Result = @sum * POWER((1 + @yearlyInterestRate), @numberOfYears);
		RETURN @Result;
	END

GO 

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)