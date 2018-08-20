use CAGPAD
go

create proc PAD.Defib_Upsert (
    @DefibId int = null,
    @Name varchar(50),
    @Description varchar(100),
    @Supplier varchar(50) = null,
    @Serial nvarchar(50) = null,
    @WarrantyExpires date = null,
    @BatteryExpiry date = null,
    @UserId int
) as

declare @now datetime = getdate()

if not exists (select * from PAD.Defibrillators where DefibId = @DefibId)
begin

    insert into PAD.Defibrillators(Name, Description, Supplier, Serial, WarrantyExpires, BatteryExpiry, ModifiedBy, ModifiedOn)
    values (@Name, @Description, @Supplier, @Serial, @WarrantyExpires, @BatteryExpiry, @UserId, @now)
    select @DefibId = SCOPE_IDENTITY()
    return @DefibId

end

else 

begin

    update PAD.Defibrillators set
        Name = @Name,
        Description = @Description,
        Supplier = @Supplier,
        Serial = @Serial,
        WarrantyExpires = @WarrantyExpires,
        BatteryExpiry = @BatteryExpiry,
        ModifiedBy = @UserId,
        ModifiedOn = @now
    where
        DefibId = @DefibId

end