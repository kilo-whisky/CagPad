use CAGPAD
go

create proc Core.User_Upsert (
    @UserId int = null,
    @UserName varchar(50),
    @Password nvarchar(50),
    @FirstName varchar(50),
    @LastName varchar(50),
    @EmailAddress varchar(50),
    @Telephone varchar(50) = null,
    @Active bit
) as

if not exists (select * from Core.Users where UserName = @UserName)
begin

    insert into Core.Users (UserName, FirstName, LastName, EmailAddress, Telephone, Active)
    values (@UserName, @Password, @FirstName, @LastName, @EmailAddress, @Telephone, @Active)
    select @UserId = SCOPE_IDENTITY()
    return @UserId

end

else 

begin

    update Core.Users set
        

end