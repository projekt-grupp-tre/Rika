﻿namespace Business.Dto.Product;

public class ReviewDTO
{
    public int ReviewId { get; set; }
    public string ClientName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}