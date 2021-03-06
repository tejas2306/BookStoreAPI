using BookStore.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IAccountRepository
    {

        Task<IdentityResult> SignUpUserAsync(SignUpModel signUpModel);
        Task<string> LogInAsync(SignInModel signModel);
    }
}
