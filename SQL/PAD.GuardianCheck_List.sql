use CAGPAD
go

alter proc PAD.GuardianCheck_List (
	@PadId int = null,
	@Date datetime = null
) as

select
	PadId, 
	Date, 
	g.UserId,
	Guardian = u.FullName, 
	CabinetOpenLock, 
	CabinetBatteriesOk, 
	CabinetLightWork, 
	NothingTouchingHeater, 
	AEDok, 
	AEDSilent, 
	ResuscitationKit
from
	PAD.GuardianChecks g
	join Core.Users u on g.UserId = u.UserId
where
	g.PadId = ISNULL(@PadId, g.PadId)
	and g.Date = ISNULL(@Date, g.Date)

if @PadId is not null and @Date is not null
begin
	
	
	create table #Issues (IssueName varchar(50))
    insert into #Issues
    exec PAD.GuardianCheck_Issues @PadId, @Date

    select * from #Issues

end

go

exec PAD.GuardianCheck_List 1, '2018-06-11'