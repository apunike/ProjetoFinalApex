using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Api;
using Utils.Dtos.Contact;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [Authorize(Policy = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var result = await _contactService.GetAllAsync();

                return Ok(new ApiResponse<List<ContactResponseDto>>(result));
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResponse(ex.Message));
            }

        }
        [Authorize(Policy = "Consumer")]
        [HttpPost]
        public async Task<IActionResult> CreateNewContact([FromBody] ContactCreateRequestDto contactDto)
        {
            try
            {
               var success =  await _contactService.CreateAsync(contactDto);

                if (success == true)
                {
                    return Ok(new ApiResponse());
                }
                else
                {
                    return BadRequest(new ApiResponse("Id do Usuario não encontrado."));
                }                        
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(ex.Message));
            }

        }
        [Authorize(Policy = "Consumer")]
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] ContactUpdateResquestDto contactDto)
        {
            try
            {
                var success = await _contactService.UpdateAsync(contactDto);

                if (success == true)
                {
                    return Ok(new ApiResponse());
                }
                else
                {
                    return BadRequest(new ApiResponse(">> Contact not Found.<<"));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(ex.Message));
            }

        }
        [Authorize(Policy = "Consumer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var success = await _contactService.DeleteAsync(id);

                if (success == true)
                {
                    return Ok(new ApiResponse());
                }
                else
                {
                    return BadRequest(new ApiResponse(">> Contact not fount <<"));
                }


            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(ex.Message));
            }

        }

    }
}
