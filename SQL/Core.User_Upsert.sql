use CAGPAD
go

alter proc Core.User_Upsert (
    @UserId int = null,
    @UserName varchar(50),
    @Password nvarchar(max),
    @Salt varchar(max),
    @FirstName varchar(50),
    @LastName varchar(50),
    @EmailAddress varchar(50),
    @Telephone varchar(50) = null,
    @Active bit
) as

if not exists (select * from Core.Users where UserName = @UserName)
begin

    insert into Core.Users (UserName, Password, Salt, FirstName, LastName, EmailAddress, Telephone, Active)
    values (@UserName, @Password, @Salt, @FirstName, @LastName, @EmailAddress, @Telephone, @Active)
    select @UserId = SCOPE_IDENTITY()
    return @UserId

end

else 

begin

    update Core.Users set
        Password = @Password,
        Salt = @Salt,
        FirstName = @FirstName,
        LastName = @LastName,
        EmailAddress = @EmailAddress,
        Telephone = @Telephone,
        Active = @Active
    where
        UserId = @UserId
    
    return @UserId

end