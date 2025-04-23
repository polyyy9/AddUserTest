namespace TestAddUserDB
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_UserName_Pass()
        {
            User user = new User { Name = "user" };
            User result = UserRepository.CheckData(user);
            Assert.AreEqual(user.Name, result.Name);
        }

        [Test]
        public void Test_UserAge_Pass()
        {
            User user = new User { Birthday = new DateTime(2001, 01, 1) };
            User result = UserRepository.CheckData(user);
            Assert.AreEqual(user.Birthday, result.Birthday);

        }

        [Test]
        public void Test_UserAge_Fail()
        {
            User user = new User { Birthday = new DateTime(2020, 01, 1) };
            User result = UserRepository.CheckData(user);
            Assert.AreEqual(user.Birthday, result.Birthday);

        }

        [Test]
        public void Test_UserEmail_Pass()
        {
            User user = new User { Email = "mail@mail.com" };
            User result = UserRepository.CheckData(user);
            Assert.AreEqual(user.Email, result.Email);
        }
        [Test]
        public void Test_UserEmail_Fail()
        {
            User user = new User { Email = "@@mail.com" };
            User result = UserRepository.CheckData(user);
            Assert.AreEqual(user.Email, result.Email);
        }

        [Test]
        public void Test_UserPhone_Pass()
        {
            User user = new User { Phone = "+7-999-999-99-11" };
            User result = UserRepository.CheckData(user);
            Assert.AreEqual(user.Phone, result.Phone);
        }

        [Test]
        public void Test_UserPhone_Fail()
        {
            User user = new User { Phone = "999-999-99-11" };
            User result = UserRepository.CheckData(user);
            Assert.AreEqual(user.Phone, result.Phone);
        }

        [Test]
        public void Test_UserrAdd_Pass()
        {
            User user = new User();
            User userToAdd = UserRepository.CheckData(user);
            bool result = UserRepository.AddToDB(userToAdd);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_UserrAdd_Fail()
        {
            User user = new User { Name = "userrrrrrrrrrrr", Birthday = new DateTime(2010, 01, 1), Email="mail.com", Phone="999999999"};
            User userToAdd = UserRepository.CheckData(user);
            bool result = UserRepository.AddToDB(userToAdd);
            Assert.IsTrue(result);
        }
    }
}