﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LeaseIt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeaseIt.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First Name")]
            [StringLength(10)]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            [StringLength(10)]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Address Line 1")]
            [StringLength(20)]
            public string StreetAdress { get; set; }

            [ Display(Name = "City")]
            [StringLength(20)]
            public string City { get; set; }

            [Display(Name = "Province")]
            [StringLength(20)]
            public string Province { get; set; }

            [Display(Name = "Country")]
            [StringLength(20)]
            public string Country { get; set; }

            [Display(Name = "Postal Code")]
            [DataType(DataType.PostalCode)]
            [StringLength(7, MinimumLength = 5, ErrorMessage = "Please enter a valid Postal Code")]
            public string ZipCode { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName= user.FirstName,
                LastName= user.LastName,
                StreetAdress= user.StreetAdress,
                City= user.City,
                Province =user.Province,
                Country = user.Country,
                ZipCode = user.ZipCode
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if(Input.FirstName != user.FirstName) 
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }

            if (Input.StreetAdress != user.StreetAdress)
            {
                user.StreetAdress = Input.StreetAdress;
                await _userManager.UpdateAsync(user);
            }

            if (Input.City != user.City)
            {
                user.City = Input.City;
                await _userManager.UpdateAsync(user);
            }

            if (Input.Province != user.Province)
            {
                user.Province = Input.Province;
                await _userManager.UpdateAsync(user);
            }

            if (Input.Country != user.Country)
            {
                user.Country = Input.Country;
                await _userManager.UpdateAsync(user);
            }
            if (Input.ZipCode != user.ZipCode)
            {
                user.ZipCode = Input.ZipCode;
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
