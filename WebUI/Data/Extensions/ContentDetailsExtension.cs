using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebUI.Data.Interfaces;

namespace WebUI.Data.Models
{
    // extension for class ContentDetails - use to add non-mapped properties, methods, etc.
    // validation annotations (e.g. [Required]) should NOT be used here as they will not be respected by Blazor
    // when using a code-first approach this extension isn't really necessary, but it may be helpful
    // when using a database-first approach, the main class definition under Data/Models will be overwritten by EF
    public partial class ContentDetails : IAuditable, ISoftDeletable, ISelectOption<int>
    {

        public List<ContentAuthor> CoAuthors { get; set; } = new List<ContentAuthor>();
        public List<ContentDerivation> Derivations { get; set; } = new List<ContentDerivation>();

        public List<ContentDerivation> ContentDerivations { get; set; }
        public List<ContentFiles> ContentFiles { get; set; } = new List<ContentFiles>();
        public List<ContentComment> ContentComments { get; set; } = new List<ContentComment>();
        public List<ContentRating> ContentRatings { get; set; } = new List<ContentRating>();

        public List<ContentExternalLink> ContentExternalLinks { get; set; }
        public List<LinkCheckingReport> LinkCheckingReports { get; set; }

        public List<ContentGrade> ContentGrades { get; set; } = new List<ContentGrade>();

        public List<ContentSubject> ContentSubjects { get; set; } = new List<ContentSubject>();

        public List<ContentTopic> ContentTopics { get; set; } = new List<ContentTopic>();

        public List<ContentStandard> ContentStandards { get; set; } = new List<ContentStandard>();

        public List<ContentBookmark> ContentBookmarks { get; set; }
        public List<ModerationComment> ModerationComments { get; set; }

        // ISelectOption Implementation - not mapped to database fields
        [NotMapped]
        public int OptionId => Id;

        [NotMapped]
        public string DisplayName => Title;

        [NotMapped]
        public bool Checked { get; set; }

        public const string DerivationOptionHopeContentID = "HOPE Content ID";
        public const string DerivationOptionExternalLink = "External Link";

        [NotMapped]
        public string SubmittedDateSortable
        {
            get
            {
                return SubmittedDateTime.ToSortableDate();
            }
        }
        [NotMapped]
        public string SubmittedDateString
        {
            get
            {
                return SubmittedDateTime.ToHumanDateOnly();
            }
        }
        [NotMapped]
        public string ApprovedDateSortable
        {
            get
            {
                return ApprovedDateTime.ToSortableDate();
            }
        }
        [NotMapped]
        public string ApprovedDateString
        {
            get
            {
                return ApprovedDateTime.ToHumanDateOnly();
            }
        }

        public string SubjectsString
        {
            get
            {
                if (ContentSubjects == null) return string.Empty;

                var s = ContentSubjects.Select(x => x.Subject.Title).ToArray();
                return string.Join(", ", s);
            }
        }
        public string GradesString
        {
            get
            {
                if (ContentGrades == null) return string.Empty;

                var s = ContentGrades.Select(x => x.Grade.Title).ToArray();
                return string.Join(", ", s);
            }
        }
        public string TopicsString
        {
            get
            {
                if (ContentTopics == null) return string.Empty;

                var s = ContentTopics.Select(x => x.Topic.Title).ToArray();
                return string.Join(", ", s);
            }
        }
        public string ContributorsString
        {
            get
            {
                List<string> authors = new List<string>();
                authors.Add(this.AuthorText);

                if (!string.IsNullOrEmpty(this.CoAuthorsText))
                    authors.Add(this.CoAuthorsText);

                return string.Join(", ", authors);
            }
        }

        public string SummaryTwoSentences
        {
            get
            {
                return SummaryText.GetFirstSentence(2);
            }
        }

        public string ModeratorCommentString
        {
            get
            {
                if(this.ModerationComments!=null && this.ModerationComments.Any())
                {
                    var last = this.ModerationComments.Where(x => !x.IsInternalComment).OrderByDescending(x => x.CommentDate).FirstOrDefault();
                    return last?.Comment;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }

}
