use CAGPAD
go

create proc Core.UserRole_Upsert (
    @UserId int,
    @RoleName varchar(10),
    @AddRemove char(1)
) as

if @AddRemove = 'A'
begin
    insert into Core.UserRoles (UserId, RoleName)
    values (@UserId, @RoleName)
end

if @AddRemove = 'R'
begin
    delete from Core.UserRoles
    where 
        UserId = @UserId
        and RoleName = @RoleName
end