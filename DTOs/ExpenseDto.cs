using Project_Backend_2024.DTO.TechStack;
namespace SmartPersonalFinancer.DTOs;
public record ExpenseDto(string Category, decimal Amount, DateTime Date);