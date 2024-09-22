namespace Domain.Models
{
    public class AccountType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<AccountModel> Accounts { get; set; }
    }
}
