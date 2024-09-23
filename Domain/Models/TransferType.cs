namespace Domain.Models
{
    public class TransferType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transfer> Transfers { get; set; }
    }
}
