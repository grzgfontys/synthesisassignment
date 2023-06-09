using LogicLayer.DTOs;

namespace LogicLayer.Models
{
	public class LineItem
    {
        private int id;
        private Item item;
		private decimal purchasePrice;
		private int amount;

		public LineItem(Item item)
		{
			this.item = item;
			this.purchasePrice = item.Price;
			this.amount = 1;
		}

		public LineItem(Item item, int amount)
		{
			this.item = item;
			this.purchasePrice = item.Price;
			this.amount = amount;
		}
		public LineItem(LineItemDTO lineItemDTO)
		{
			this.id = lineItemDTO.Id;
			this.item = new Item(lineItemDTO.ItemDTO);
			this.purchasePrice = lineItemDTO.PurchasePrice;
			this.amount = lineItemDTO.Amount;
		}

		public decimal TotalPrice()
		{
			return PriceWithoutDiscount() - DiscountProfit();
		}
		public decimal DiscountProfit()
		{
			decimal discountProfit = 0;
			if (Item.Discounts != null)
			{
				foreach (IDiscount discount in Item.Discounts)
				{
					discountProfit += discount.CalculateDiscount(amount, purchasePrice);
				}
			}
			return discountProfit;
		}
		public decimal PriceWithoutDiscount()
		{
			return amount * purchasePrice;
		}
		public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Item Item
		{
			get { return item; }
			set { item = value; }
		}

		public decimal PurchasePrice
		{
			get { return Math.Round(purchasePrice, 2); }
			set { purchasePrice = value; }
		}
		public int Amount
		{
			get { return amount; }
			set { amount = value; }
		}
	}
}
