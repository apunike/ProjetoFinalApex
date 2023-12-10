using Data.Interfaces;
using Data.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Dtos.Contact;

namespace Services.ApiServices
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _userRepository;


        public ContactService(
            IContactRepository contactRepository,
            IUserRepository userRepository)
        {
            _contactRepository = contactRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(ContactCreateRequestDto contactCreateDto)
        {
            var user = await _userRepository.GetByIdAsync(contactCreateDto.UserId);

            if (user != null)
            {

                var contact = new Contact()
                {

                    Name = contactCreateDto.Name,
                    Phone = contactCreateDto.Phone,
                    UserId = contactCreateDto.UserId,
                    CreatedAdt = DateTime.Now
                };

                await _contactRepository.CreateAsync(contact);

                await _contactRepository.SaveChangesAsync(); 

                return true;
            }

            return false;
        }

        public async Task <bool> DeleteAsync(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact != null)
            {
                _contactRepository.Delete(contact);

                await _contactRepository.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<List<ContactResponseDto>> GetAllByUserIdAsync(int userId)
        {
            var contacts = await  _contactRepository.GetAllByUserIdAsync(userId);

            var contactsDtos = new List<ContactResponseDto>();

            foreach (var contactModel in contacts)
            {
                contactsDtos.Add(
                    new ContactResponseDto()
                    {
                        Id = contactModel.Id,
                        Name = contactModel.Name,
                        Phone = contactModel.Phone,
                        UserId = contactModel.UserId

                    }
                  );
            }
            return contactsDtos;


        }


        public async Task<bool> UpdateAsync(ContactUpdateResquestDto contactUpdateDto)
        {
            var contactFound = await _contactRepository.GetByIdAsync(contactUpdateDto.Id);

            if (contactFound != null)
            {
                contactFound.Name = contactUpdateDto.Name;
                contactFound.Phone = contactUpdateDto.Phone;

                contactFound.UpdatedAdt = DateTime.Now;

                _contactRepository.Update(contactFound);

                await _contactRepository.SaveChangesAsync();

                return true;
            }

            return false;

        }
    }
}
