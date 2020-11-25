using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiredBrainCoffee.Models;
using WiredBrainCoffee.Services;
using WiredBrainCoffeeClient.Components;

namespace WiredBrainCoffee.Client.Pages
{
    public partial class Order
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IMenuService MenuService { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }
        
        [CascadingParameter] 
        public IModalService Modal { get; set; }

        public List<OrderItem> CurrentOrder { get; set; } = new List<OrderItem>();
        public List<MenuItem> FoodMenuItems { get; set; } = new List<MenuItem>();
        public List<MenuItem> CoffeeMenuItems { get; set; } = new List<MenuItem>();
        public decimal OrderTotal { get; set; } = 0;
        public decimal SalesTax { get; set; } = 0.06m;
        private string PromoCode { get; set; } = "";
        private bool FoodHidden { get; set; } = true;
        private bool IsValidPromoCode { get; set; } = false;
        private bool CoffeeHidden { get; set; } = false;

        public async Task CheckPromoCode()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/promocode.js");
            IsValidPromoCode = await module.InvokeAsync<bool>("CheckPromoCode", PromoCode);
        }

        private void ShowCoffee()
        {
            CoffeeHidden = false;
            FoodHidden = true;
        }

        private void ShowFood()
        {
            CoffeeHidden = true;
            FoodHidden = false;
        }

        async Task AddExtras(MenuItem item)
        {
            item.Extras = new Extras();
            var formModal = Modal.Show<CoffeeExtrasModal>("Enhance Your Coffee");
            var result = await formModal.Result;

            if (!result.Cancelled)
            {
                item.Extras = (Extras)result.Data;
                AddToOrder(item);
            }
        }

        public void AddToOrder(MenuItem item)
        {
            CurrentOrder.Add(new OrderItem()
            {
                Name = item.Name,
                Id = item.Id,
                Price = item.Price,
                Extras = item.Extras
            });

            OrderTotal += item.Price;
        }

        public void RemoveFromOrder(OrderItem item)
        {
            CurrentOrder.Remove(item);
            OrderTotal -= item.Price;
        }

        public void PlaceOrder()
        {
            NavManager.NavigateTo("order-confirmation");
        }


        IJSObjectReference module;
        protected async override Task OnInitializedAsync()
        {
            var menuItems = await MenuService.GetMenuItems();

            FoodMenuItems = menuItems.Where(x => x.Category == "Food").ToList();
            CoffeeMenuItems = menuItems.Where(x => x.Category == "Coffee").ToList();
        }
    }
}
