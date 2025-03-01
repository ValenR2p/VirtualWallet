﻿using Domain.Models;

namespace TransferApplication.Interfaces
{
    public interface ITransferQuery
    {
        Task<List<Transfer>> GetUserTransfers(Guid UserId);
        Task<Transfer> GetTransferById(Guid id);
        Task<bool> GetPendingTransfer(Guid UserId);
        List<Transfer> GetAllTransfers();

    }
}
