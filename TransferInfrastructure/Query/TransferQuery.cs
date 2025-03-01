﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using TransferApplication.Interfaces;
using TransferInfrastructure.Persistence;

namespace TransferInfrastructure.Query
{
    public class TransferQuery : ITransferQuery
    {
        private readonly TransferContext _context;
        public TransferQuery(TransferContext context)
        {
            _context = context;
        }

        public List<Transfer> GetAllTransfers()
        {
            return _context.Transfers.ToList();
        }

        public async Task<bool> GetPendingTransfer(Guid UserId)
        {
            var status = false;

            var transfer = _context.Transfers.Where(t => t.Status == "Pending")
                .FirstOrDefault(t => t.SrcAccountId == UserId);

            //Se busca si hay alguna transferencia pendiente y se compara si se encontro algo
            if (transfer != null)
            {
                status = true;
            }
            return status;
        }
        public async Task<Transfer> GetTransferById(Guid Id)
        {
            var project = _context.Transfers.
                FirstOrDefault(s => s.Id == Id);
            return project;
        }
        public async Task<List<Transfer>> GetUserTransfers(Guid UserId)
        {
            return await _context.Transfers.Where(t => t.SrcAccountId == UserId).ToListAsync();
        }
    }
}
