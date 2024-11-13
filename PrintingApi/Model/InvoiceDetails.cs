namespace PrintingApi.Model
{
    public class InvoiceDetails
    {
        public int Id { get; set; }  
        public string? Company {  get; set; }
        public string? StreetAddress { get; set; }
        public string? CityZipCode { get; set; }
        public string? Website { get; set; } 
        public string? Date { get; set; } 
        public string? ProductName { get; set; }

        public double? Price { get; set; }  
    }
}
