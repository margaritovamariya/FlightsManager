using FlightManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// UserIndexModel
    /// </summary>
    public class UserIndexViewModel
    {
        /// <summary>
        /// gets,sets pagerModel pager
        /// </summary>
        public PagerViewModel Pager { get; set; }
        /// <summary>
        /// gets,sets Icollection of Users
        /// </summary>
        public ICollection<UserViewModel> Items { get; set; }
    }
}
