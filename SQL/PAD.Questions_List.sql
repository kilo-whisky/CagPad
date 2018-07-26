use CAGPAD
go

create proc PAD.Questions_List as

select
	q.QuestionId,
	q.Question
from 
	PAD.Questions q
where
	q.Active = 1