SELECT SUM(g.DepositAmount - h.DepositAmount) AS 'SumDifference'
FROM WizzardDeposits AS h
JOIN WizzardDeposits AS g ON g.Id + 1 = h.Id