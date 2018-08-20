use CAGPAD
go

create proc PAD.Guardians_Upsert (
    @PadId int,
    @UserId int,
    @AddRemove char(1)
) as

if @AddRemove = 'A'
begin
    insert into PAD.Guardians (PadId, UserId)
    values (@PadId, @UserId)
end

if @AddRemove = 'R'
begin
    delete from PAD.Guardians
    where
        PadId = @PadId
        and UserId = @UserId
end
    