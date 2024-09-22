using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferApplication.Request
{
    public class CreateTransferRequest
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        //public string Status { get; set; } //podriamos tener un objeto de tipo State?
        public string Description { get; set; }
        public int TypeId { get; set; }
        public Guid SrcAccountId { get; set; }
        public Guid DestAccountId { get; set; }
    }
}
