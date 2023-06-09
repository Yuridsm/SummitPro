﻿namespace SummitPro.Application.Feature.UpdateBarbecue
{
    public class UpdateBarbecueCommandModel
    {
        public Guid BarbecueIdentifier { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; } = string.Empty;
        public List<string>? AdditionalMarks { get; set; } = new List<string>();
        public List<Guid>? Participants { get; set; } = new List<Guid>();
    }
}
