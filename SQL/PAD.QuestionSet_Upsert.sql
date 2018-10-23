use CAGPAD
go

alter proc PAD.QuestionSet_Upsert (
    @SetId int = null,
    @Name varchar(50)
) as

if @SetId is null and @Name not in (select Name from PAD.QuestionSet)
begin
    insert into PAD.QuestionSet (Name)
    values (@Name)
    set @SetId = SCOPE_IDENTITY()
    return @SetId
end

if @SetId is not null
begin
    update PAD.QuestionSet set
        Name = @Name
    where
        SetId = @SetId

    return @SetId
end