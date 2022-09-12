using Blazored.TextEditor;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data;
using WebUI.Data.Models;
using WebUI.Data.ViewModels;
using WebUI.Helpers;
using WebUI.Services;

namespace WebUI.Pages.Contents
{
    public partial class ContentEntry
    {
       


        public string[] IslandList = new string[] {
            "Ka Pae ʻAina ʻo Hawaiʻi (The Hawaiian Archipelago)",
        "Papahānaumokuākea (Northwestern Hawaiian Islands)",
        "Niʻihau Island",
        "Kauaʻi Island",
        "Oʻahu Island",
        "Molokaʻi Island",
        "Lānaʻi Island",
        "Kahoʻolawe Island",
        "Maui Island",
        "Hawaiʻi Island",
        };

        [Inject]
        public IConfiguration Configuration { get; set; }
        [Parameter]
        public int? ContentDetailId { get; set; }

        private string _selectedPrimaryPhoto;

        string _loadingInfo = string.Empty;
        bool _isLoading = false;

        List<ContentDerivationOption> _derivations = new List<ContentDerivationOption>();

        List<string> _derivationOptions = new List<string> {
            ContentDetails.DerivationOptionHopeContentID,
            ContentDetails.DerivationOptionExternalLink
        };
        string _openEducationLicensingAddressed = "Yes";

        List<string> _openEducationLicensingAddressedOptions = new List<string> { "Yes", "No" };

        public int Step { get; set; } = 1;

        ServiceBase Service { get; set; }

        private ApplicationUser _currentUser;
        List<Data.Models.Grade> _grades;
        List<Data.Models.Subject> _subjects;
        private List<Standard> _standards;
        private List<CoAuthorItem> _contentAuthorUserList;
        private List<Data.Models.Topic> _topics;
        ContentDetails _contentDetails = null;
        BlazoredTextEditor summaryRTF, essentialRTF, purposeRTF,
            materialRTF, resourcesRTF, communityRTF,
            formativeRTF, summativeRTF, finalRTF;

        public Dictionary<string, TempFile> PhotoDictionary = new Dictionary<string, TempFile>();
        public Dictionary<string, TempFile> MaterialDictionary = new Dictionary<string, TempFile>();
        public Dictionary<string, TempFile> ResourceDictionary = new Dictionary<string, TempFile>();
        public Dictionary<string, TempFile> CommunityDictionary = new Dictionary<string, TempFile>();
        public Dictionary<string, TempFile> FormativeDictionary = new Dictionary<string, TempFile>();
        public Dictionary<string, TempFile> SummativeDictionary = new Dictionary<string, TempFile>();
        public Dictionary<string, TempFile> PacingDictionary = new Dictionary<string, TempFile>();
        public Dictionary<string, TempFile> StudentWorkDictionary = new Dictionary<string, TempFile>();
        //private IList<ApplicationUser> SelectedCoAuthors;
        private List<CoAuthorItem> _coAuthorEmails = new List<CoAuthorItem>();

