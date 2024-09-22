using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferApplication.Interfaces;
using TransferInfrastructure.Persistence;

namespace TransferInfrastructure.Command
{
    public class TransferCommand : ITransferCommand
    {
        private readonly TransferContext _context;
        public TransferCommand(TransferContext context)
        {
            _context = context;
        }

        public async Task CompleteTransfer(AccountModel srcAccount, AccountModel destAccount)
        {
            //Este metodo tendria que ir en AccountCommand
            _context.Update(srcAccount);
            _context.Update(destAccount);
            await _context.SaveChangesAsync();
            //return true;
            //throw new NotImplementedException();
        }

        public async Task DeleteTransfer(Transfer transfer)
        {
            _context.Remove(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task InsertTransfer(Transfer transfer)
        {
            _context.Add(transfer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransfer(Transfer transfer)
        {
            _context.Update(transfer);
            await _context.SaveChangesAsync();
        }
    }
}
