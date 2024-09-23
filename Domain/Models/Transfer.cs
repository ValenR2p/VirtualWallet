namespace Domain.Models
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } //podriamos tener un objeto de tipo State?
        public string Description { get; set; }

        public int TypeId { get; set; }
        public TransferType TransferType { get; set; }

        public Guid SrcAccountId { get; set; }
        public AccountModel SrcAccount { get; set; }

        public Guid DestAccountId { get; set; }
        public AccountModel DestAccount { get; set; }
    }
}
