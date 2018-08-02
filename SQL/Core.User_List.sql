use CAGPAD
go

alter proc Core.User_List (
    @UserId int = null,
    @UserName varchar(50) = null,
    @EmailAddress varchar(50) = null
) as

select
    u.UserId,
    u.UserName,
    u.Password,
    u.FirstName,
    u.LastName,
    u.EmailAddress,
    u.Telephone,
    u.FullName,
    u.Active
from
    Core.Users u
where
    u.UserId = ISNULL(@UserId, u.UserId)
    and u.UserName = ISNULL(@UserName, u.UserName)
    and u.EmailAddress = ISNULL(@EmailAddress, u.EmailAddress)
go

exec Core.User_List