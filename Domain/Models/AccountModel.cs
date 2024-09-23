using Domain.Models;
using System.Security.Principal;

namespace Domain.Models
{
    public class AccountModel
    {
        public Guid AccountId { get; set; }
        public required string CBU { get; set; }
        public required string Alias { get; set; }
        public required int NumberAccount { get; set; }
        public required decimal Balance { get; set; }

        public int AccTypeId { get; set; }
        public AccountType AccountType { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public int CurrencyId { get; set; }
        public TypeCurrency TypeCurrency { get; set; }

        public int StateId { get; set; }
        public StateAccount StateAccount { get; set; }
    }
}
