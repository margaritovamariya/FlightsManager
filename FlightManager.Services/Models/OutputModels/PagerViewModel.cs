using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Models.OutputModels
{
    /// <summary>
    /// PagerModel
    /// </summary>
    public class PagerViewModel
    {
        /// <summary>
        /// Gets,sets currenPage
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// gets,sets pagesCount
        /// </summary>
        public int PagesCount { get; set; }
    }
}
