use CAGPAD
go

alter proc PAD.Answers_Upsert (
    @CheckId int,
    @QuestionId int,
    @Answer bit,
    @UserId int
) as

declare @AnswerId int

insert into PAD.Answers (CheckId, QuestionId, Answer, UserId)
values (@CheckId, @QuestionId, @Answer, @UserId)
select @AnswerId = SCOPE_IDENTITY()

if @Answer = 0

begin

    declare @PadId int,
            @DefibId int,
            @CabinetId int,
            @IssueType char(1),
            @Now datetime = getdate()

    select @IssueType = Type from PAD.Questions q where QuestionId = @QuestionId
    select @PadId = p.PadId, @DefibId = p.Defib, @CabinetId = p.Cabinet from PAD.PADSites p join PAD.GuardianChecks c on p.PadId = c.PadId and c.CheckId = @CheckId    

    insert into PAD.Issues (AnswerId, PadId, DefibId, CabinetId, ReportedBy, ReportedOn, Resolved)
    select
        @AnswerId,
        @PadId,
        case when @IssueType = 'D' then @DefibId else null end,
        case when @IssueType = 'C' then @CabinetId else null end,
        @UserId,
        @Now,
        0

end