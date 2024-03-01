using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace MaldsKeeperWebApp.Controllers
{
    public class DonateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessDonation(string email, long amount)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
            {
                "card",
            },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = amount,
                        Currency = "pln",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Invisible Book",
                            Description = "A revolutionary reading experience where imagination is your only limit.",
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "https://localhost:7182/donate/success",
                CancelUrl = "https://localhost:7182/donate/cancel",
                CustomerEmail = email,
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Redirect(session.Url);
        }
    }
}
