use CAGPAD
go

alter proc Core.User_Login (
    @Username varchar(50),
    @Password nvarchar(max) = null
) as

select
    UserId,
    UserName,
    Salt,
    Match = cast(case when @Password = Password then 1 else 0 end as bit)
from
    Core.Users
where
    UserName = @Username

go

exec Core.User_Login 'KWOOD'

