
using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class AccountRepository : IAccountRepository
    {

        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> _signInManager;
        public IConfiguration _configuration;

        public BinaryReader JwtRegisteredClaimName { get; private set; }
        public object Encoading { get; private set; }

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration    
            
            )
        {

            _userManager = userManager;
            _signInManager = signInManager;
           _configuration = configuration; 
                
        }

        public async Task<IdentityResult> SignUpUserAsync(SignUpModel signUpModel) {

            var user = new ApplicationUser() { 
            
            FirstName = signUpModel.FirstName,
            LastName = signUpModel.LastName,    
            Email = signUpModel.Email,  
            UserName = signUpModel.UserName,    

            };
            return await _userManager.CreateAsync(user, signUpModel.Password);
            

        }

       public async  Task<string> LogInAsync(SignInModel signModel ) {

            var result = await _signInManager.PasswordSignInAsync(signModel.Email, signModel.Password, false , false );
            if (result.Succeeded) {
var authClaims  = new List<Claim> { new Claim(ClaimTypes.Name, signModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


                var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignInKey,
                                                               SecurityAlgorithms.HmacSha256Signature)
                    );


              return   new JwtSecurityTokenHandler().WriteToken(token);

            } else {
                return null;
            }
        
        }
    }
}
