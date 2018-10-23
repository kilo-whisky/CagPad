use CAGPAD
go

alter proc PAD.PadSite_Upsert (
    @PadId int = null,
    @Location varchar(50),
    @Address varchar(max) = null,
    @Description varchar(max) = null,
    @Cabinet int = null,
    @Defib int = null,
    @Owner varchar(50) = null,
    @OwnerTel varchar(12) = null,
    @OwnerEmail varchar(50) = null,
    @InstallDate date = null,
    @Funding varchar(100) = null,
    @Amount decimal(18,2) = null,
    @Insurance varchar(50) = null,
    @PadsExpiry date = null,
    @Map nvarchar(max) = null,
    @QuestionSet int
) as

if not exists (select * from PAD.PADSites where PadId = @PadId)
begin
    insert into PAD.PADSites(Location, Address, Description, Cabinet, Defib, Owner, OwnerTel, OwnerEmail, InstallDate, Funding, Amount, Insurance, PadsExpiry, Map, QuestionSet)
    values (@Location, @Address, @Description, @Cabinet, @Defib, @Owner, @OwnerTel, @OwnerEmail, @InstallDate, @Funding, @Amount, @Insurance, @PadsExpiry, @Map, @QuestionSet)
    select @PadId = SCOPE_IDENTITY()
    return @PadId
end

else 

begin
    update PAD.PADSites set
        Location = @Location,
        Address = @Address,
        Description = @Description,
        Cabinet = @Cabinet,
        Defib = @Defib,
        Owner = @Owner,
        OwnerTel = @OwnerTel,
        OwnerEmail = @OwnerEmail,
        InstallDate = @InstallDate,
        Funding = @Funding,
        Amount = @Amount,
        Insurance = @Insurance,
        PadsExpiry = @PadsExpiry,
        Map = @Map,
        QuestionSet = @QuestionSet
    where
        PadId = @PadId
end