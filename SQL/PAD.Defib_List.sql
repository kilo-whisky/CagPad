use CAGPAD
go

alter proc PAD.Defib_List (
	@DefibId int = null,
    @Selected bit = null
) as

select * into #Selected from PAD.PADSites where Defib is not null

select
	DefibId, 
	Name, 
	Description, 
	Supplier, 
	Serial, 
	WarrantyExpires, 
	BatteryExpiry
into
    #Defibs
from
	PAD.Defibrillators d
where
	d.DefibId = ISNULL(@DefibId, d.DefibId)

if @Selected = 1
    select
        d.*
    from
        #Defibs d
        join #Selected s on d.DefibId = s.Defib

if @Selected = 0
    select 
        d.*
    from
        #Defibs d
    where
        d.DefibId not in (select Defib from #Selected)

if @Selected is null
    select * from #Defibs


go

exec pad.Defib_List null, null