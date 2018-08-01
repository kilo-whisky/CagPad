use CAGPAD
go

alter proc PAD.Answers_List (
    @CheckId int
) as

select
    a.CheckId,
    q.QuestionId,
    q.Question,
    a.AnswerId,
    a.Answer,
    i.IssueId,
    i.Description,
    i.Severity,
    i.Image,
    i.Resolved
from
    PAD.Questions q
    join PAD.Answers a on q.QuestionId = a.QuestionId
    left join PAD.Issues i on a.AnswerId = i.AnswerId
where
    a.CheckId = @CheckId
    and q.Active = 1

go

exec PAD.Answers_List 2