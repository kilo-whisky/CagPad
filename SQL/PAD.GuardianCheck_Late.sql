use CAGPAD
go

alter proc PAD.GuardianCheck_Late (
    @Late bit
) as

declare @today datetime = getdate();

with lastchecks as (
    select
        p.PadId,
        LastCheck = cast('2018-06-14' as date)--max(p.Completed)
    from
        PAD.GuardianChecks p
    group by
        p.PadId
)

select
    p.PadId,
    p.Location,
    l.LastCheck,
    Late = cast(case 
        when DATEDIFF(WEEK, isnull(l.LastCheck, '1900-01-01'), @today) > 2 then 1
        else 0
    end as bit)
into
    #Results
from
    PAD.PADSites p
    left join lastchecks l on p.PadId = l.PadId

select
    *
from
    #Results r
where
    r.Late = ISNULL(@Late, r.Late)

go

exec PAD.GuardianCheck_Late null