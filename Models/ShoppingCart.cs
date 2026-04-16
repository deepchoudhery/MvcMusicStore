using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Models
{
    public partial class ShoppingCart
    {
        private readonly MusicStoreEntities _dbContext;

        private ShoppingCart(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        string ShoppingCartId { get; set; } = string.Empty;

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(Controller controller)
        {
            var dbContext = controller.HttpContext.RequestServices.GetRequiredService<MusicStoreEntities>();
            var cart = new ShoppingCart(dbContext);
            cart.ShoppingCartId = cart.GetCartId(controller.HttpContext);
            return cart;
        }

        public static ShoppingCart GetCart(HttpContext httpContext, MusicStoreEntities dbContext)
        {
            var cart = new ShoppingCart(dbContext);
            cart.ShoppingCartId = cart.GetCartId(httpContext);
            return cart;
        }

        public void AddToCart(Album album)
        {
            var cartItem = _dbContext.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _dbContext.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = _dbContext.Carts.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _dbContext.Carts.Remove(cartItem);
                }

                _dbContext.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _dbContext.Carts.Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _dbContext.Carts.Remove(cartItem);
            }

            _dbContext.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _dbContext.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in _dbContext.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in _dbContext.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Album!.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album!.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Album.Price);
                _dbContext.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;
            _dbContext.SaveChanges();
            EmptyCart();

            return order.OrderId;
        }

        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity?.Name))
                {
                    context.Session.SetString(CartSessionKey, context.User.Identity.Name);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }

            return context.Session.GetString(CartSessionKey)!;
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = _dbContext.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _dbContext.SaveChanges();
        }
    }
}
