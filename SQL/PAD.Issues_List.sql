use CAGPAD
go

alter proc PAD.Issues_List (
    @IssueId int = null,
    @CheckId int = null,
    @Resolved bit = null,
    @PadId int = null,
    @DefibId int = null,
    @CabinetId int = null
) as

select distinct
	i.IssueId, 
	i.PadId, 
	PadSite = p.Location,
    a.AnswerId,
    a.CheckId,
    q.Question,
	i.DefibId, 
    i.Image,
	Defib = d.Name,
	i.CabinetId, 
	Cabinet = c.Name,
	i.Severity, 
	i.Description, 
	ReportedBy = u.FullName, 
	i.ReportedOn,
	i.Resolved
from 
	PAD.Issues i
    left join PAD.Answers a on i.AnswerId = a.AnswerId and a.CheckId = isnull(@CheckId, a.CheckId)
    left join PAD.Questions q on a.QuestionId = q.QuestionId 
	left join PAD.PADSites p on i.PadId = p.PadId
	left join PAD.Defibrillators d on i.DefibId = d.DefibId
	left join PAD.Cabinets c on i.CabinetId = c.CabinetId
	left join core.Users u on i.ReportedBy = u.UserId
where
    i.IssueId = ISNULL(@IssueId, i.IssueId)
    and isnull(a.CheckId, 0) = ISNULL(@CheckId, isnull(a.CheckId,0))
	and isnull(i.PadId,0) = ISNULL(@PadId, isnull(i.PadId,0))
	and isnull(i.DefibId,0) = ISNULL(@DefibId, isnull(i.DefibId,0))
	and isnull(i.CabinetId,0) = ISNULL(@CabinetId, isnull(i.CabinetId,0))
    and i.Resolved = ISNULL(@Resolved, i.Resolved)

go

exec PAD.Issues_List
    @IssueId = null,
    @CheckId = null,
    @Resolved = null,
    @PadId = null,
    @CabinetId = null,
    @DefibId = null