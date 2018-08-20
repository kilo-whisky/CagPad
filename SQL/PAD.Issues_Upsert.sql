use CAGPAD
go

alter proc PAD.Issues_Upsert (
    @IssueId int = null,
    @PadId int = null,
    @DefibId int = null,
    @CabinetId int = null,
    @Severity int,
    @Description varchar(max),
    @Image varchar(max),
    @UserId int,
    @Resolved bit
) as

declare @now datetime = getdate()

if @IssueId is null
begin

    insert into PAD.Issues (PadId, DefibId, CabinetId, Severity, Description, Image, ReportedBy, ReportedOn, Resolved)
    values (@PadId, @DefibId, @CabinetId, @Severity, @Description, @Image, @UserId, @now, 0)
    select @IssueId = SCOPE_IDENTITY()
    return @IssueId

end

else 

begin

    update PAD.Issues set
        Severity = @Severity,
        Description = @Description,
        Image = @Image,
        Resolved = @Resolved
    where
        IssueId = @IssueId
    
    return @IssueId

end