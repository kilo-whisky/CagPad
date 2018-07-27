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
	g.Date, 
	g.UserId,
	Guardian = u.FullName,
    g.Completed,
    g.Notes
from
	PAD.GuardianChecks g
	join Core.Users u on g.UserId = u.UserId
where
    g.CheckId = ISNULL(@CheckId, g.CheckId)
	and g.PadId = ISNULL(@PadId, g.PadId)
	and g.Date = ISNULL(@Date, g.Date)

go

exec PAD.GuardianCheck_List 1