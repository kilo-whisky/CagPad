use CAGPAD
go

create proc PAD.QuestionSet_List (
    @SetId int = null
) as

select
    qs.SetId,
    qs.Name
from
    PAD.QuestionSet qs
where
    qs.SetId = isnull(@SetId, qs.SetId)

go

exec PAD.QuestionSet_List