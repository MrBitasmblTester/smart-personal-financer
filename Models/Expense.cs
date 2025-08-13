using Project_Backend_2024.DTO.TechStack;
namespace SmartPersonalFinancer.Models;
public class Expense { public Guid Id {get;set;}=Guid.NewGuid(); public string Category {get;set;}=""; public decimal Amount {get;set;}; public DateTime Date {get;set;}=DateTime.UtcNow; }