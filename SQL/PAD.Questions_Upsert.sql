use CAGPAD
go

create proc PAD.Questions_Upsert(
    @QuestionId int = null,
    @Question varchar(1000),
    @Type char(1),
    @Active bit,
    @QuestionOrder int = null
) as

if not exists (select * from PAD.Questions where QuestionId = @QuestionId)
begin
    insert into PAD.Questions (Question, Type, Active, QuestionOrder)
    values (@Question, @Type, @Active, @QuestionOrder)
    select @QuestionId = SCOPE_IDENTITY()
    return @QuestionId
end

else 

begin
    update PAD.Questions set
        Question = @Question,
        Type = @Type,
        Active = @Active,
        QuestionOrder = @QuestionOrder
    where
        QuestionId = @QuestionId
end