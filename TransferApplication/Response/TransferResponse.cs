using Domain.Models;

namespace TransferApplication.Response
{
    public class TransferResponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } //podriamos tener un objeto de tipo State?
        public string Description { get; set; }
        public int TypeId { get; set; }
        public AccountModel SrcAccount { get; set; }
        public AccountModel DestAccount { get; set; }
    }
}
