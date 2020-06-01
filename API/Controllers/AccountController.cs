using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 ITokenService tokenService,
                                 IMapper mapper)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUserInfoByToken()
        {
            var user = await _userManager.FindByClaimsPrincipal(HttpContext.User).ConfigureAwait(false);

            var loginResponse = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };

            return Ok(loginResponse);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckIfEmailExists([FromQuery] string email)
        {
            return Ok(await CheckIfEmailExistsAsync(email).ConfigureAwait(false));
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByClaimsPrincipalWithAddressAsync(HttpContext.User).ConfigureAwait(false);

            var addressDto = _mapper.Map<Address, AddressDto>(user?.Address);
            return Ok(addressDto);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var  user = await _userManager.FindByClaimsPrincipalWithAddressAsync(HttpContext.User).ConfigureAwait(false);

            user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);

            if (result.Succeeded) return Ok(addressDto);

            return BadRequest("Problem updating the user's address");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email).ConfigureAwait(false);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false).ConfigureAwait(false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var loginResponse = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };

            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            bool emailTaken = await CheckIfEmailExistsAsync(registerDto.Email).ConfigureAwait(false);

            if (emailTaken)
            {
                var errorResponse = new ApiValidationError
                {
                    Errors = new [] { "Email address is in use. "}
                };

                return new BadRequestObjectResult(errorResponse);
            }

            var result = await _userManager.CreateAsync(user, registerDto.Password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            var registerResponse = new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };

            return Ok(registerResponse);
        }

        private async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            return user != null;
        }
    }
}