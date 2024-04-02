namespace EbillApplication.Model
{
    public class EbillProperties
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public double units { get; set; }
        public double charges { get; set; }
        public string site { get; set; }
        public string site1 { get; set; }
        public bool isActive { get; set; }
    }
}
