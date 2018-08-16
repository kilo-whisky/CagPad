use CAGPAD
go

alter proc PAD.IssueNotes_List (
    @IssueId int,
    @NoteId int = null
) as

select
    i.NoteId,
    i.IssueId,
    NoteBy = u.FullName,
    i.NoteOn,
    i.Note
from
    PAD.IssueNotes i
    join Core.Users u on i.NoteBy = u.UserId
where
    i.IssueId = @IssueId
    and i.NoteId = ISNULL(@NoteId, i.NoteId)
    and i.Active = 1

go

exec PAD.IssueNotes_List 1