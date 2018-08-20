use CAGPAD
go

alter proc PAD.GuardianCheck_List ( 
    @CheckId int = null,
	@PadId int = null,
	@Date date = null
) as

select
    g.CheckId,
	g.PadId, 
    p.Location,
	g.Date, 
	g.UserId,
	Guardian = u.FullName,
    g.Completed,
    g.Notes,
    Issues = isnull((select count(*) from pad.Answers where CheckId = g.CheckId and Answer = 0),0)
from
	PAD.GuardianChecks g
	join Core.Users u on g.UserId = u.UserId
    join PAD.PADSites p on g.PadId = p.PadId
where
    g.CheckId = ISNULL(@CheckId, g.CheckId)
	and g.PadId = ISNULL(@PadId, g.PadId)
	and g.Date = ISNULL(@Date, g.Date)
order by
    g.Date desc

go

exec PAD.GuardianCheck_List null, 1, null
