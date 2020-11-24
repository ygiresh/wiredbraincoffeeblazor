using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using WiredBrainCoffee.Models;

namespace WiredBrainCoffee.Services
{
    public interface IContactService
    {
        Task SubmitContact(Contact contact, IReadOnlyList<IBrowserFile> files);
    }
}