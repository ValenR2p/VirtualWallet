using Azure.Core;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferApplication.Interfaces;
using TransferApplication.Mappers.IMappers;
using TransferApplication.Request;
using TransferApplication.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TransferApplication.UseCases
{
    public class TransferServices : ITransferServices
    {
        private readonly ITransferCommand _command;
        private readonly ITransferQuery _query;
        //private readonly ITransferMapper _mapper;

        //Necesitaria acceder a la Cuenta que realiza la transferencia y verificar en la lista de transferencias,
        //si hay alguna a su nombre que este pendiente

        //NECESITO EL ACCOUNTSERVICES PROBABLEMENTE
        //public TransferServices(ITransferCommand command, ITransferQuery query, ITransferMapper mapper) { 
        public TransferServices(ITransferCommand command, ITransferQuery query) { 
            _command = command;
            _query = query;            
        }
        public async Task<TransferResponse> CreateTransfer(CreateTransferRequest request)
        {
            //Hace falta una verificacion de si existe el destino de la transferencia
            //tengo que tener un metodo que me permita obtener una cuanta por su Id
            var srcAccount = new AccountModel {
                AccountId = new Guid(),
                Alias = "AliasSrc",
                CBU = "CBUSrc",
                NumberAccount = 1,
                Balance = 2500,
            };
            var destAccount = new AccountModel
            {
                AccountId = new Guid(),
                Alias = "AliasDest",
                CBU = "CBUDest",
                NumberAccount = 2,
                Balance = 0,
            };
            //var srcAccount = _accountServices.GetById(request.SrcAccountId)
            //var destAccount = _accountServices.GetById(request.DestAccountId)
            //Verificar si existe el destinatario

            var transfer = new Transfer
            {
                Amount = request.Amount,
                Date = request.Date,
                Status = "Pending",
                Description = request.Description,
                TypeId = request.TypeId,
                SrcAccountId = request.SrcAccountId,
                DestAccountId = request.DestAccountId,
            };
            //await MakeTransfer(transfer, srcAccount, destAccount);
            await _command.InsertTransfer(transfer);

            if (await MakeTransfer(transfer, srcAccount, destAccount) == false) {
                transfer.Status = "Canceled";
                //_command.UpdateTransfer(transfer):
            }
            transfer.Status = "Completed";
            //_command.UpdateTransfer(transfer):


            //Que seria mejor, que se inserte la transferencia en la DB una vez se cancela o se termina, o guardarla aunque este pendiente?
            //await _command.InsertTransfer(transfer);
            return new TransferResponse
            {
                Id = transfer.Id,
                Amount = transfer.Amount,
                Date = transfer.Date,
                Status = transfer.Status,
                Description = transfer.Description,
                TypeId = transfer.TypeId,
                SrcAccount = srcAccount,
                DestAccount = destAccount
            };
            //throw new NotImplementedException();
        }
        public async Task<bool> MakeTransfer(Transfer transfer, AccountModel srcAccount, AccountModel destAccount)
        {
            if (await _query.GetPendingTransfer(transfer.SrcAccountId) == false) {
                //Verificar si se tiene saldo suficiente y otras cuestiones mas
                
                //If (verificar que se haga bien la transferencia, si esta bloqueada la cuenta o si se cancela por X motivo la transferencia)

                //var estadoDest = await _accountServices.GetEstado(destAccount.StateId);
                //var estadoSrc = await _accountServices.GetEstado(srcAccount.StateId);
                //if (estadoDest == bloqueado,no habilitado,etc OR estadoSrc == bloqueado,no habilitado,etc)
                //{
                //return false;
                //}
                srcAccount.Balance -= transfer.Amount;
                destAccount.Balance += transfer.Amount;
                await _command.CompleteTransfer(srcAccount, destAccount);
                return true;
            }
            return false;
            //throw new NotImplementedException();
        }
        public Task<TransferResponse> UpdateTransfer(CreateTransferRequest request)
        {

            // Buscar primero si existe la transferencia y despues si pisar los datos.

            var transfer = new Transfer {
                Amount = request.Amount,
                Date = request.Date,
                Status = "",
                Description = request.Description,
                TypeId = request.TypeId,
                SrcAccountId = request.SrcAccountId,
                DestAccountId = request.DestAccountId,
            };
            _command.UpdateTransfer(transfer);
            throw new NotImplementedException();
        }

        public Task<TransferResponse> DeleteTransfer(Transfer transfer)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransferResponse>> GetAll()
        {
            var tranfersList = _query.GetAllTransfers();

            List<TransferResponse> transfersListResponse = new List<TransferResponse>();

            foreach (var transfer in tranfersList)
            {
                var transferData = new TransferResponse
                {
                    Id = transfer.Id,
                    Amount = transfer.Amount,
                    Date = transfer.Date,
                    Status = transfer.Status,
                    Description = transfer.Description,
                    TypeId = transfer.TypeId,
                    SrcAccount = transfer.SrcAccount,
                    DestAccount = transfer.DestAccount,
                };

                transfersListResponse.Add(transferData);
            }

            return transfersListResponse;
        }

        public Task<List<TransferResponse>> GetAllByUser(Guid id)
        {
            throw new NotImplementedException();
        }

        
    }
}
