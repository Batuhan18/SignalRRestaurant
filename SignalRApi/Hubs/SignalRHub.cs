using Microsoft.AspNetCore.SignalR;
using SignalR.BussinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;

namespace SignalRApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productsService;
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMenuTableService _menuTableService;

        public SignalRHub(ICategoryService categoryService, IProductService productsService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService)
        {
            _categoryService = categoryService;
            _productsService = productsService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _menuTableService = menuTableService;
        }

        public async Task SendStatistic()
        {
            var values = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", values);

            var values1 = _productsService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", values1);

            var values2 = _categoryService.TActiveCategoryCount();
            var values3 = _categoryService.TPasiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", values2);
            await Clients.All.SendAsync("ReceivePasiveCategoryCount", values3);

            var values4 = _productsService.TProductCountByCategoryNameHamburger();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameHamburger", values4);

            var values5 = _productsService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveProductCountByCategoryNameDrink", values5);

            var values6 = _productsService.TProductPriceAvg();
            await Clients.All.SendAsync("ReceiveProductPriceAvg", values6.ToString("0.00") + "₺");

            var values7 = _productsService.TProductNamePriceByMax();
            await Clients.All.SendAsync("ReceiveProductNamePriceByMax", values7);

            var values8 = _productsService.TProductNamePriceByMin();
            await Clients.All.SendAsync("ReceiveProductNamePriceByMin", values8);

            var values9 = _productsService.TProductPriceByHamburger();
            await Clients.All.SendAsync("ReceiveProductPriceByHamburger", values9.ToString("0.00") + "₺");

            var values10 = _orderService.TTotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", values10);

            var values11 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", values11);

            var values12 = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", values12.ToString("0.00") + "₺");

            var values13 = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", values13.ToString("0.00") + "₺");

            var values15 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", values15);
        }

        public async Task SendProgress()
        {
            var values = _moneyCaseService.TTotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", values.ToString("0.00") + "₺");


            var values1 = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", values1);

            var values2 = _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", values2);
        }


    }
}
