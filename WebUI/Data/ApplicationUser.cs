using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using WebUI.Data.Enums;
using WebUI.Data.Interfaces;
using WebUI.Data.Models;

namespace WebUI.Data
{
    public class ApplicationUser : IdentityUser, ISelectOption<string>
    {
        // additional, DB mapped fields for users should be defined here
        // create a migration after making changes and then update the database
        //[PersonalData]
        public string ProfilePhoto { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string School { get; set; }

        public ICollection<UserGrade> Grades { get; set; } 
        public ICollection<UserSubject> Subjects { get; set; }
        //public ICollection<ContentAuthor> Contents { get; set; }

        [PersonalData]
        public string Status { get; set; }

        [PersonalData]
        public DateTime CreatedDateTime { get; set; }
        [PersonalData]
        public string CreatedById { get; set; }
        [PersonalData]
        public ApplicationUser CreatedBy { get; set; }

        [PersonalData]
        public DateTime? ChangedDateTime { get; set; }
        [PersonalData]
        public string ChangedById { get; set; }
        [PersonalData]
        public ApplicationUser ChangedBy { get; set; }

        [PersonalData]
        public bool Deleted { get; set; }
        [PersonalData]
        public string DeletedById { get; set; }
        [PersonalData]
        public ApplicationUser DeletedBy { get; set; }
        [PersonalData]
        public DateTime? DeletedDateTime { get; set; }

        // not mapped, virtual collections of entities that have many to one relationships with users
        // if you want to use these navigation properties anywhere they need to be defined
        
        public virtual List<ContentDetails> ContentCreatedBy { get; set; }
        public virtual List<ContentDetails> ContentChangedBy { get; set; }
        public virtual List<Grade> GradeCreatedBy { get; set; }
        public virtual List<Grade> GradeChangedBy { get; set; }
        public virtual List<Subject> SubjectCreatedBy { get; set; }
        public virtual List<Subject> SubjectChangedBy { get; set; }
        public virtual List<Topic> TopicCreatedBy { get; set; }
        public virtual List<Topic> TopicChangedBy { get; set; }
        public virtual List<CPLConnection> CPLConnectionCreatedBy { get; set; }
        public virtual List<CPLConnection> CPLConnectionChangedBy { get; set; }
        public virtual List<Standard> StandardCreatedBy { get; set; }
        public virtual List<Standard> StandardChangedBy { get; set; }
        public virtual List<ApplicationUser> UserCreatedBy { get; set; }
        public virtual List<ApplicationUser> UserChangedBy { get; set; }
        public virtual ICollection<ContentBookmark> ContentBookmarks { get; set; }
        //public virtual List<ContentAuthor> CoAuthors { get; set; }

        /* ISelectOption<string> Implementation */
        public string OptionId => this.Id;
        public string DisplayName => $"{this.UserName}";
        public string FullName => this.FirstName + " " + this.LastName;
    }
}
