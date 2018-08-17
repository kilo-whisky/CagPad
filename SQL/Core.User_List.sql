use CAGPAD
go

alter proc Core.User_List (
    @UserId int = null,
    @UserName varchar(50) = null,
    @EmailAddress varchar(50) = null,
    @PadId int = null,
    @RoleName varchar(10) = null
) as


select distinct
    u.UserId,
    u.UserName,
    u.Password,
    u.FirstName,
    u.LastName,
    u.EmailAddress,
    u.Telephone,
    u.FullName,
    u.Active
into #Users
from
    Core.Users u
where
    u.UserId = ISNULL(@UserId, u.UserId)
    and u.UserName = ISNULL(@UserName, u.UserName)
    and u.EmailAddress = ISNULL(@EmailAddress, u.EmailAddress)

if @PadId is not null
    select
        UserId
    from
        PAD.Guardians
    where
        PadId = @PadId

if @RoleName is null
    select
        *
    from
        #Users

if @RoleName is not null
    select
        *
    from
        #Users u 
        join Core.UserRoles ur on u.UserId = ur.UserId and ur.RoleName = @RoleName

go

exec Core.User_List null, null, null, null, null