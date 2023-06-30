namespace FarmerApp.Models.ViewModels.ResponseModels
{
	public class TreatmentResponseModel
	{
		public int Id { get; set; }
		public string DrugName { get; set; }
		public string DrugWeight { get; set; }
		public string TreatedProductsIds { get; set; }
        public DateTime TreatmentDate { get; set; }
    }
}

