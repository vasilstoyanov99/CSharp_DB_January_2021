CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(30))
RETURNS INT
AS
	BEGIN 
		RETURN (SELECT COUNT(*)
				FROM Commits AS c
					JOIN Users AS u ON c.ContributorId = u.Id 
				WHERE u.Username = @username)
	END