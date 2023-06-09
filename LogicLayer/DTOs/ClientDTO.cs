namespace LogicLayer.DTOs
{
	public class ClientDTO
	{
		public int Id
		{
			get;
			set;
		}

		public string Firstname
		{
			get;
			set;
		}

		public string Lastname
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public string Password
		{
			get; set;
		}

		public string Salt
		{
			get; set;
		}

		public string Username
		{
			get; set;
		}
		public int? AmountOfPoints
		{
			get;
			set;
		}

		public AddressDTO? AddressDTO
		{
			get;
			set;
		}
	}
}
