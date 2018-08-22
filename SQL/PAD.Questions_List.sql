use CAGPAD
go

alter proc PAD.Questions_List (
    @QuestionId int = null,
    @Active bit = null
) as

select
	q.QuestionId,
	q.Question,
    q.Type,
    TypeName = case q.Type
        when 'T' then 'Training'
        when 'C' then 'Cabinet'
        when 'D' then 'Defibrillator'
    end,
    q.Active,
    q.QuestionOrder
from 
	PAD.Questions q
where
    q.Active = ISNULL(@Active, q.Active)
    and q.QuestionId = ISNULL(@QuestionId, q.QuestionId)
order by
    ISNULL(q.QuestionOrder, 999 + q.QuestionId)

go

exec PAD.Questions_List