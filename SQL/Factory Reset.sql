delete from PAD.Issues
delete from PAD.Answers
delete from PAD.GuardianChecks

DBCC CHECKIDENT ('PAD.Issues', RESEED, 0);
GO
DBCC CHECKIDENT ('PAD.Answers',RESEED, 0);
GO
DBCC CHECKIDENT ('PAD.GuardianChecks', RESEED, 0);
GO