﻿using DataAccessLayer;
using LogicLayer.InterfacesManagers;
using LogicLayer.InterfacesRepository;
using LogicLayer.Managers;
using LogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public bool? OrderSuccess { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
			//IOrderRepository orderRepository;
			//IOrderManager orderManager;

			//orderRepository = new OrderRepository();
			//orderManager = new OrderManager(orderRepository);

			//Order order = orderManager.ReadOrderByClientIdOrderId(2, 4);
		}
	}
}