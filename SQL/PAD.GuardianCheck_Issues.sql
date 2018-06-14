use CAGPAD
go

create proc PAD.GuardianCheck_Issues (@PadId int, @Date datetime)
as

begin

    create table #issues (IssueName varchar(50))

    declare @sql nvarchar(max) = ''
    select @sql = @sql + ' insert into #Issues (IssueName) select ''' + c.name + ''' from PAD.GuardianChecks g where g.padid = ' + cast(@PadId as varchar(5)) + ' and g.date = ''' + cast(@Date as varchar(20)) + ''' and ' + c.name + ' = 0;'
	from 
		sys.columns c
		join sys.tables t on c.object_id = t.object_id
	where
		t.name = 'GuardianChecks'
		and c.system_type_id = 104

	exec (@sql)

	select * from #issues

end

	