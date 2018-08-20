use CAGPAD
go

alter proc Core.User_Login (
    @Username varchar(50),
    @Password nvarchar(max) = null
) as

select
    UserId,
    UserName,
    FullName,
    Salt,
    Match = cast(case when @Password = Password then 1 else 0 end as bit)
into #result
from
    Core.Users
where
    UserName = @Username

if (select top 1 Match from #result) = 1
update Core.Users set LastLogon = GETDATE() where UserName = @Username

select * from #result

go

exec Core.User_Login 'KWOOD'

