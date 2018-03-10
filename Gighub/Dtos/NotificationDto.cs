﻿using System;
using Gighub.Models;

namespace Gighub.Dtos
{
    public class NotificationDto    
    {
        
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public GigDto Gig { get; private set; }

    }
}