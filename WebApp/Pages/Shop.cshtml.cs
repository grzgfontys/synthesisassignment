using DataAccessLayer;
using LogicLayer.InterfacesManagers;
using LogicLayer.InterfacesRepository;
using LogicLayer.Managers;
using LogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text.Json;

namespace WebApp.Pages
{
    public class ShopModel : PageModel
    {
        IItemRepository itemRepository;
        IItemManager itemManager;
		IItemCategoryRepository itemCategoryRepository;
		IItemCategoryManager itemCategoryManager;
		IShoppingCartRepository shoppingCartRepository;
		IShoppingCartManager shoppingCartManager;

		public List<Item> items;

		[BindProperty]
		public int ItemId { get; set; }
		[BindProperty]
		public int Amount { get; set; }
		[BindProperty]
		public string mess { get; set; }

		ShoppingCart shoppingCart;


		public ShopModel()
		{
			itemRepository = new ItemRepository();
			itemManager = new ItemManager(itemRepository);
			itemCategoryRepository = new ItemCategoryRepository();
			itemCategoryManager = new ItemCategoryManager(itemCategoryRepository);
			shoppingCartRepository = new ShoppingCartRepository();
			shoppingCartManager = new ShoppingCartManager(shoppingCartRepository);

			items = itemManager.ReadAvailableItems();
		}

        public void OnGet()
        {

		}
		public void OnPost()
		{

		}
		public void OnPostAddItem()
		{
			bool success;

			Item addedItem = items.Find(x => x.Id == ItemId);

			if (Amount > 0 && Amount <= addedItem.StockAmount)
			{
				shoppingCart = shoppingCartManager.ReadShoppingCart(int.Parse(User.FindFirst("Id").Value));

				LineItem addedLineItem = new LineItem(addedItem, Amount);

				if (shoppingCart.IsAddedLineItemNew(addedLineItem))
				{
					success = shoppingCartManager.CreateShoppingCartItem(int.Parse(User.FindFirst("Id").Value), addedLineItem);
				}
				else
				{
					success = shoppingCartManager.UpdateShoppingCartItem(shoppingCart.LastUpdatedLineItem);
				}
				if(success)
				{
					mess = "Item added to shopping cart";
				}
				else
				{
					mess = "Couldn't add item to shopping cart";
				}
			}
		}
	}
}
