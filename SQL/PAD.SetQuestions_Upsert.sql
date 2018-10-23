use CAGPAD
go

create proc PAD.SetQuestions_Upsert (
    @SetId int,
    @QuestionId int,
    @AddRemove char(1)
) as

if @AddRemove = 'A'
begin
    insert into PAD.SetQuestions (SetId, QuestionId)
    values (@SetId, @QuestionId)
end

if @AddRemove = 'R'
begin
    delete from PAD.SetQuestions
    where
        SetId = @SetId
        and QuestionId = @QuestionId
end