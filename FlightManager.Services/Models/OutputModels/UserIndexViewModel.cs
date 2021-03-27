using FlightManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    public class UserIndexViewModel
    {
        public PagerViewModel Pager { get; set; }
        public ICollection<UserViewModel> Items { get; set; }
    }
}
