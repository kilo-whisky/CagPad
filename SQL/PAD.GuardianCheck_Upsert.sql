use CAGPAD
go

alter proc PAD.GuardianCheck_Upsert (
	@PadId int,
	@Date date = null,
	@UserId int,
	@CabinetOpenLock bit,
	@CabinetBatteriesOk bit,
	@CabinetLightWork bit,
	@NothingTouchingHeater bit,
	@AEDOk bit,
	@AEDSilent bit,
	@ResusKit bit
) as

if not exists (select * from PAD.GuardianChecks g where g.PadId = @PadId and g.Date = @Date)
begin

	insert into PAD.GuardianChecks (PadId, Date, UserId, CabinetOpenLock, CabinetBatteriesOk, CabinetLightWork, NothingTouchingHeater, AEDok, AEDSilent, ResuscitationKit)
	values (@PadId, @Date, @UserId, @CabinetOpenLock, @CabinetBatteriesOk, @CabinetLightWork, @NothingTouchingHeater, @AEDok, @AEDSilent, @ResusKit)

    create table #issues (IssueName varchar(50))
    insert into #issues
    exec PAD.GuardianCheck_Issues @PadId, @Date

    --return 
    select count(*) from #issues

end

else

begin

	update PAD.GuardianChecks set
		CabinetOpenLock = @CabinetOpenLock, 
		CabinetBatteriesOk = @CabinetBatteriesOk, 
		CabinetLightWork = @CabinetLightWork, 
		NothingTouchingHeater = @NothingTouchingHeater, 
		AEDok = @AEDok, 
		AEDSilent = @AEDSilent, 
		ResuscitationKit = @ResusKit
	where
		PadId = @PadId
		and Date = @Date

end