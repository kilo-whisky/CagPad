use CAGPAD
go

alter proc PAD.GuardianCheck_Upsert (
    @CheckId int = null,
	@PadId int,
	@Date date = null,
	@UserId int,
    @Complete bit,
    @Notes varchar(max) = null
) as

declare @now datetime = getdate()

if not exists (select * from PAD.GuardianChecks g where g.PadId = @PadId and g.Date = @Date)
begin

	insert into PAD.GuardianChecks (PadId, Date, UserId)
	values (@PadId, cast(@now as date), @UserId)

end

else

begin

    update PAD.GuardianChecks set
      Notes = @Notes
    where
        CheckId = @CheckId

	if @Complete = 1
    begin
        update PAD.GuardianChecks set
            Completed = @now
        where
            CheckId = @CheckId
    end
    

end