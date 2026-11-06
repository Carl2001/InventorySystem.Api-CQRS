using System;
namespace InventorySystem.Api.Models
{
    public class InventoryMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
    }
}