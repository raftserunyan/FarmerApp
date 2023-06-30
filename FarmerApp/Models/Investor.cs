namespace FarmerApp.Models
{
	public class Investor
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string PhoneNumber { get; set; }
		public int InvestedAmount { get; set; }
        public DateTime InvestedDate { get; set; }
    }
}

