namespace DAL.Models
{
    public class Step
    {
        public int Id { get; set; }
        public Document Document { get; set; }
        public User User { get; set; }
        public bool? Status { get; set; }
        public int SerialNumber { get; set; }
    }
}