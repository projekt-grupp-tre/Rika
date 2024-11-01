﻿namespace RikaWebApp.ViewModels;

public class ReviewViewModel
{
    public string ClientName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}