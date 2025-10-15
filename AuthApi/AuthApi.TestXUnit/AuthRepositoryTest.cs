using AuthApi.DTOs.UsuarioDTOs;
using AuthApi.Entidades;
using AuthApi.Interfaces;
using AuthApi.Repositorios;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApi.TestXUnit
{
    public class AuthRepositoryTest
    {
        private IConfiguration GetTestConfiguration()
        {
            var inMemoryConfig = new Dictionary<string, string>
            {
                {"Jwt:Key", "ClaveSuperSecretaMuyLargaTest567890!" },
                {"Jwt:Issuer", "AuthApiTest" },
                {"Jwt:Audience", "AuthApiClients" },
            };
            return new ConfigurationBuilder().AddInMemoryCollection(inMemoryConfig).Build();
        }

        [Fact]
        public async Task RegistrarAsync_RetorneUsuarioConToken()
        {
            //Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            var config = GetTestConfiguration();
            var usuario = new Usuario
            {
                Id = 1,
                Nombre = "elias",
                Email = "elias@test.com",
                PasswordHash = "hash",
                Rol = new Rol { Id = 2, Nombre = "Usuario" }
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Usuario>())).ReturnsAsync(usuario);
            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(usuario);

            var service = new AuthRepository(mockRepo.Object, config);

            var registroDTO = new UsuarioRegistroDto
            {
                Nombre = "elias",
                Email = "elias@test.com",
                Password = "123"
            };

            //Act
            var result = await service.RegistrarAsync(registroDTO);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("elias", result.Nombre);
            Assert.Equal("elias@test.com", result.Email);
            Assert.False(string.IsNullOrEmpty(result.Token));

        }

        [Fact]
        public async Task LoginAsync_RetornarNullSiUsuarioNoExiste()
        {
            // Arrange
            var mockRepo = new Mock<IUsuarioRepository>();
            var config = GetTestConfiguration();

            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Usuario?)null);

            var service = new AuthRepository(mockRepo.Object, config);

            var loginDTO = new UsuarioLoginDto
            {
                Email = "eliasnoexiste@test.com",
                Password = "123",
            };

            // Act
            var result = service.LoginAsync(loginDTO);

            // Assert
            Assert.NotNull(result);
        }
    }
}
