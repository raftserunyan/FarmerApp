namespace FarmerApp.Models
{
	public class Treatment
	{
		public int Id { get; set; }
		public string DrugName { get; set; }
		public string DrugWeight { get; set; }
		public string TreatedProductsIds { get; set; }
        public DateTime TreatmentDate { get; set; }
    }
}