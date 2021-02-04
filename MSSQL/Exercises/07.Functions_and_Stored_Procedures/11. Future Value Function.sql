CREATE FUNCTION ufn_CalculateFutureValue 
				(@sum DECIMAL(15,2), 
				@yearlyInterestRate FLOAT, 
				@numberOfYears INT)
RETURNS DECIMAL(15, 4)
AS
	BEGIN
		DECLARE @Result DECIMAL(15, 4);
		SET @Result = @sum * POWER((1 + @yearlyInterestRate), @numberOfYears);
		RETURN @Result;
	END

GO 

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)