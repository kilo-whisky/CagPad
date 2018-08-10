use CAGPAD
go

alter proc Core.UserRole_List (
    @UserId int = null,
    @UserName varchar(50) = null
) as

select
    ur.UserId,
    ur.roleName
from
    Core.UserRoles ur
    join Core.Users u on ur.UserId = u.UserId
where
    u.UserId = ISNULL(@UserId, u.UserId)
    and u.UserName = ISNULL(@UserName, u.UserName)

go

exec Core.UserRole_List null, 'kwood'