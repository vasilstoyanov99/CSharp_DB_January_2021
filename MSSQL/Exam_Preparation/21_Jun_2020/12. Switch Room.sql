CREATE PROC usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
	BEGIN
		DECLARE @OldRoomId INT = (SELECT RoomId
									FROM Trips
								  WHERE Id = @TripId)
		DECLARE @OldRoomHotelId INT = (SELECT HotelId
									FROM Rooms
								WHERE Id = @OldRoomId)
		DECLARE @NewRoomHotelId INT = (SELECT HotelId
									FROM Rooms
								WHERE Id = @TargetRoomId)

		IF @OldRoomHotelId != @NewRoomHotelId
			THROW 50001, 'Target room is in another hotel!', 1

		DECLARE @NeededBeds INT = (SELECT COUNT(*)
									FROM Accounts AS a
									 JOIN AccountsTrips as atr ON atr.AccountId = a.Id
									  JOIN Trips AS t ON atr.TripId = t.Id
									WHERE t.Id = @TripId)
		DECLARE @NewRoomBeds INT = (SELECT Beds
										FROM Rooms
									WHERE Id = @TargetRoomId)

		IF @NeededBeds > @NewRoomBeds
			THROW 50002, 'Not enough beds in target room!', 2
		
		UPDATE Trips
		SET RoomId = @TargetRoomId
		WHERE Id = @TripId
	END
