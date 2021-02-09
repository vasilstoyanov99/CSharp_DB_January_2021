CREATE FUNCTION udf_CalculateTickets
			(@origin VARCHAR(50), @destination VARCHAR(50), @peopleCount INT)
RETURNS VARCHAR(MAX)
AS
	BEGIN
		IF @peopleCount <= 0
			RETURN 'Invalid people count!'
		IF ( SELECT COUNT(*)
				 FROM Flights AS f
					JOIN Tickets AS t ON f.Id = t.FlightId
				 WHERE f.Origin = @origin AND f.Destination = @destination) < 1
	      RETURN 'Invalid flight!'

		DECLARE @TotalPrice DECIMAL(12, 2)
		DECLARE @PricePerPerson DECIMAL(12, 2)
		SET @PricePerPerson = ( SELECT t.Price
							    FROM Flights AS f
								  JOIN Tickets AS t ON f.Id = t.FlightId
							    WHERE f.Origin = @origin AND f.Destination = @destination)

	    SET @TotalPrice = @PricePerPerson * @peopleCount
		RETURN CONCAT('Total price ', @TotalPrice)
	END