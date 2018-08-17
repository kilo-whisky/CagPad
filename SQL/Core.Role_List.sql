use CAGPAD
go

alter proc Core.Role_List (
    @UserId int = null
) as

select
    RoleName,
    RoleDescription
from
    Core.Roles

if @UserId is not null

select
    r.RoleName
from
    Core.Roles r
    join Core.UserRoles ur on r.RoleName = ur.RoleName and ur.UserId = @UserId

go

exec core.Role_List 1