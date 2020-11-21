using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WiredBrainCoffee.Models;

namespace WiredBrainCoffee.Services
{
    public class FileService
    {
        private readonly HttpClient http;

        public FileService(HttpClient http)
        {
            this.http = http;
        }

        public async Task UploadFile(IBrowserFile file)
        {
            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);

            await http.PostAsJsonAsync("Contact/upload", buffer);
        }
    }
}
