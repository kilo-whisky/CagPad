use CAGPAD
go

alter proc PAD.Questions_List (
    @QuestionId int = null,
    @Active bit = null,
    @PadId int = null
) as

select distinct
    q.QuestionId,
	q.Question,
    q.Type,
    TypeName = case q.Type
        when 'T' then 'Training'
        when 'C' then 'Cabinet'
        when 'D' then 'Defibrillator'
    end,
    q.Active,
    ISNULL(q.QuestionOrder, 999 + q.QuestionId)
from 
    PAD.Questions q 
    join PAD.SetQuestions sq on q.QuestionId = sq.QuestionId
    join PAD.PADSites p on sq.SetId = p.QuestionSet
where
    p.PadId = isnull(@PadId, p.PadId)
order by
    ISNULL(q.QuestionOrder, 999 + q.QuestionId)

go

exec PAD.Questions_List null, null, 5