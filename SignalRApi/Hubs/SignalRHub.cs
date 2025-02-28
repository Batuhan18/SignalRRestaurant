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
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

        public SignalRHub(ICategoryService categoryService, IProductService productsService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService)
        {
            _categoryService = categoryService;
            _productsService = productsService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _menuTableService = menuTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }
        public static int clientCount { get; set; } = 0;

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

        public async Task GetBookingList()
        {
            var value = _bookingService.TGetListAll();
            await Clients.All.SendAsync("ReceiveBookingList", value);
        }

        public async Task SendNotification()
        {
            var values = _notificationService.TNotificationCountByStatusFalse();
            await Clients.All.SendAsync("ReceiveNotificationCountByStatusFalse", values);

            var value = _notificationService.TGetAllNotificationByFalse();
            await Clients.All.SendAsync("ReceiveGetAllNotificationByFalse", value);
        }

        public async Task GetMenuTableStatus()
        {
            var value = _menuTableService.TGetListAll();
            await Clients.All.SendAsync("ReceiveMenuTableStatus", value);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            clientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
