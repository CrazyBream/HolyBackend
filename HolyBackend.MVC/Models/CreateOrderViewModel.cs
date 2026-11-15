using System;
using System.ComponentModel.DataAnnotations;
using HolyBackend.Core.Models;

namespace HolyBackend.MVC.Models
{
    public class CreateOrderViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Назва заявки є обов'язковою.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Назва має містити від 5 до 100 символів.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Опис проблеми є обов'язковим.")]
        [MinLength(20, ErrorMessage = "Опис має містити хоча б 20 символів для точного розуміння гріха.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public OrderType Type { get; set; } = OrderType.Standard;
    }
}