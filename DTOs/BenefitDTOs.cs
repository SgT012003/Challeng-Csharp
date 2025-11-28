using System;

namespace CarePlusApi.DTOs
{
    // DTO de Resposta para Benefícios (Rewards)
    public class BenefitResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CostPoints { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsClaimed { get; set; }
        public DateTime? ClaimedAt { get; set; }
    }
}
