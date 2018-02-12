IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL 
  DROP TABLE dbo.Users; 
CREATE TABLE Users (
    EmailAddress varchar(255) NOT NULL,
    PasswordHash BINARY(64) NOT NULL,
    CONSTRAINT [PK_User_EmailAddress] PRIMARY KEY CLUSTERED (EmailAddress ASC)
);