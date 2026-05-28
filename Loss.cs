using Sklad1.Models;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Общие правила расчета цвета и рамки тепловой карты склада.
    /// </summary>
    public static class WarehouseMapRules
    {
        public const int LowQuantityThreshold = 10;
        public static readonly string[] Rows = { "A", "B", "C", "D", "E", "F", "G", "H" };
        public static readonly int[] Columns = { 1, 2, 3, 4, 5, 6 };

        public static CellVisualState CalculateCellState(IEnumerable<ProductBatch> batches, DateTime today)
        {
            var active = batches.Where(b => b.Quantity > 0 && b.Status == "active").ToList();
            if (active.Count == 0)
                return CellVisualState.Empty();

            var states = active.Select(b => CalculateBatchState(b.Quantity, (b.ExpiryDate.Date - today.Date).Days)).ToList();
            var main = states.OrderBy(s => s.Priority).First();

            var hasHighPriority = states.Any(s => s.FillKind == CellFillKind.Red || s.FillKind == CellFillKind.Orange);
            var needBlueBorder = !hasHighPriority && states.Any(s => s.HasBlueBorder);

            main.HasBlueBorder = needBlueBorder;
            return main;
        }

        public static CellVisualState CalculateBatchState(int quantity, int daysLeft)
        {
            if (daysLeft < 15 && quantity < LowQuantityThreshold)
            {
                return new CellVisualState
                {
                    FillKind = CellFillKind.Red,
                    BackColor = Color.IndianRed,
                    HasBlueBorder = false,
                    Priority = 1,
                    Description = "Критично: срок < 15 дней и количество < 10"
                };
            }

            if (daysLeft < 15)
            {
                return new CellVisualState
                {
                    FillKind = CellFillKind.Orange,
                    BackColor = Color.Orange,
                    HasBlueBorder = false,
                    Priority = 2,
                    Description = "Срок меньше 15 дней"
                };
            }

            if (daysLeft <= 30)
            {
                return new CellVisualState
                {
                    FillKind = CellFillKind.Yellow,
                    BackColor = Color.Khaki,
                    HasBlueBorder = quantity < LowQuantityThreshold,
                    Priority = 3,
                    Description = "Срок от 15 до 30 дней"
                };
            }

            return new CellVisualState
            {
                FillKind = CellFillKind.Green,
                BackColor = Color.LightGreen,
                HasBlueBorder = quantity < LowQuantityThreshold,
                Priority = 4,
                Description = "Срок больше 30 дней"
            };
        }

        public static string NormalizeCellCode(string? cellCode, Guid productId)
        {
            if (!string.IsNullOrWhiteSpace(cellCode))
            {
                var normalized = cellCode.Trim().ToUpperInvariant();
                if (Rows.Any(r => normalized.StartsWith(r)) && int.TryParse(normalized.Substring(1), out var col) && Columns.Contains(col))
                    return normalized;
            }

            return GetDefaultCellCode(productId);
        }

        public static string GetDefaultCellCode(Guid productId)
        {
            var bytes = productId.ToByteArray();
            var hash = Math.Abs(BitConverter.ToInt32(bytes, 0));
            var row = Rows[hash % Rows.Length];
            var col = Columns[(hash / Rows.Length) % Columns.Length];
            return $"{row}{col}";
        }
    }

    public sealed class CellVisualState
    {
        public CellFillKind FillKind { get; set; }
        public Color BackColor { get; set; }
        public bool HasBlueBorder { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; } = string.Empty;

        public static CellVisualState Empty()
        {
            return new CellVisualState
            {
                FillKind = CellFillKind.Empty,
                BackColor = Color.White,
                HasBlueBorder = false,
                Priority = 99,
                Description = "Пустая ячейка"
            };
        }
    }

    public enum CellFillKind
    {
        Empty,
        Red,
        Orange,
        Yellow,
        Green
    }
}
