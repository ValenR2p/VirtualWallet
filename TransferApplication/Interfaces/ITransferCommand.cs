using Domain.Models;

namespace TransferApplication.Interfaces
{
    public interface ITransferCommand
    {
        public Task InsertTransfer(Transfer transfer);
        public Task CompleteTransfer(AccountModel srcAccount, AccountModel destAccount);
        public Task DeleteTransfer(Transfer transfer);
        public Task UpdateTransfer(Transfer transfer);
    }
}
