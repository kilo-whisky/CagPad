use CAGPAD
go

create proc PAD.Cabinet_Upsert (
    @CabinetId int = null,
    @Name varchar(50),
    @Description varchar(100),
    @Supplier varchar(50) = null,
    @HeartSafeNumber int = null,
    @Expiry date = null,
    @UserId int
) as

declare @now datetime = getdate()

if not exists (select * from PAD.Cabinets where CabinetId = @CabinetId)
begin

    insert into PAD.Cabinets (Name, Description, Supplier, HeartSafeNumber, WarrantyExpiry, ModifiedBy, ModifiedOn)
    values (@Name, @Description, @Supplier, @HeartSafeNumber, @Expiry, @UserId, @now)
    select @CabinetId = SCOPE_IDENTITY()
    return @CabinetId

end

else 

begin

    update PAD.Cabinets set
        Name = @Name,
        Description = @Description,
        Supplier = @Supplier,
        HeartSafeNumber = @HeartSafeNumber,
        WarrantyExpiry = @Expiry,
        ModifiedBy = @UserId,
        ModifiedOn = @now
    where
        CabinetId = @CabinetId

end