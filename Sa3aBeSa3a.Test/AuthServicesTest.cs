using Moq;
using Sa3aBeSa3a.Application.Services;
using Sa3aBeSa3a.Domain.Entities;
using Sa3aBeSa3a.Domain.Dtos;
using Sa3aBeSa3a.Domain.Enums;
using Sa3aBeSa3a.Domain.Exceptions;
using Sa3aBeSa3a.Domain.Interfaces;
using Sa3aBeSa3a.Domain.Interfaces.Repositries;
using Sa3aBeSa3a.Domain.Interfaces.Services;

namespace Sa3aBeSa3a.Test
{
    public class AuthServicesTest
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock = new();
        private readonly Mock<ITokenServices> _tokenServicesMock = new();
        public AuthServices CreateService()
        {
            return new AuthServices
                (
                _authRepositoryMock.Object,
                _tokenServicesMock.Object
                );
        }
        [Fact]
        public async Task GetProfileAsync_WhenMemberExists_ReturnMemberDto()
        {
            int memberId = 10;

            var member = new Members
            {
                Id = memberId,
                Name = "Omer",
                Email = "omer@test.com",
                PhoneNumber = "01012345678"
            };

            _authRepositoryMock.Setup(repo => repo.GetProfileAsync(memberId)).ReturnsAsync(member);

            var service = CreateService();
            var result = await service.GetProfileAsync(memberId);

            Assert.NotNull(result);
            Assert.Equal(memberId, result.MemberId);
            Assert.Equal("Omer", result.Name);
            Assert.Equal("omer@test.com", result.Email);
            Assert.Equal("01012345678", result.Phone);
        }
        [Fact]
        public async Task GetProfileAsynk_WhenMemberNotExist_ThrowNotFoundException()
        {
            int memberId = 11;
            _authRepositoryMock.Setup(repo => repo.GetProfileAsync(memberId)).ReturnsAsync((Members?)null);
             var service = CreateService();

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => service.GetProfileAsync(memberId));

            Assert.Equal(Err.UserNotFound, exception.Code);
        }
        [Fact]
        public async Task RegisterAsync_WhenNoException_ReturnAuthResultDtos()
        {
            var expireddate = new DateTime(2026, 11, 16, 23, 0, 0);
            var member = new Members
            {
                Id = 1,
                Name = "Ali",
                Email = "ali@test.com",
                PhoneNumber = "010123456",
                Role = MemberRole.Member
            };
            var memberdto = new MemberDto
            {
                MemberId = 1,
                Name = "Ali",
                Email = "ali@test.com",
                Phone = "010123456",
                Role = MemberRole.Member
            };
            RegisterDto dto = new RegisterDto
            {
                Name = "Ali",
                Email = "ali@test.com",
                Password = "123ali123",
                PhoneNumber = "010123456"
            };


            _authRepositoryMock.Setup(repo => repo.AddAsync(dto)).ReturnsAsync(member);
            _authRepositoryMock
                .Setup(repo => repo.IsEmailExistsAsync(dto.Email))
                .ReturnsAsync(false);
            _tokenServicesMock
                .Setup(token => token.Create(It.IsAny<Members>()))
                .Returns(new TokenResult("fake-jwt-token", expireddate));
            _authRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                 .Returns(Task.CompletedTask);
            var service = CreateService();
            var result = await service.RegisterAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("fake-jwt-token", result.Token);
            Assert.Equal(expireddate, result.ExpiresAt);
            Assert.Equal(memberdto.MemberId, result.Member.MemberId);
            Assert.Equal(memberdto.Email, result.Member.Email);
            Assert.Equal(memberdto.Name, result.Member.Name);
            Assert.Equal(memberdto.Phone, result.Member.Phone);
            Assert.Equal(memberdto.Role, result.Member.Role);
            Assert.False(string.IsNullOrEmpty(member.PasswordHash));
            Assert.NotEqual(dto.Password, member.PasswordHash);

        }
    }
}
