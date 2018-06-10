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
	
	declare @sql nvarchar(max) = ''

	create table #Issues (IssueName varchar(50))

	select @sql = @sql + ' insert into #Issues (IssueName) select ''' + c.name + ''' from PAD.GuardianChecks g where g.padid = ' + cast(@PadId as varchar(5)) + ' and g.date = ''' + cast(@Date as varchar(20)) + ''' and ' + c.name + ' = 0;'
	from 
		sys.columns c
		join sys.tables t on c.object_id = t.object_id
	where
		t.name = 'GuardianChecks'
		and c.system_type_id = 104

	exec (@sql)

	select * from #Issues

end

go

exec PAD.GuardianCheck_List 1, '2018-06-10'