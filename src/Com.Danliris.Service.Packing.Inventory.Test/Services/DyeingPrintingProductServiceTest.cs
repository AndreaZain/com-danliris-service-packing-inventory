﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingProduct;
using Com.Danliris.Service.Packing.Inventory.Data.Models.DyeingPrintingAreaMovement;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.Repositories.DyeingPrintingAreaMovement;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Services
{
    public class DyeingPrintingProductServiceTest
    {
        public DyeingPrintingProductService GetService(IServiceProvider serviceProvider)
        {
            return new DyeingPrintingProductService(serviceProvider);
        }

        public Mock<IServiceProvider> GetServiceProvider(IDyeingPrintingAreaOutputProductionOrderRepository repo)
        {
            var spMock = new Mock<IServiceProvider>();

            spMock.Setup(s => s.GetService(typeof(IDyeingPrintingAreaOutputProductionOrderRepository)))
                .Returns(repo);

            return spMock;
        }

        private DyeingPrintingProductPackingViewModel ViewModel
        {
            get
            {
                return new DyeingPrintingProductPackingViewModel()
                {
                    Color = "c",
                    FabricPackingId = 1,
                    FabricSKUId = 1,
                    HasPrintingProductPacking = true,
                    HasPrintingProductSKU = true,
                    Id = 1,
                    Material = new Application.CommonViewModelObjectProperties.Material()
                    {
                        Id = 1,
                        Name = "s"
                    },
                    MaterialConstruction = new Application.CommonViewModelObjectProperties.MaterialConstruction()
                    {
                        Id = 1,
                        Name = "s"
                    },
                    MaterialWidth = "1",
                    Motif = "s",
                    ProductionOrder = new Application.CommonViewModelObjectProperties.ProductionOrder()
                    {
                        Id = 1,
                        No = "s"
                    },
                    ProductPackingCodes = new List<string>() { "s" },
                    ProductPackingId = 1,
                    ProductSKUCode = "s",
                    ProductSKUId = 1,
                    Quantity = 1,
                    ProductPackingLength = 1,
                    ProductPackingType = "s",
                    UomUnit = "s",
                    YarnMaterial = new Application.ToBeRefactored.CommonViewModelObjectProperties.YarnMaterial()
                    {
                        Id = 1,
                        Name = "s"
                    }
                };
            }
        }

        private DyeingPrintingAreaOutputProductionOrderModel Model
        {
            get
            {
                var model = new DyeingPrintingAreaOutputProductionOrderModel("PACKING", "GUDANG JADI", true, ViewModel.ProductionOrder.Id, ViewModel.ProductionOrder.No, ViewModel.ProductionOrder.Type, ViewModel.ProductionOrder.OrderQuantity, "", "", "", "",
                     "", ViewModel.Color, ViewModel.Motif, ViewModel.UomUnit, "", "", "", 0, ViewModel.Id, 0, ViewModel.Material.Id, ViewModel.Material.Name, ViewModel.MaterialConstruction.Id, ViewModel.MaterialConstruction.Name,
                     ViewModel.MaterialWidth, "", ViewModel.Quantity, "", ViewModel.ProductPackingType, 0, "", "", 0, "", ViewModel.YarnMaterial.Id, ViewModel.YarnMaterial.Name, ViewModel.ProductSKUId, ViewModel.FabricSKUId, ViewModel.ProductSKUCode,
                    ViewModel.HasPrintingProductSKU, ViewModel.ProductPackingId, ViewModel.FabricPackingId, string.Join(',', ViewModel.ProductPackingCodes), ViewModel.HasPrintingProductPacking, ViewModel.ProductPackingLength);


                model.DyeingPrintingAreaOutput = new DyeingPrintingAreaOutputModel(DateTimeOffset.UtcNow, "PACKING", "s", "s", false, "GUDANG JADI", "S", "OUT", new List<DyeingPrintingAreaOutputProductionOrderModel>() { model });

                return model;
            }
        }

        [Fact]
        public void Should_Success_Read()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.ReadAll())
                 .Returns(new List<DyeingPrintingAreaOutputProductionOrderModel>() { Model }.AsQueryable());

            repoMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = service.GetDataProductPacking(1, 25, "{}", "{}", null);

            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task Should_Success_UpdatePrintingPacking()
        {
            var repoMock = new Mock<IDyeingPrintingAreaOutputProductionOrderRepository>();


            repoMock.Setup(s => s.UpdateHasPrintingProductPacking(It.IsAny<int>(), It.IsAny<bool>()))
                 .ReturnsAsync(1);

            var service = GetService(GetServiceProvider(repoMock.Object).Object);

            var result = await service.UpdatePrintingStatusProductPacking(ViewModel.Id, true);

            Assert.NotEqual(0, result);
        }
    }
}
