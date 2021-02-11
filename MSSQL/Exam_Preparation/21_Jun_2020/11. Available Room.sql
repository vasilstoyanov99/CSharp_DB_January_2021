CREATE FUNCTION udf_GetAvailableRoom 
			(@HotelId INT, @Date DATE, @People INT)
RETURNS NVARCHAR(MAX)
AS
	BEGIN
		IF ( SELECT TOP(1) COUNT(*)
				FROM Rooms
			WHERE HotelId = @HotelId AND Beds >= @People
			GROUP BY Price
				ORDER BY Price DESC ) < 1
		   RETURN 'No rooms available'

		DECLARE @RoomId INT = ( SELECT TOP(1) Id
									FROM Rooms
								WHERE HotelId = @HotelId AND Beds >= @People
								ORDER BY Price DESC )

		IF (SELECT COUNT(*) 
				FROM Trips AS t
			LEFT JOIN Rooms AS r ON t.RoomId = r.Id
				WHERE r.Id = @RoomId
				AND @Date NOT BETWEEN ArrivalDate AND ReturnDate
				AND CancelDate IS NULL) < 1
			RETURN 'No rooms available'

		DECLARE @RoomPrice DECIMAL(12, 2) = (SELECT Price
												FROM Rooms
											 WHERE Id = @RoomId)
		DECLARE @RoomType NVARCHAR(20) = (SELECT [Type]
												FROM Rooms
										  WHERE Id = @RoomId)
		DECLARE @HotelBaseRate DECIMAL(12, 2) = (SELECT BaseRate
													FROM Hotels
												 WHERE Id = @HotelId) 
		DECLARE @Beds INT = (SELECT Beds
								FROM Rooms
							 WHERE Id = @RoomId)

		DECLARE @TotalCost DECIMAL(12, 2) = (@HotelBaseRate + @RoomPrice) * @People

		RETURN CONCAT('Room ', @RoomId, ': ', @RoomType, ' (', @Beds, ' beds) - $', @TotalCost)
	END