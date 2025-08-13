using Project_Backend_2024.DTO.TechStack;
namespace SmartPersonalFinancer.Models;
public class SavingsGoal{ public Guid Id{get;set;}=Guid.NewGuid(); public string Name{get;set;}=""; public decimal Target{get;set;}; public decimal Current{get;set;}; }