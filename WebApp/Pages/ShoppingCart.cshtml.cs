using DataAccessLayer;
using LogicLayer.InterfacesManagers;
using LogicLayer.InterfacesRepository;
using LogicLayer.Managers;
using LogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    [Authorize]
    public class ShoppingCartModel : PageModel
    {
        [BindProperty]
        public int LineItemId { get; set; }

        IShoppingCartManager shoppingCartManager;
        IShoppingCartRepository shoppingCartRepository;
        public ShoppingCart shoppingCart;

        public ShoppingCartModel()
        {
            shoppingCartRepository = new ShoppingCartRepository();
            shoppingCartManager = new ShoppingCartManager(shoppingCartRepository);
        }

        public void OnGet()
        {
            shoppingCart = shoppingCartManager.ReadShoppingCart(int.Parse(User.FindFirst("Id").Value));
			shoppingCartManager.UpdateShoppingCartItems(int.Parse(User.FindFirst("Id").Value), shoppingCart);
		}

        public void OnPostDeleteItem()
		{
			shoppingCart = shoppingCartManager.ReadShoppingCart(int.Parse(User.FindFirst("Id").Value));
			shoppingCartManager.UpdateShoppingCartItems(int.Parse(User.FindFirst("Id").Value), shoppingCart);

			LineItem deletedLineItem = shoppingCart.AddedItems.Find(x => x.Id == LineItemId);

			shoppingCartManager.DeleteShoppingCart(deletedLineItem);
            shoppingCart.AddedItems.Remove(deletedLineItem);
        }

        public IActionResult OnPostPlaceOrder()
		{
			return RedirectToPage("/Shipping");
		}
    }
}
