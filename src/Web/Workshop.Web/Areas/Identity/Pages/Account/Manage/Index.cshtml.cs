namespace Workshop.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using Workshop.Data.Models;
    using Workshop.Services.Cloudinary;
    using Workshop.Services.Data.HashProvider;
    using Workshop.Services.Data.Users;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IUsersService usersService;
        private readonly IHashProvider hashProvider;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICloudinaryService cloudinaryService,
            IUsersService usersService,
            IHashProvider hashProvider)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.cloudinaryService = cloudinaryService;
            this.usersService = usersService;
            this.hashProvider = hashProvider;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            if (this.Input.AvatarFile != null)
            {
                using var ms = new MemoryStream();
                this.Input.AvatarFile.CopyTo(ms);
                var destinationData = ms.ToArray();
                var currentAvatarHash = this.usersService.GetAvatarHash(user.Id);
                var newAvatarHash = this.hashProvider.HashOfGivenByteArray(destinationData);
                if (currentAvatarHash == newAvatarHash)
                {
                    this.StatusMessage = "You are trying to set same avatar as old one.";
                    await this.LoadAsync(user);
                    return this.RedirectToPage();
                }

                var pictureUrl = await this.cloudinaryService.UploadPictureAsync(destinationData, "Avatar", "Avatars", 250, 250);
                await this.usersService.ChangeAvatarAsync(user.Id, pictureUrl, newAvatarHash);
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            this.Username = userName;

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
            };
        }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Avatar")]
            public IFormFile AvatarFile { get; set; }
        }
    }
}
