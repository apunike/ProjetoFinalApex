using Data.Interfaces;
using Data.Models;
using Moq;
using Services.ApiServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Dtos.User;
using Xunit;

namespace Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task MetodoCreateAsync_Chama_UserRepository_CreateAsync_E_SaveChangesAsync()
        {
            //Preparação (Arrange)
            var userCreateDto = new UserCreateRequestDto
            {
                Name = "Joao",
                Email = "Joao@exemplo.com",
                Password = "123"
            };

            //Ação (Act)
            await _userService.CreateAsync(userCreateDto);

            // Afirmar ou Validar (Assert)
            _userRepositoryMock.Verify(repository => repository.CreateAsync(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(repository => repository.SaveChangeAsync(), Times.Once);
        }

        [Fact]
        public async Task MetodoDeleteAsync_QuandoExisteUsuario_Chama_UserRepository_Delete_E_SaveChangeAsync_E_RetornaTrue()
        {
            //Preparação (Arrange)
            var userId = 1;
            var existingUser = new User { Id = userId };
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);

            //Ação (Act)
            var result = await _userService.DeleteAsync(userId);

            //Afirmar ou Validar (Assert)
            _userRepositoryMock.Verify(repository => repository.Delete(existingUser), Times.Once);
            _userRepositoryMock.Verify(repository => repository.SaveChangeAsync(), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_QuandoNaoExisteUsuario_RetornaFalse()
        {
            // Preparação (Arrange)
            var userId = 1;

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(userId))
                .ReturnsAsync((User)null);

            //Açãoc(Act)
            var result = await _userService.DeleteAsync(userId);

            // Afirmar ou Validar (Assert)
            _userRepositoryMock.Verify(repo => repo.Delete(It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.SaveChangeAsync(), Times.Never);
            Assert.False(result);

        }

        [Fact]
        public async Task GetAllAsync_Retorna_Lista_De_UserResponseDto()
        {
            //Preparação (Arrange)
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Joao",
                    Contacts = new List<Contact>
                    {
                        new Contact {Name = "Pai", Phone ="99998888"}
                    }
                },

                new User
                {
                    Id = 2,
                    Name = "Maria",
                    Email = "maria@eexemplo.com",
                    Contacts = new List<Contact>
                    {
                        new Contact {Name = "Filho", Phone ="88889999"}

                    }
                }
            };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(users);
            //Ação (Act)
            var result = await _userService.GetAllAsync();

            //Afirmar ou Validar (Assert)
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count);
            Assert.IsType<List<UserResponseDto>>(result);

        }

        [Fact]
        public async Task UpdateAsync_QuandoExisteUsuario_Chama_UserRepository_Update_E_SaveChangeAsync_E_RetornaTrue()
        {
            //Preparação (Arrange)
            var userUpdateDto = new UserUpdateRequestDto
            {
                Id = 1,
                Name = "John Doe",
                Email = "Johndoe@exmeplo.com"
            };

            var existingUser = new User { Id = userUpdateDto.Id };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userUpdateDto.Id))
                .ReturnsAsync(existingUser);

            //Ação (Act)
            var result = await _userService.UpdateAsync(userUpdateDto);


            //Afirmar ou Validat (Assert)
            _userRepositoryMock.Verify(repo => repo.Update(existingUser), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangeAsync(), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_Quando_Nao_ExisteUsuario_RetornaFalse()
        {
            //Preparação (Arrange)
            var userUpdateDto = new UserUpdateRequestDto
            {
                Id = 1,
                Name = "John Doe",
                Email = "Johndoe@exmeplo.com"
            };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userUpdateDto.Id))
                .ReturnsAsync((User)null);

            //Ação (Act)
            var result = await _userService.UpdateAsync(userUpdateDto);

            //Afirmar ou Validat (Assert)
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.SaveChangeAsync(), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateToAdmin_QuandoExisteUsuario_Chama_UserRepository_Update_E_SaveChangeAsync()
        {
            //Preparação (Arrange)
            var userId = 1;
            var existingUser = new User { Id = userId };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(existingUser);

            //Ação (Act)
            await _userService.UpdateToAdmin(userId);

            //Afirmar ou Validat (Assert)
            _userRepositoryMock.Verify(repo => repo.Update(existingUser), Times.Once);
            _userRepositoryMock.Verify(repo => repo.SaveChangeAsync(), Times.Once);

        }

        [Fact]
        public async Task UpdateToAdmin_QuandoNaoExisteUsuario_NaoChama_UserRepository_Update_E_SaveChangeAsync()
        {
            //Preparação (Arrange)
            var userId = 1;
        
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync((User)null);

            //Ação (Act)
            await _userService.UpdateToAdmin(userId);

            //Afirmar ou Validat (Assert)
            _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never);
            _userRepositoryMock.Verify(repo => repo.SaveChangeAsync(), Times.Never);

        }
    }
}
  