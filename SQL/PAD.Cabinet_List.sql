USE CAGPAD
GO

alter proc PAD.Cabinet_List (
	@CabinetId int = null,
    @Selected bit = null
) as

select * into #selected from PAD.PADSites where Cabinet is not null

select
	CabinetId, 
	Name, 
	Description, 
	Supplier, 
	HeartSafeNumber, 
	WarrantyExpiry
into
    #cabinets
from
	PAD.Cabinets c
where
	c.CabinetId = ISNULL(@CabinetId, c.CabinetId)

if @Selected = 1
    select
        c.*
    from 
        #cabinets c
        join #selected s on c.CabinetId = s.Cabinet

if @Selected = 0
    select
        c.*
    from
        #cabinets c
    where
        c.CabinetId not in (select Cabinet from #selected)

if @Selected is null
    select * from #cabinets

go

exec pad.Cabinet_List null, null