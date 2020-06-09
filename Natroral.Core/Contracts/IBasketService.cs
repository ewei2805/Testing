using System.Collections.Generic;
using System.Web;
using Natroral.Core.Models;
using Natroral.Core.ViewModels;

namespace Natroral.Services
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
        BasketItem FindBaketItem(HttpContextBase httpContext, string itemId);
        void EditBaketItem(HttpContextBase httpContext, BasketItem item, string itemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
        void ClearBasket(HttpContextBase httpContext);
    }   
}