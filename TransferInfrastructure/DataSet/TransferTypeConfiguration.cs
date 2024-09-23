using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferInfrastructure.DataSet
{
    public class TransferTypeConfiguration : IEntityTypeConfiguration<TransferType>
    {
        public void Configure(EntityTypeBuilder<TransferType> builder)
        {
            builder.HasData(
                new TransferType { Id = 1 , Name = "Nacional" },
                new TransferType { Id = 2, Name = "Internacional" }
            );
        }
    }
}
