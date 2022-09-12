﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebUI.Data.Interfaces;

namespace WebUI.Data.Models
{
    // extension for class Topic - use to add non-mapped properties, methods, etc.
    // validation annotations (e.g. [Required]) should NOT be used here as they will not be respected by Blazor
    // when using a code-first approach this extension isn't really necessary, but it may be helpful
    // when using a database-first approach, the main class definition under Data/Models will be overwritten by EF
    public partial class Topic : IAuditable, ISoftDeletable, ISelectOption<int>
    {
        // ISelectOption Implementation - not mapped to database fields
        [NotMapped]
        public int OptionId => Id;

        [NotMapped]
        public string DisplayName => Title;
    }
}
