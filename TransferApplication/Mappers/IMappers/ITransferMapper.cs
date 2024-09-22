using Domain.Models;

using TransferApplication.Response;

namespace TransferApplication.Mappers.IMappers
{
    public interface ITransferMapper
    {
        Task<TransferResponse> GetOneTransfer(Transfer transfer);
        Task<List<TransferResponse>> GetTransfers(Transfer transfer);
    }
}
