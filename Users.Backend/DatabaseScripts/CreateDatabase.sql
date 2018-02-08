CREATE TABLE Users (
    ID int IDENTITY(1,1) PRIMARY KEY,
    EmailAddress varchar(255) NOT NULL,
    PasswordValue varchar(255),
);