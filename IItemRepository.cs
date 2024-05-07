using AccumenSalesActivity.Models.Master;
using AccumenSalesActivity.Models.ViewModel;


namespace AccumenSalesActivity.Repository
{
    public interface IItemRepository
    {
       // Task<ItemDetailsDTO> GetALLItemDetails(ItemStkVM itemStkVM);
        Task<ItemStocksDTO> GetItemStockDetails(string? ItemName, Int64? Barcode, int CompanyId);
    }
}