        protected override async Task OnInitializedAsync()
        {
            Service = await _factory.CreateService<ServiceBase>(_authenticationStateProvider);
            this._currentUser = Service.GetCurrentUser() ;
            _grades = Service.GetContext().Grades.OrderBy(x => x.Id).ToList();
            _subjects = Service.GetContext().Subjects.OrderBy(x => x.Id).ToList();
            _standards = Service.GetContext().Standards.OrderBy(x => x.Id).ToList();
            _contentAuthorUserList = Service.GetContext().Users.Select(x => new CoAuthorItem { UserId = x.Id, EmailAddress = x.Email, IsValid = true, Author = x }).ToList();
            _topics = Service.GetContext().Topics.ToList();

            if ((ContentDetailId ?? 0) == 0)
            {
                _contentDetails = new ContentDetails();
                _contentDetails.CoAuthors = new List<ContentAuthor>(); 
                _contentDetails.CreatedBy = Service.GetCurrentUser();
                _contentDetails.CreatedById = Service.GetCurrentUser().Id;
                _contentDetails.CreatedDateTime = DateTime.Now;

                var initialDerivation = -1;
                var success = NavManager.TryGetQueryString<int>("d", out initialDerivation);
                if (!success)
                {
                    initialDerivation = -1;
                }

                if (initialDerivation >= 0)
                {
                    var dcontent = Service.GetContext().ContentDetails.Where(x => x.Id == initialDerivation).FirstOrDefault();
                    var cd = new ContentDerivationOption
                    {
                        Category = ContentDetails.DerivationOptionHopeContentID,
                        ContentId = initialDerivation,
                        ContentDetails = dcontent,
                    };

                    _derivations.Add(cd);
                }
            }
            else
            {
                _contentDetails = Service.GetContext().ContentDetails
                    .Include(x => x.CreatedBy)
                    .Include(x => x.ContentExternalLinks)
                    .Include(x => x.Derivations)
                    .Include(x => x.ContentFiles)
                    .Include(x => x.ContentGrades)
                    .Include(x => x.ContentStandards)
                    .Include(x => x.ContentSubjects)
                    .Include(x => x.ContentTopics)
                    .ThenInclude(x => x.Topic)
                    .Include(x => x.CoAuthors)
                    .ThenInclude(x => x.Author)
                    .Where(x => x.Id == ContentDetailId).FirstOrDefault();

                PhotoDictionary = BuildDictionary(ContentFiles.PhotoContentFileCategory);
                MaterialDictionary = BuildDictionary(ContentFiles.MaterialContentFileCategory);
                ResourceDictionary = BuildDictionary(ContentFiles.ResourceContentFileCategory);
                CommunityDictionary = BuildDictionary(ContentFiles.CommunityContentFileCategory, true);
                FormativeDictionary = BuildDictionary(ContentFiles.FormativeContentFileCategory, true);
                SummativeDictionary = BuildDictionary(ContentFiles.SummativeContentFileCategory, true);
                PacingDictionary = BuildDictionary(ContentFiles.PacingContentFileCategory);
                StudentWorkDictionary = BuildDictionary(ContentFiles.StudentWorkContentFileCategory, true);

                _derivations = new List<ContentDerivationOption>();
                _coAuthorEmails = new List<CoAuthorItem>();
                SetCoAuthors();
                SetContentDerivations();
                SetContentExternalLinks();
                SetSelectedTopics();
                SetSelectedGrades();
                SetSelectedSubjects();
                SetSelectedStandards();
                _openEducationLicensingAddressed = _contentDetails.OpenEduLicAdd ? "Yes" : "No";
                _selectedPrimaryPhoto = _contentDetails.ContentFiles.Where(x => x.Category == ContentFiles.PhotoContentFileCategory && x.IsPrimaryPhoto).FirstOrDefault()?.Filename;
                 


            }
        }

