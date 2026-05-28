using Microsoft.EntityFrameworkCore;
using Sklad1.Data;
using Sklad1.Models;
using Sklad1.Properties;
using System.Collections.Generic;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Сервис для работы со сроками годности
    /// </summary>
    public static class ExpiryService
    {
        public static ExpiryStatus GetExpiryStatus(DateTime expiryDate, int warningDays = 7, int dangerDays = 3)
        {
            var daysLeft = (expiryDate - DateTime.Today).Days;

            if (daysLeft < 0)
                return ExpiryStatus.Expired;
            if (daysLeft <= dangerDays)
                return ExpiryStatus.Danger;
            if (daysLeft <= warningDays)
                return ExpiryStatus.Warning;

            return ExpiryStatus.Normal;
        }

        public static Color GetStatusColor(ExpiryStatus status)
        {
            return status switch
            {
                ExpiryStatus.Expired => Color.LightGray,
                ExpiryStatus.Danger => Color.LightCoral,
                ExpiryStatus.Warning => Color.LightYellow,
                _ => Color.White
            };
        }

        public static int GetDaysLeft(DateTime expiryDate)
        {
            return (expiryDate - DateTime.Today).Days;
        }

        public static async Task<int> WriteOffExpiredBatches()
        {
            var writtenOffCount = 0;

            try
            {
                using (var bd = new Context())
                {
                    var todayUtc = DateTime.UtcNow.Date;

                    var expiredBatches = bd.ProductBatches
                        .Include(b => b.Product)
                        .Where(b => b.Status == "active" && b.ExpiryDate <= todayUtc && b.Quantity > 0)
                        .ToList();

                    foreach (var batch in expiredBatches)
                    {
                        if (batch.Product == null || batch.Product.Quantity <= 0)
                            continue;

                        int writeOffQuantity = batch.Quantity;
                        if (batch.Product.Quantity < batch.Quantity)
                        {
                            writeOffQuantity = batch.Product.Quantity;
                            batch.Quantity = writeOffQuantity;
                        }

                        if (writeOffQuantity <= 0) continue;

                        using (var transaction = await bd.Database.BeginTransactionAsync())
                        {
                            try
                            {
                                var loss = new Loss
                                {
                                    Id = Guid.NewGuid(),
                                    ProductId = batch.ProductId,
                                    Quantity = writeOffQuantity,
                                    PurchasePrice = batch.PurchasePrice,
                                    Type = "expired",
                                    Date = DateTime.UtcNow
                                };
                                bd.Losses.Add(loss);

                                batch.Status = "expired";
                                batch.Quantity = 0;
                                batch.Product.Quantity -= writeOffQuantity;

                                await bd.SaveChangesAsync();
                                await transaction.CommitAsync();

                                writtenOffCount += writeOffQuantity;
                            }
                            catch (Exception ex)
                            {
                                await transaction.RollbackAsync();
                            }
                        }
                    }

                    var productsToDelete = bd.Products.Where(p => p.Quantity <= 0).ToList();
                    if (productsToDelete.Any())
                    {
                        bd.Products.RemoveRange(productsToDelete);
                        await bd.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error(ex, Resources.ErrorWriteOff);
            }

            return writtenOffCount;
        }

        public static bool CanShip(DateTime expiryDate)
        {
            return GetDaysLeft(expiryDate) >= 0;
        }

        public static IOrderedQueryable<ProductBatch> GetFifoBatches(Context bd, Guid productId)
        {
            return bd.ProductBatches
                .Where(b => b.ProductId == productId && b.Status == "active" && b.Quantity > 0)
                .OrderBy(b => b.ExpiryDate);  
        }

        public static decimal GetDiscount(int daysLeft)
        {
            if (daysLeft < 0) return 0.50m;   
            if (daysLeft <= 1) return 0.30m;  
            if (daysLeft <= 3) return 0.20m;  
            if (daysLeft <= 7) return 0.10m;    
            return 0;
        }

        public static decimal GetDiscountedPrice(decimal originalPrice, int daysLeft)
        {
            decimal discount = GetDiscount(daysLeft);
            return Math.Round(originalPrice * (1 - discount), 2);
        }

        public static string GetDiscountText(int daysLeft)
        {
            decimal discount = GetDiscount(daysLeft);
            if (discount == 0) return "-";
            return $"-{discount * 100}%";
        }
    }

    /// <summary>
    /// Статус срока годности
    /// </summary>
    public enum ExpiryStatus
    {
        Normal,
        Warning,
        Danger,
        Expired
    }
}
