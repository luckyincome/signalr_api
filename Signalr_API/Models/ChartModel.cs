﻿using System.Collections.Generic;

namespace Signalr_API.Models
{
    public class ChartModel
    {
        public List<int> Data { get; set; }
        public string? Label { get; set; }
        public string? BackgroundColor { get; set; }

        public ChartModel()
        {
            Data = new List<int>();
        }
    }
}
