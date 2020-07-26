using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using barter_razor.Models;
using barter_razor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace barter_razor.Pages.ItemList
{
    [Authorize]
    public class PostItemModel : PageModel
    {
        private readonly DBConnector DBCommunicator;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public Item Item {get; set;}

        public ItemUploader ItemUploadRecord { get; set; }

        public List<SelectListItem> Categories {get;}

        public PostItemModel(DBConnector DBCommunicator, UserManager<IdentityUser> _userManager)
        {
            this.DBCommunicator = DBCommunicator;
            this.Categories = GetDropDownCategories();
            this._userManager = _userManager;
            this.Item = new Item();
        }

        //Move categories to DB or appsettings
        public List<SelectListItem> GetDropDownCategories()
        {
            return new List<SelectListItem>
            {
                new SelectListItem(){ Text = "Please Select", Selected = true, Disabled = true },
                new SelectListItem(){ Text = "Book", Value = "Book" },
                new SelectListItem(){ Text = "Tools", Value = "Tools" },
                new SelectListItem(){ Text = "Camping", Value = "Camping" },
                new SelectListItem(){ Text = "Computing", Value = "Computing" },
                new SelectListItem(){ Text = "Kitchenware", Value = "Kitchenware" },
                new SelectListItem(){ Text = "Applicances", Value = "Applicances" },
                new SelectListItem(){ Text = "Home Entertainment", Value = "Home Entertainment" },
                new SelectListItem(){ Text = "Other", Value = "Other" }
            };
        }
        private async Task<IdentityUser> getCurrentUser()
        {
            if(User.Identity.IsAuthenticated)
            {
                return await this._userManager.GetUserAsync(HttpContext.User);
            }
            else{ return null;}
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                //Creates Item Upload record as global attribute
                createItemUploadRecord();
                //This adds Model Insert Query to DB context Queue 
                await DBCommunicator.AddAsync(ItemUploadRecord);
                //This executes DB context Queue queries 
                await DBCommunicator.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        private async void createItemUploadRecord()
        {
            var user = await getCurrentUser();

            ItemUploadRecord = new ItemUploader();
            ItemUploadRecord.copyItem(Item);
            ItemUploadRecord.IdentityUser = user;
        }
    }
}
