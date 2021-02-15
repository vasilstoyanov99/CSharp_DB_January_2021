UPDATE RepositoriesContributors
SET ContributorId = NULL
WHERE RepositoryId = 3

DELETE RepositoriesContributors
WHERE RepositoryId = 3

DELETE Issues
WHERE RepositoryId = 3
