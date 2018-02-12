IF EXISTS(SELECT *
          FROM   dbo.Users)
  DROP TABLE dbo.Users
CREATE TABLE Users (
    EmailAddress varchar(255) NOT NULL,
    PasswordHash BINARY(64) NOT NULL,
    CONSTRAINT [PK_User_EmailAddress] PRIMARY KEY CLUSTERED (EmailAddress ASC)
);