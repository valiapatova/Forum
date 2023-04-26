using Forum.Data.Models;
using Forum.DTOs;
using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Mappers
{
	public class UserMapper
	{
		public UserResponseGetDto ConvertUserToUserDto(User user)
		{
			UserResponseGetDto model = new UserResponseGetDto(user);

			return model;
		}
		public UserResponseGetIncludeDto ConvertUserToIncludeUserDto(User user)
		{
			UserResponseGetIncludeDto model = new UserResponseGetIncludeDto(user);

			return model;
		}

		public List<UserResponseGetIncludeDto> ConvertUsersToIncludeUsersDto(List<User> users)
		{

			List<UserResponseGetIncludeDto> usersToView = users.Select(u => new UserResponseGetIncludeDto(u)).ToList();


			return usersToView;
		}



		public User ConvertUserDtoToUserForCreate(UserRequestForCreateDto userDto)
		{
			User model = new User();

			model.FirstName = userDto.FirstName;
			model.LastName = userDto.LastName;
			model.Username = userDto.Username;
			model.Password = userDto.Password;
			model.Email = userDto.Email;
			model.RoleId = 2;
			return model;
		}
		public User ConvertUserDtoToUserForUpdate(UserRequestForUpdateDto userDto, User oldUser)
		{
			oldUser.FirstName = userDto.FirstName;
			oldUser.LastName = userDto.LastName;

			oldUser.Password = userDto.Password;
			oldUser.Email = userDto.Email;

			return oldUser;
		}

		public User Convert(RegisterViewModel viewModel)
		{
			return new User()
			{
				Username = viewModel.Username,
				Password = viewModel.Password,
				FirstName = viewModel.FirstName,
				LastName = viewModel.LastName,
				Email = viewModel.Email,
				RoleId = 2

			};

		}
	}
}