        private Dictionary<string, TempFile> BuildDictionary(string v, bool hasDescription = false)
        {
            Dictionary<string, TempFile> dictionary = new Dictionary<string, TempFile>();
            var files = _contentDetails.ContentFiles.Where(x => x.Category == v);
            foreach (var f in files)
            {
                //1\Photos\cat.jpg
                string filename = f.RelativePath.Split(@"\").LastOrDefault();
                string path = Path.Combine(Configuration["AppSettings:BaseDirectory"], f.RelativePath).Replace(@"\", @"/").ToString();
                var filecontent = ImageTool.GetByteFromPath(path);
                var tf = new TempFile
                {
                    Content = filecontent,
                    ContentFileId = f.Id,
                    Description = f.Description,
                    Filename = filename,
                    HasDescription = hasDescription,
                    ContentType = f.ContentType,
                };
                if (f.ContentType.StartsWith("image"))
                {
                    tf.Thumbnail = ImageTool.CreateThumbnail(filecontent);
                }
                dictionary.Add(filename, tf);
            }
            return dictionary;
        }

        private void SetCoAuthors()
        {
            var list = _contentDetails.CoAuthors.ToList();
            _coAuthorEmails = list.Select(x => new CoAuthorItem
            {
                UserId = x.AuthorId,
                EmailAddress = x.Author.Email,
                Author = x.Author,
                IsValid = true,
            }).ToList();
        }
        private void SetContentDerivations()
        {
            var l = _contentDetails.Derivations.Select(x => new ContentDerivationOption
            {
                Category = ContentDetails.DerivationOptionHopeContentID,
                ContentId = x.ContentDetailsId,
            }).ToList();

            _derivations.AddRange(l);
        }
        private void SetContentExternalLinks()
        {
            var l = _contentDetails.ContentExternalLinks.Select(x => new ContentDerivationOption
            {
                Category = ContentDetails.DerivationOptionExternalLink,
                ExternalLink = x.ExternalLink,
            }).ToList();

            _derivations.AddRange(l);
        }

        private void SetSelectedTopics()
        {
            SelectedTopics = _contentDetails.ContentTopics.Select(x => x.Topic).ToList();
        }
        private void SetSelectedGrades()
        {
            var s = _contentDetails.ContentGrades.Select(x => x.GradeId).ToArray();
            foreach (var i in _grades)
            {
                if (s.Contains(i.Id))
                {
                    i.Checked = true;
                }
            }
        }
        private void SetSelectedSubjects()
        {
            var s = _contentDetails.ContentSubjects.Select(x => x.Subject).ToList();
            SelectedSubjects = s;
        }
        private void SetSelectedStandards()
        {
            var s = _contentDetails.ContentStandards.Select(x => x.Standard).ToList();
            SelectedStandards = s;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await SetAllRTF();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task SetAllRTF()
        {
            switch (Step)
            {
                case 1:
                    await SetSummaryRTF();

                    break;
                case 2:
                    await SetEssentialRTF();
                    await SetPurposeRTF();
                    break;
                case 3:
                    await SetMaterialRTF();
                    await SetResourcesRTF();
                    await SetCommunityRTF();
                    break;
                case 4:
                    await SetFormativeRTF();
                    await SetSummativeRTF();
                    await SetFinalRTF();
                    break;
                case 5:
                    break;
                default:
                    break;
            }
            StateHasChanged();
        }

        private async Task GetAllRTF(int previousStep)
        {
            switch (previousStep)
            {
                case 1:
                    await GetSummaryRTF();
                    break;
                case 2:
                    await GetEssentialRTF();
                    await GetPurposeRTF();
                    break;
                case 3:
                    await GetMaterialRTF();
                    await GetResourcesRTF();
                    await GetCommunityRTF();
                    break;
                case 4:
                    await GetFormativeRTF();
                    await GetSummativeRTF();
                    await GetFinalRTF();
                    break;
                case 5:
                    break;
                default:
                    break;
            }
            //StateHasChanged();
        }

        private void BuildObject()
        {
            _contentDetails.CoAuthors = GetCoAuthors();
            _contentDetails.Derivations = GetContentDerivations();
            _contentDetails.ContentExternalLinks = GetContentExternalLinks();
            _contentDetails.OpenEduLicAdd = _openEducationLicensingAddressed == "Yes";
            _contentDetails.ContentGrades = GetSelectedGrades();
            _contentDetails.ContentStandards = GetSelectedStandards();
            _contentDetails.ContentSubjects = GetSelectedSubjects();
            _contentDetails.ContentTopics = GetSelectedTopics();

            _contentDetails.AuthorText = _currentUser.FullName;
            _contentDetails.CoAuthorsText = string.Join(", ", _contentDetails.CoAuthors.Select(x => x.Author.FullName).ToArray());
            _contentDetails.GradesText = string.Join(", ", _contentDetails.ContentGrades.Select(x => x.Grade.Title).ToArray());
            _contentDetails.SubjectsText = string.Join(", ", _contentDetails.ContentSubjects.Select(x => x.Subject.Title).ToArray());
            _contentDetails.TopicsText = string.Join(", ", _contentDetails.ContentTopics.Select(x => x.Topic.Title).ToArray());

            _contentDetails.GradesIdText = string.Join(", ", _contentDetails.ContentGrades.Select(x => x.Grade.Id).ToArray());
            _contentDetails.SubjectsIdText = string.Join(", ", _contentDetails.ContentSubjects.Select(x => x.Subject.Id).ToArray());
            _contentDetails.TopicsIdText = string.Join(", ", _contentDetails.ContentTopics.Select(x => x.Topic.Id).ToArray());

            _contentDetails.SubmittedDateTime = _contentDetails.SubmittedDateTime ?? DateTime.Now;
            _contentDetails.CreatedById = _contentDetails.CreatedById ??_currentUser.Id;

            var primaryPhoto = _contentDetails.ContentFiles.Where(x => x.IsPrimaryPhoto).FirstOrDefault();
            if (primaryPhoto == null)
            {
                primaryPhoto = _contentDetails.ContentFiles.OrderByDescending(x => x.Id).FirstOrDefault();
            }
            _contentDetails.PrimaryPhotoPath = primaryPhoto?.RelativePath;

        }

        private List<ContentTopic> GetSelectedTopics()
        {
            var selected = SelectedTopics.Where(x => x.Id > 0)
                .Select(x => new ContentTopic { ContentDetailsId = _contentDetails.Id, TopicId = x.Id, Topic = x }).ToList();
            var newTopics = SelectedTopics.Where(x => x.Id == 0);
            foreach (var nt in newTopics)
            {
                selected.Add(new ContentTopic
                {
                    Topic = nt,
                });
            }
            return selected;
            
        }

        private List<ContentSubject> GetSelectedSubjects()
        {
            var selected = SelectedSubjects.Where(x => x.Id > 0).Select(x => new ContentSubject { ContentDetailsId = _contentDetails.Id, SubjectId = x.Id, Subject= x }).ToList();
            return selected;
        }

        private List<ContentStandard> GetSelectedStandards()
        {
            var selected = SelectedStandards.Where(x => x.Id > 0).Select(x => new ContentStandard { ContentDetailsId = _contentDetails.Id, StandardId = x.Id, Standard = x }).ToList();
            return selected;

            //var selected = _standards.Where(x => x.Checked ?? false).ToList();
            //return selected.Select(x => new ContentStandard
            //{
            //    ContentDetailsId = _contentDetails.Id,
            //    Standard = x,
            //    StandardId = x.Id
            //}).ToList();
        }
         

        private List<ContentGrade> GetSelectedGrades()
        {
            var selected = _grades.Where(x => x.Checked ?? false).ToList();
            return selected.Select(x => new ContentGrade
            {
                ContentDetailsId = _contentDetails.Id,
                Grade = x,
                GradeId = x.Id
            }).ToList();
        }

        private List<ContentDerivation> GetContentDerivations()
        {
            return _derivations.Where(x => x.Category == ContentDetails.DerivationOptionHopeContentID && x.ContentId != null).Select(x => new ContentDerivation
            {
                ContentDetailsId = _contentDetails.Id,
                DerivationId = x.ContentId,
            }).ToList();
        }
        private List<ContentExternalLink> GetContentExternalLinks()
        {
            return _derivations.Where(x => x.Category == ContentDetails.DerivationOptionExternalLink).Select(x => new ContentExternalLink
            {
                ContentDetailsId = _contentDetails.Id,
                ExternalLink = x.ExternalLink,
            }).ToList();
        }

        private List<ContentAuthor> GetCoAuthors()
        {
            if (_coAuthorEmails == null) return null;
            return _coAuthorEmails.Where(x => x.IsValid).Select(x => new ContentAuthor
            {
                AuthorId = x.UserId,
                Author = x.Author,
                ContentId = _contentDetails.Id
            }).ToList();
        }

        private async Task<IEnumerable<ApplicationUser>> SearchAuthor(string searchText)
        {
            searchText = searchText?.ToLower();
            var search = await Service.GetContext()
                .Users.Where(x => x.FirstName != null && x.LastName != null && x.FirstName.Contains(searchText)
            || x.LastName.Contains(searchText)).ToListAsync();
            return search;
        }

        private async Task<IEnumerable<ContentDetails>> SearchContent(string searchText)
        {
            searchText = searchText?.ToLower();
            var search = await Service.GetContext()
                .ContentDetails.Where(x => x.Title != null && x.Title.Contains(searchText)).ToListAsync();
            return search;

        }

        private IList<WebUI.Data.Models.Topic> SelectedTopics = new List<WebUI.Data.Models.Topic>();
        private IList<WebUI.Data.Models.Subject> SelectedSubjects = new List<WebUI.Data.Models.Subject>();
        private IList<WebUI.Data.Models.Standard> SelectedStandards = new List<WebUI.Data.Models.Standard>();
        private bool _isSaving;
        private bool _showHelp;

        private async Task<IEnumerable<WebUI.Data.Models.Topic>> SearchTopic(string searchText)
        {
            searchText = searchText?.ToLower();
            var search = _topics.Where(x => x.Title != null && x.Title.ToLower().Contains(searchText)).ToList();
            return search;
        }
        private async Task<IEnumerable<WebUI.Data.Models.Subject>> SearchSubject(string searchText)
        {
            searchText = searchText?.ToLower();
            var search = _subjects.Where(x => x.Title != null && x.Title.ToLower().Contains(searchText)).ToList();
            return search;
        }
        private async Task<IEnumerable<WebUI.Data.Models.Standard>> SearchStandard(string searchText)
        {
            searchText = searchText?.ToLower();
            var search = _standards.Where(x => x.Title != null && x.Title.ToLower().Contains(searchText)).ToList();
            return search;
        }
        private Task<WebUI.Data.Models.Topic> ItemAddedMethod(string searchText)
        {
            var t = new WebUI.Data.Models.Topic
            {
                Title = searchText
            };
            return Task.FromResult(t);
        }

        private async Task PerformStepAsync(int step)
        {
            await GetAllRTF(Step);
            Step = step;
            StateHasChanged();
            await SetAllRTF();

        }
        private async Task PerformStepPreviousAsync()
        {
            if (Step > 1)
            {
                await GetAllRTF(Step);
                Step--;
                await SetAllRTF();
            }
        }
        private async Task PerformStepNextAsync()
        {
            if (Step < 5)
            {
                await GetAllRTF(Step);
                Step++;
                await SetAllRTF();
            }
        }
        private void AddDerivation()
        {
            _derivations.Add(new ContentDerivationOption
            {
                Category = ContentDetails.DerivationOptionHopeContentID
            });
        }
        private void RemoveDerivation(int index)
        {
            _derivations.RemoveAt(index);
        }
        private void AddCoAuthor()
        {
            _coAuthorEmails.Add(new CoAuthorItem
            {
                EmailAddress = string.Empty
            });
        }
        private void RemoveCoAuthor(int index)
        {
            _coAuthorEmails.RemoveAt(index);
        }

        private void UpdateCoAuthor(ChangeEventArgs args, int index)
        {
            var value = (string)args.Value;
            _coAuthorEmails[index].EmailAddress = value;
            _coAuthorEmails[index].IsValid = false;
            _coAuthorEmails[index].InvalidMessage = "Invalid user. Email address not found.";
            var exist = _contentAuthorUserList.Where(x => x.EmailAddress == value).FirstOrDefault();
            if (exist != null)
            {
                var duplicated = _coAuthorEmails.Where(x => x.EmailAddress == value).Count();
                if (duplicated > 1)
                {
                    _coAuthorEmails[index].UserId = null;
                    _coAuthorEmails[index].IsValid = false;
                    _coAuthorEmails[index].Author = null;
                    _coAuthorEmails[index].InvalidMessage = "Duplicate email address.";
                }
                else
                {
                    _coAuthorEmails[index].UserId = exist.UserId;
                    _coAuthorEmails[index].Author = exist.Author;
                    _coAuthorEmails[index].IsValid = true;
                    _coAuthorEmails[index].InvalidMessage = string.Empty;
                }
            }
        }

        private void DerivationChanged(ChangeEventArgs a, int index)
        {
            _derivations[index].Category = a.Value.ToString();
        }

        private async Task LoadImage(InputFileChangeEventArgs e)
        {
            _loadingInfo = "Uploading...";
            await LoadFiles(e);
            _loadingInfo = string.Empty;
        }

        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            _isLoading = true;
            //foreach (var file in e.GetMultipleFiles(3))
            //{
            //    var i = await PrepareFileAsync(file);
            //    DataFile df = i;
            //    df.S3ObjectId = Guid.NewGuid().ToString();

            //    var ms = new MemoryStream();
            //    await file.OpenReadStream(maxFileSize).CopyToAsync(ms);
            //    await _s3Wrapper.UploadFileAsync(df.S3ObjectId, ms);

            //    Service.Save(df, df => df.DataFileId);
            //}
            //var list = Service.GetQueryable<DataFile>(false, false); 
            _isLoading = false;
        }

        private async Task SaveForLaterAsync()
        {
            _isSaving = true;
            await GetAllRTF(Step);
            BuildObject();
            _contentDetails.SubmittedDateTime = null;
            var isNew = _contentDetails.Id == 0;

            Service.Save(_contentDetails, x => x.Id);
            await UploadFilesToServer();
            _toaster.Add("Content saved.", MatToastType.Success);

            _isSaving = false;
            if (isNew)
            {
                NavManager.NavigateTo("/content/my");
            }
        }

        private async Task UploadFilesToServer()
        {
            await UploadToServer(PhotoDictionary, ContentFiles.PhotoContentFileCategory, _selectedPrimaryPhoto);
            await UploadToServer(MaterialDictionary, ContentFiles.MaterialContentFileCategory);
            await UploadToServer(ResourceDictionary, ContentFiles.ResourceContentFileCategory);
            await UploadToServer(CommunityDictionary, ContentFiles.CommunityContentFileCategory);
            await UploadToServer(FormativeDictionary, ContentFiles.FormativeContentFileCategory);
            await UploadToServer(SummativeDictionary, ContentFiles.SummativeContentFileCategory);
            await UploadToServer(PacingDictionary, ContentFiles.PacingContentFileCategory);
            await UploadToServer(StudentWorkDictionary, ContentFiles.StudentWorkContentFileCategory);
        }

        private async Task SubmitForReviewAsync()
        {
            _isSaving = true;
            await GetAllRTF(Step);
            BuildObject();
            if (!ValidationMessages.Where(x=>!x.Valid).Any())
            {
                _contentDetails.Status = "Submitted/Pending";
                _contentDetails.SubmittedDateTime = DateTime.Now;
                Service.Save(_contentDetails, x => x.Id);
                await UploadFilesToServer();
                Step = 6;

            }
            else
            {
                _toaster.Add("Please fill out all required fields before submitting this content", MatToastType.Danger);
            }
            _isSaving = false;
        }

        private void CancelContent()
        {
            NavManager.NavigateTo("/content/my", true);
        }

        private bool IsRTFEmpty(string s)
        {
            return string.IsNullOrEmpty(s) || s == "<p><br></p>";
        }

        private async Task ForceRefreshValidationMessageAsync()
        {
            await GetAllRTF(Step);
            StateHasChanged();
        }

        private List<ValidationMessageAlert> ValidationMessages
        {
            get
            {
                BuildObject();
                var validationMessages = new List<ValidationMessageAlert>();

                validationMessages.Add(new ValidationMessageAlert { Message = " Title is required!", Step = 1, Valid = !string.IsNullOrEmpty(_contentDetails.Title) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Summary is required! " , Step = 1, Valid = !IsRTFEmpty(_contentDetails.Summary) });
                //validationMessages.Add(new ValidationMessageAlert { Message = " CoAuthors is required!", Step = 1, Valid = !(_contentDetails.CoAuthors == null || !_contentDetails.CoAuthors.Any()) });

                validationMessages.Add(new ValidationMessageAlert { Message = " Grade is required!", Step = 2, Valid = !(_contentDetails.ContentGrades == null || !_contentDetails.ContentGrades.Any()) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Subject is required!", Step = 2, Valid = !(_contentDetails.ContentSubjects == null || !_contentDetails.ContentSubjects.Any()) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Topic is required!", Step = 2, Valid = !(_contentDetails.ContentTopics == null || !_contentDetails.ContentTopics.Any()) });
                validationMessages.Add(new ValidationMessageAlert { Message = " EssentialQuestion is required!", Step = 2, Valid = !(IsRTFEmpty(_contentDetails.EssentialQuestion)) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Objective is required!", Step = 2, Valid = !(IsRTFEmpty(_contentDetails.Objective)) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Connection To Hawaii is required!", Step = 2, Valid = !(string.IsNullOrEmpty(_contentDetails.ConnectionToHawaii)) });

                validationMessages.Add(new ValidationMessageAlert { Message = " Materials is required!", Step = 3, Valid = !(IsRTFEmpty(_contentDetails.MaterialsText)) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Resources is required!", Step = 3, Valid = !(IsRTFEmpty(_contentDetails.ResourcesText)) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Connection is required!", Step = 3, Valid = !(IsRTFEmpty(_contentDetails.ConnectionText)) });

                validationMessages.Add(new ValidationMessageAlert { Message = " Standard is required!", Step = 4, Valid = !(_contentDetails.ContentStandards == null || !_contentDetails.ContentStandards.Any()) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Formative Assessments is required!", Step = 4, Valid = !(IsRTFEmpty(_contentDetails.FormativeAssessments)) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Formative Assessments File is required!", Step = 4, Valid = FormativeDictionary.Any() });
                validationMessages.Add(new ValidationMessageAlert { Message = " Summative Assessments is required!", Step = 4, Valid = !(IsRTFEmpty(_contentDetails.SummativeAssessments)) });
                validationMessages.Add(new ValidationMessageAlert { Message = " Summative Assessments File is required!", Step = 4, Valid = SummativeDictionary.Any() });
                validationMessages.Add(new ValidationMessageAlert { Message = " Final Product/Performance is required!", Step = 4, Valid = !(IsRTFEmpty(_contentDetails.FinalProduct)) });

                validationMessages.Add(new ValidationMessageAlert { Message = " Pacing Guide File is required!", Step = 5, Valid = PacingDictionary.Any() });
                validationMessages.Add(new ValidationMessageAlert { Message = " Artifacts File is required!", Step = 5, Valid = StudentWorkDictionary.Any() });


                return validationMessages;
            }

        }

        #region .RTF GETSET.
        public async Task SetSummaryRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await summaryRTF.LoadHTMLContent(_contentDetails.Summary);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetSummaryRTF()
        {
            try
            {
                _contentDetails.Summary = await summaryRTF.GetHTML();
                _contentDetails.SummaryText = await summaryRTF.GetText();
            }
            catch { }
        }
        public async Task SetEssentialRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await essentialRTF.LoadHTMLContent(_contentDetails.EssentialQuestion);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetEssentialRTF()
        {
            try
            {
                _contentDetails.EssentialQuestion = await essentialRTF.GetHTML();
            }
            catch { }
        }
        public async Task SetPurposeRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await purposeRTF.LoadHTMLContent(_contentDetails.Objective);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetPurposeRTF()
        {
            try
            {
                _contentDetails.Objective = await purposeRTF.GetHTML();
            }
            catch { }
        }

        public async Task SetMaterialRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await materialRTF.LoadHTMLContent(_contentDetails.MaterialsText);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetMaterialRTF()
        {
            try
            {
                _contentDetails.MaterialsText = await materialRTF.GetHTML();
            }
            catch { }
        }
        public async Task SetResourcesRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await resourcesRTF.LoadHTMLContent(_contentDetails.ResourcesText);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetResourcesRTF()
        {
            try
            {
                _contentDetails.ResourcesText = await resourcesRTF.GetHTML();
            }
            catch { }
        }
        public async Task SetCommunityRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await communityRTF.LoadHTMLContent(_contentDetails.ConnectionText);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetCommunityRTF()
        {
            try
            {
                _contentDetails.ConnectionText = await communityRTF.GetHTML();
            }
            catch { }
        }
        public async Task SetFormativeRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await formativeRTF.LoadHTMLContent(_contentDetails.FormativeAssessments);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetFormativeRTF()
        {
            try
            {
                _contentDetails.FormativeAssessments = await formativeRTF.GetHTML();
            }
            catch { }
        }
        public async Task SetSummativeRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await summativeRTF.LoadHTMLContent(_contentDetails.SummativeAssessments);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetSummativeRTF()
        {
            try
            {
                _contentDetails.SummativeAssessments = await summativeRTF.GetHTML();
            }
            catch { }
        }
        public async Task SetFinalRTF()
        {

            int attempt = 0;
            bool failed = true;
            do
            {
                try
                {
                    await finalRTF.LoadHTMLContent(_contentDetails.FinalProduct);
                    failed = false;
                }
                catch { }
                attempt++;


            } while (failed && attempt < 4);
        }

        public async Task GetFinalRTF()
        {
            try
            {
                _contentDetails.FinalProduct = await finalRTF.GetHTML();
            }
            catch { }
        }

        #endregion

        #region . UPLOAD FILES .

        async Task UploadToDictionary(Dictionary<string, TempFile> d, InputFileChangeEventArgs e, bool hasDescription = false)
        {
            foreach (var item in e.GetMultipleFiles(5))   // MAX FILE COUNT ONLY 5
            {

                using MemoryStream fs = new MemoryStream();
                await item.OpenReadStream().CopyToAsync(fs);
                var fileContent = fs.ToArray();
                var fileName = item.Name;
                if (!d.ContainsKey(fileName))
                {
                    var c = new TempFile
                    {
                        Content = fileContent,
                        Filename = fileName,
                        ContentType = item.ContentType,
                        HasDescription = hasDescription
                    };
                    if (item.ContentType.ToLower().StartsWith("image"))
                    {
                        c.Thumbnail = ImageTool.CreateThumbnail(fileContent);
                    }
                    d.Add(fileName, c);
                }
            }
        }

        void RemoveTempFile(Dictionary<string, TempFile> d, string key)
        {
            d.Remove(key);
        }
        async Task UploadToServer(Dictionary<string, TempFile> d, string subDirectory, string? primaryPhoto = null)
        {
            var currentList = _contentDetails.ContentFiles.Where(x => x.Category == subDirectory).ToList();

            foreach (var b in d)
            {
                var fileName = b.Key;
                var fileBytes = b.Value.Content;
                var skip = currentList.Where(x => x.RelativePath.EndsWith(fileName)).FirstOrDefault();
                if (skip != null)
                {
                    skip.Description = b.Value.HasDescription ? b.Value.Description : string.Empty;
                    skip.ContentType = b.Value.ContentType;
                    skip.Category = subDirectory;
                    skip.Filename = fileName;
                    skip.IsPrimaryPhoto = primaryPhoto == fileName;
                }
                else
                {
                    var contentFile = new ContentFiles(Configuration["AppSettings:BaseDirectory"], _contentDetails.Id, subDirectory, fileName, fileBytes, category: subDirectory);
                    contentFile.Description = b.Value.HasDescription ? b.Value.Description : string.Empty;
                    contentFile.ContentType = b.Value.ContentType;
                    contentFile.Category = subDirectory;
                    contentFile.Filename = fileName;
                    contentFile.IsPrimaryPhoto = primaryPhoto == fileName;
                    _contentDetails.ContentFiles.Add(contentFile);

                }
            }

            for (var i = currentList.Count - 1; i >= 0; i--)
            {
                var c = currentList[i];
                var keep = d.Where(x => c.RelativePath.EndsWith(x.Key)).Any();
                if (!keep)
                {
                    var delItem = currentList[i];
                    Service.GetContext().ContentFiles.Remove(delItem);
                    //currentList.RemoveAt(i);
                }

            }
            await Service.SaveChangesAsync();
        }

        //private async Task<bool> IsSummaryEmpty()
        //{
        //    try
        //    {
        //        var content = await summaryRTF.GetText();
        //        return string.IsNullOrEmpty(content);

        //    }catch(Exception e)
        //    {
        //        return true;
        //    }
        //}

        //private async Task<bool> IsRTFEmptyAsync(BlazoredTextEditor b)
        //{
        //    return false;
        //}

        #endregion

        private void ShowHelp(string key)
        {
            _showHelp = true;
        }
    }


}
