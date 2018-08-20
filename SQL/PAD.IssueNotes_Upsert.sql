use CAGPAD
go

alter proc PAD.IssueNotes_Upsert (
    @IssueId int,
    @NoteId int = null,
    @UserId int,
    @Note varchar(max)
) as

declare @now datetime = getdate()

if not exists (select * from PAD.IssueNotes where NoteId = @NoteId)
begin
    insert into PAD.IssueNotes (IssueId, NoteBy, NoteOn, Note, Active)
    values (@IssueId, @UserId, @now, @Note, 1)
    select @NoteId = SCOPE_IDENTITY()
    return @NoteId
end

else 

begin
    update PAD.IssueNotes set
        Active = 0
    where
        NoteId = @NoteId
end