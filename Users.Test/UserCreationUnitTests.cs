using GenFu;
using Users.Backend;
using Xunit;

namespace Users.Test
{
    public class UserCreationUnitTests
    {
        [Fact]
        public void CreateUser_Success()
        {
            UserValidator userValidator = new UserValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", 6);
            using (var db = new UsersRepository())
            {
                // Arrange
                GenFu.GenFu.Configure<User>()
                    .Fill(x => x.EmailAddress).AsEmailAddress()
                    .Fill(x => x.Password, u => MockDataUtilities.RandomValidPassword(6))
                    .Fill(x => x.PasswordConfirmation, u => u.Password);
                var user = GenFu.GenFu.New<User>();
                
                // Act
                string actualMessage = db.AddUser(user, userValidator);

                // Assert
                Assert.Equal(
                    Messages.AddUserSuccess,
                    actualMessage);
            }
        }
        
        [Fact]
        public void CreateUser_InvalidEmailAddress()
        {
            UserValidator userValidator = new UserValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", 6);
            using (var db = new UsersRepository())
            {
                // Arrange
                GenFu.GenFu.Configure<User>()
                    .Fill(x => x.EmailAddress, "a@")
                    .Fill(x => x.Password, u => MockDataUtilities.RandomValidPassword(6))
                    .Fill(x => x.PasswordConfirmation).Fill(u => u.Password);
                var user = GenFu.GenFu.New<User>();

                // Act
                string actualMessage = db.AddUser(user, userValidator);

                // Assert
                Assert.Equal(
                    Messages.InvalidEmailAddress,
                    actualMessage);
            }
        }

        [Fact]
        public void CreateUser_PasswordDoesNotMatchPattern()
        {
            // Arrange
            UserValidator userValidator = new UserValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", 6);
            var db = new UsersRepository();
            GenFu.GenFu.Configure<User>()
                .Fill(x => x.EmailAddress).AsEmailAddress()
                .Fill(x => x.Password, u => MockDataUtilities.RandomInvalidPassword(6))
                .Fill(x => x.PasswordConfirmation).Fill(u => u.Password);
            var user = GenFu.GenFu.New<User>();

            // Act
            string actualMessage = db.AddUser(user, userValidator);

            // Assert
            Assert.Equal(
                Messages.PasswordDoesNotMatchPattern,
                actualMessage);
        }

        [Fact]
        public void CreateUser_PasswordTooShort()
        {
            UserValidator userValidator = new UserValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", 6);
            var db = new UsersRepository();
            // Arrange
            GenFu.GenFu.Configure<User>()
                .Fill(x => x.EmailAddress).AsEmailAddress()
                .Fill(x => x.Password, u => MockDataUtilities.RandomValidPassword(4))
                .Fill(x => x.PasswordConfirmation).Fill(u => u.Password);
            var user = GenFu.GenFu.New<User>();

            // Act
            string actualMessage = db.AddUser(user, userValidator);

            // Assert
            Assert.Equal(
                Messages.PasswordTooShort,
                actualMessage);
        }

        [Fact]
        public void CreateUser_PasswordsDoNotMatch()
        {
            UserValidator userValidator = new UserValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", 6);
            var db = new UsersRepository();
            // Arrange
            GenFu.GenFu.Configure<User>()
                .Fill(x => x.EmailAddress).AsEmailAddress()
                .Fill(x => x.Password, u => MockDataUtilities.RandomValidPassword(6))
                .Fill(x => x.PasswordConfirmation, u => MockDataUtilities.RandomValidPassword(6));
            var user = GenFu.GenFu.New<User>();

            // Act
            string actualMessage = db.AddUser(user, userValidator);

            // Assert
            Assert.Equal(
                Messages.PasswordsDoNotMatch,
                actualMessage);
        }

        [Fact]
        public void CreateUser_EmailAddressExistsAlready()
        {
            UserValidator userValidator = new UserValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", 6);
            using (var db = new UsersRepository())
            {
                // Arrange
                GenFu.GenFu.Configure<User>()
                    .Fill(x => x.EmailAddress).AsEmailAddress()
                    .Fill(x => x.Password, u => MockDataUtilities.RandomValidPassword(6))
                    .Fill(x => x.PasswordConfirmation, u => u.Password);
                var user = GenFu.GenFu.New<User>();
                
                // Act
                db.AddUser(user, userValidator);
                // add user again
                string actualMessage = db.AddUser(user, userValidator);

                // Assert
                Assert.Equal(
                    Messages.UserExists,
                    actualMessage);
            }
        }
    }
}
