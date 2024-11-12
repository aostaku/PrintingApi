namespace PrintingApi.Model
{
    public class InvoiceDetails
    {
        public int Id { get; set; }  
        public string? Company {  get; set; }
        public string? StreetAdress { get; set; }
        public string? CityZipCode { get; set; }
        public string? Website { get; set; } 
        public DateTime? Date { get; set; } 
        public string? ProductName { get; set; }

        public double? Price { get; set; }  

    }
}
