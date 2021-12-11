using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class AccountRepository : IAccountRepository
    {

        public UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
                
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

            var result = await _signInManager.PasswordSignInAsync(signModel.Email, signModel.Password, true  );
        
        }
    }
}
