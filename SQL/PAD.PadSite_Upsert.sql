use CAGPAD
go

alter proc PAD.PadSite_Upsert (
    @PadId int = null,
    @Location varchar(50),
    @Address varchar(max) = null,
    @Description varchar(max) = null,
    @Cabinet int = null,
    @Defib int = null,
    @Owner varchar(50),
    @OwnerTel varchar(12),
    @OwnerEmail varchar(50),
    @InstallDate date,
    @Funding varchar(100),
    @Amount decimal(18,2),
    @Insurance varchar(50),
    @PadsExpiry date,
    @Map nvarchar(max)
) as

if not exists (select * from PAD.PADSites where PadId = @PadId)
begin
    insert into PAD.PADSites(Location, Address, Description, Cabinet, Defib, Owner, OwnerTel, OwnerEmail, InstallDate, Funding, Amount, Insurance, PadsExpiry, Map)
    values (@Location, @Address, @Description, @Cabinet, @Defib, @Owner, @OwnerTel, @OwnerEmail, @InstallDate, @Funding, @Amount, @Insurance, @PadsExpiry, @Map)
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
        Map = @Map
    where
        PadId = @PadId
end