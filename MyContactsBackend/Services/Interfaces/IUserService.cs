using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Dtos.User;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task CreatAsync(UserCreateRequestDto userCreateDto);
        Task<List<UserResponseDto>> GetAllAsync();
        Task<bool> UpdateAsync(UserUpdateRequestDto userUpdateDto);
        Task<bool> DeleteAsync(int id);
    }
}
