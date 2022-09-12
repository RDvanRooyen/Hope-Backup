using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Data;
using WebUI.Data.Models;

namespace WebUI.Services
{
    /// <summary>
    /// Service for KAMS report data
    /// </summary>
    public class ReportService
    {
        protected readonly ApplicationDbContext<ApplicationUser> context;

        public ReportService(ApplicationDbContext<ApplicationUser> context)
        {
            this.context = context;
        }

        

        /// <summary>
        /// Get all data for the content by author report using the given report options
        /// </summary>
        /// <returns>A list of contents by author</returns>
        //public async Task<List<AuthorContentRow>> GetAuthorContentDataAsync(AuthorContentReportOptions opts)
        //{
        //    var rows = await context.AuthorContentRows
        //        .FromSqlInterpolated($@"spAuthorContent @authorIdParam={opts.AuthorId}, @includeAllClientsParam={opts.AllAuthors}")
        //        .ToListAsync();

        //    return rows;
        //}

        /// <summary>
        /// Get all data for the content by author report using the given report options
        /// </summary>
        /// <returns>A list of contents by author</returns>
        //public async Task<List<AuthorContentRow>> GetContentAuthorDataAsync(ContentAuthorReportOptions opts)
        //{
        //    var rows = await context.AuthorContentRows
        //        .FromSqlInterpolated($@"spContentAuthor @contentIdParam={opts.ContentId}, @includeAllClientsParam={opts.AllContents}")
        //        .ToListAsync();

        //    return rows;
        //}
    }
}
