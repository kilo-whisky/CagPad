use CAGPAD
go

alter proc PAD.Questions_List (
    @QuestionId int = null
) as

select
	q.QuestionId,
	q.Question,
    q.Type,
    q.Active,
    q.QuestionOrder
from 
	PAD.Questions q
where
	q.Active = 1
    and q.QuestionId = ISNULL(@QuestionId, q.QuestionId)
order by
    ISNULL(q.QuestionOrder, 999 + q.QuestionId)

go

exec PAD.Questions_List