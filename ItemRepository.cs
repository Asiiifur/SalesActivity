using AccumenSalesActivity.Data;
using AccumenSalesActivity.Models.Company;
using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AccumenSalesActivity.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly CompanyDBConnection _cdb;

        public ItemRepository(CompanyDBConnection cdb)
        {
            _cdb = cdb;
        }

        //public async Task<ItemDetailsDTO> GetAItemDetails(ItemStkVM itemStkVM)
        //{
        //    ItemStocksDTO itemStocks = new ItemStocksDTO();
        //    var ItemsId = "";


        //    if (itemStkVM.ItemName != null && itemStkVM.ItemStockID == 0)
        //    {
        //        ItemsId = _cdb.Item.Where(x => x.ItemName == itemStkVM.ItemName || x.ItemId == itemStkVM.ItemName).Select(x => x.ItemId).FirstOrDefault();
        //    }
        //    else if (itemStkVM.ItemName != null && itemStkVM.ItemStockID != 0)
        //    {
        //        var Item = _cdb.Item.Where(x => x.ItemName == itemStkVM.ItemName || x.ItemId == itemStkVM.ItemName).Select(x => x.ItemId).FirstOrDefault();
        //        ItemsId = _cdb.ItemStock.Where(x => x.ItemStockID == itemStkVM.ItemStockID && x.ItemID == Item).Select(x => x.ItemID).FirstOrDefault();
        //    }
        //    else if (itemStkVM.ItemName == null && itemStkVM.ItemStockID != 0)
        //    {
        //        ItemsId = _cdb.ItemStock.Where(x => x.ItemStockID == itemStkVM.ItemStockID).Select(x => x.ItemID).FirstOrDefault();
        //    }


        //    if (!string.IsNullOrEmpty(ItemsId))
        //    {



        //        var query = from itm in _cdb.ItemStock.Where(x => x.ItemID == ItemsId && x.StockLocationID == itemStkVM.StockLocationID && x.StockQuantity != 0)
        //                    join it in _cdb.Item on itm.ItemID equals it.ItemId into iit
        //                    from ittm in iit.DefaultIfEmpty()
        //                    join um in _cdb.UnitOfMeasure on itm.UnitOfMeasureID equals um.UnitOfMeasureId into uoom
        //                    from uom in uoom.DefaultIfEmpty()
        //                    select new ItemStockListDTO
        //                    {
        //                        ItemName = ittm.ItemName,
        //                        Barcode = itm.ItemStockID,
        //                        UnitOfMeasureID = itm.UnitOfMeasureID,
        //                        UnitOfMeasureName = uom.UnitOfMeasureName,
        //                        AvailableStock = itm.StockQuantity,
        //                    };
        //        //itemStocks.itemStockList = await query.ToListAsync().ConfigureAwait(false);
        //        itemStocks.itemStockList = query.ToList();

        //        var itms = await _cdb.Item.Where(x => x.ItemId == ItemsId)
        //                 .Select(x => new
        //                 {
        //                     ItemId = x.ItemId,
        //                     ItemName = x.ItemName ?? "DefaultName"
        //                 }).FirstOrDefaultAsync();

        //        if (itms != null)
        //        {
        //            itemStocks.ItemID = itms.ItemId;
        //            itemStocks.ItemName = itms.ItemName;
        //            itemStocks.StockLocationId = itemStkVM.StockLocationID;
        //            itemStocks.Status = "Success";

        //        }



        //    }
        //    else
        //    {
        //        itemStocks.Status = "Wrong Item / No Data Found";
        //    }

        //    return itemStocks;
        //}
        public async Task<ItemStocksDTO> GetItemStockDetails(string? ItemName , Int64? Barcode, int CompanyId )
        {
            ItemStocksDTO itemStocks = new ItemStocksDTO();
            var ItemsId = "";


            if (ItemName != null && Barcode == null)
            {
                ItemsId = _cdb.Item.Where(x => x.ItemName ==ItemName || x.ItemId == ItemName).Select(x => x.ItemId).FirstOrDefault();
            }
            else if (ItemName != null && Barcode != null)
            {
                var Item = _cdb.Item.Where(x => x.ItemName == ItemName || x.ItemId ==   ItemName).Select(x => x.ItemId).FirstOrDefault();
                ItemsId = _cdb.ItemStock.Where(x => x.ItemStockID == Barcode && x.ItemID == Item).Select(x => x.ItemID).FirstOrDefault();
            }
            else if (ItemName == null && Barcode != null)
            {
                ItemsId = _cdb.ItemStock.Where(x => x.ItemStockID == Barcode).Select(x => x.ItemID).FirstOrDefault();
            }


            if (!string.IsNullOrEmpty(ItemsId))
            {

            

                    var query = from itm in _cdb.ItemStock.Where(x => x.ItemID == ItemsId && x.StockLocationID==    CompanyId && x.StockQuantity !=0)
                                join it in _cdb.Item on itm.ItemID equals it.ItemId into iit
                                from ittm in iit.DefaultIfEmpty()
                                join um in _cdb.UnitOfMeasure on itm.UnitOfMeasureID equals um.UnitOfMeasureId into uoom
                                    from uom in uoom.DefaultIfEmpty()
                                    select new ItemStockListDTO
                                    {
                                        ItemName= ittm.ItemName,
                                        Barcode = itm.ItemStockID,
                                        UnitOfMeasureID = itm.UnitOfMeasureID,
                                        UnitOfMeasureName = uom.UnitOfMeasureName,
                                        AvailableStock = itm.StockQuantity,
                                    };
                //itemStocks.itemStockList = await query.ToListAsync().ConfigureAwait(false);
                itemStocks.itemStockList =  query.ToList();

               var itms = await _cdb.Item.Where(x => x.ItemId == ItemsId) 
                        .Select(x => new
                        { 
                            ItemId = x.ItemId, 
                            ItemName = x.ItemName ?? "DefaultName" 
                        }).FirstOrDefaultAsync();

                if (itms != null)
                {
                    itemStocks.ItemID = itms.ItemId;
                    itemStocks.ItemName = itms.ItemName;
                    itemStocks.StockLocationId = CompanyId;
                    itemStocks.Status = "Success";

                }

            

            }
            else
            {
                itemStocks.Status = "Wrong Item / No Data Found";
            }

            return itemStocks;
        }


    }
}
