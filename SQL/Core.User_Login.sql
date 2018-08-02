use CAGPAD
go

alter proc Core.User_Login (
    @Username varchar(50),
    @Password nvarchar(50)
) as

if not exists (select * from Core.Users where UserName = @Username)
begin
    return -1
end

declare @UserId int,
        @CurrentPassword nvarchar(50)

select @UserId = UserId, @CurrentPassword = Password from Core.Users where UserName = @Username and Active = 1

if @Password = @CurrentPassword
begin
    return @UserId
end
else
begin
    return -2
end


