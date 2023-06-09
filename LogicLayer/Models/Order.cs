using LogicLayer.DTOs;

namespace LogicLayer.Models
{
	public class Order
	{
		private int id;
		private Client client;
		private int? orderBonusPoints;
		private int? orderSpentBonusPoints;
		private DateOnly orderDate;
		private DateOnly? deliveryDate;
		private OrderStatus orderStatus;
		private PaymentMethod paymentMethod;
		private Address address;
        private List<LineItem> purchasedItems;

        public Order()
		{

		}
		public Order(OrderDTO orderDTO)
		{
			this.id = orderDTO.Id;
			this.client = new Client(orderDTO.clientDTO);
			this.orderBonusPoints = orderDTO.OrderBonusPoints;
			this.orderSpentBonusPoints = orderDTO.OrderSpentBonusPoints;
			this.orderDate = orderDTO.OrderDate;
			this.deliveryDate = orderDTO.DeliveryDate;
			this.orderStatus = (OrderStatus)orderDTO.OrderStatus;
			this.paymentMethod = (PaymentMethod)orderDTO.PaymentMethod;
			this.purchasedItems = new List<LineItem>();
			foreach (LineItemDTO lineItemDTO in orderDTO.PurchasedItems)
			{
				this.purchasedItems.Add(new LineItem(lineItemDTO));
			}
			this.address = new Address(orderDTO.AddressDTO);
		}

		public Order(Client client, int? orderBonusPoints, int? orderSpentBonusPoints, DateOnly orderDate, DateOnly? deliveryDate, OrderStatus orderStatus, PaymentMethod paymentMethod, Address address, List<LineItem> purchasedItems)
		{
			this.client = client;
			this.orderBonusPoints = orderBonusPoints;
			this.orderSpentBonusPoints = orderSpentBonusPoints;
			this.orderDate = orderDate;
			this.deliveryDate = deliveryDate;
			this.orderStatus = orderStatus;
			this.paymentMethod = paymentMethod;
			this.address = address;
			this.purchasedItems = purchasedItems;
		}


		public decimal TotalPrice()
		{
			return Math.Round(PriceWithoutDiscount() - DiscountProfit(), 2);
		}
		public decimal DiscountProfit()
		{
			decimal discountProfit = 0;
			foreach (LineItem lineItem in purchasedItems)
			{
				discountProfit += lineItem.DiscountProfit();
			}
			return Math.Round(discountProfit, 2);
		}
		public decimal PriceWithoutDiscount()
		{
			decimal priceWithoutDiscount = 0;
			foreach (LineItem lineItem in purchasedItems)
			{
				priceWithoutDiscount += lineItem.PriceWithoutDiscount();
			}
			return Math.Round(priceWithoutDiscount, 2);
		}
		public int? TotalBonusPointsAfterOrder()
		{
			if (orderBonusPoints != null&& client.BonusCard != null && orderSpentBonusPoints != null )
				return orderBonusPoints + client.BonusCard.AmountOfPoints - orderSpentBonusPoints;
			else
				return null;
		}
		public int Id { get => id; set => id = value; }
		public Client Client { get => client; set => client = value; }
		public int? OrderBonusPoints { get => orderBonusPoints; set => orderBonusPoints = value; }
		public int? OrderSpentBonusPoints { get => orderSpentBonusPoints; set => orderSpentBonusPoints = value; }
		public DateOnly OrderDate { get => orderDate; set => orderDate = value; }
		public DateOnly? DeliveryDate { get => deliveryDate; set => deliveryDate = value; }
		public OrderStatus OrderStatus { get => orderStatus; set => orderStatus = value; }
		public PaymentMethod PaymentMethod { get => paymentMethod; set => paymentMethod = value; }
		public Address Address { get => address; set => address = value; }
		public List<LineItem> PurchasedItems { get => purchasedItems; set => purchasedItems = value; }
	}
}
