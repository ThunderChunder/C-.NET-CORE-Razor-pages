using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using barter_razor.Models;
using barter_razor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace barter_razor.Pages.ItemList
{
    public class IndexModel : PageModel
    {
        private readonly DBConnector DBCommunicator;
        public List<ItemUploader> Items { get; set;} 
        public IndexModel(DBConnector DBCommunicator)
        {
            this.DBCommunicator = DBCommunicator;
        }

        public async Task OnGet()
        {
            Items = await DBCommunicator.ItemUploaderRecords.ToListAsync();
            await DBCommunicator.UserNames.ToListAsync();
        }
    }
}
