using ClassLibrary1;
using ClassLibrary1.Service;

namespace TestProject1;
using Moq;
using NUnit.Framework;
public class Tests
{
    private Mock<IExternalUserService> _mockService;
    private IExternalUserService _service;
    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IExternalUserService>();
       
    }

    [Test]
    public void Test1()
    {
        
        Assert.Pass();
    }
    [Test]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = 2;
            var expectedUser = new Data
            {
                id = userId,
                email = "janet.weaver@reqres.in",
                first_name = "Janet",
                last_name = "Weaver",
                avatar = "https://reqres.in/img/faces/2-image.jpg"
            };
            var support = new Support
            {
                url = "https://reqres.in/#support-heading",
                text = "To keep ReqRes free, contributions towards server costs are appreciated!"
            };
            RootObjectUser rootObjectUser = new RootObjectUser()
            {
                data = expectedUser,
                support = support
            };

            _mockService.Setup(s => s.GetUserByIdAsync(userId))
                        .ReturnsAsync(rootObjectUser);

            // Act
            var result = await _mockService.Object.GetUserByIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.id, result.data.id);
            Assert.AreEqual(expectedUser.email, result.data.email);
            Assert.AreEqual(expectedUser.first_name, result.data.first_name);
            Assert.AreEqual(expectedUser.last_name, result.data.last_name);
            Assert.AreEqual(expectedUser.avatar, result.data.avatar);
        }

        // [Test]
        // public async Task GetAllUsersAsync_ReturnsListOfUsers()
        // {
        //     // Arrange
        //     var expectedUsers = new List<Data>
        //     {
        //         new Data
        //         {
        //             Id = 1,
        //             Email = "george.bluth@reqres.in",
        //             FirstName = "George",
        //             LastName = "Bluth",
        //             Avatar = "https://reqres.in/img/faces/1-image.jpg"
        //         },
        //         new Data
        //         {
        //             Id = 2,
        //             Email = "janet.weaver@reqres.in",
        //             FirstName = "Janet",
        //             LastName = "Weaver",
        //             Avatar = "https://reqres.in/img/faces/2-image.jpg"
        //         }
        //     };
        //
        //     _mockService.Setup(s => s.GetAllUsersAsync())
        //                 .ReturnsAsync(expectedUsers);
        //
        //     // Act
        //     var result = await _mockService.Object.GetAllUsersAsync();
        //
        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual(2, ((List<Data>)result).Count);
        // }
}