using Microsoft.AspNetCore.Components;
using ContosoUniversity.Components.EventModels;
using System;
using System.Threading.Tasks;

namespace ContosoUniversity.Components.Navigation
{
    #region source info
    /*
     * Pagination adapted from and uses highly modified version of code from:
     * https://www.thecodehubs.com/pagination-in-blazor/
     * https://github.com/faisal5170/BlazorDemo-Latest/tree/Pagination
     */
    #endregion source info

    public partial class Pager
    {
        [Parameter]
        public Func<LoadDataPagerEventArgs, Task<LoadDataPageResult>> OnLoadDataFromDb { get; set; }

        [Parameter]
        public int PagerSize { get; set; }

        [Parameter]
        public int PageSize { get; set; }

        [Parameter]
        public int? FirstLoadPage { get; set; }

        #region variables

        int curPage;
        int endPage;
        int startPage;
        int totalPages;

        #endregion variables

        public async Task ResetToFirstPage()
        {
            int firstPage = FirstLoadPage != null ? FirstLoadPage.Value : 1;
            startPage = 0;
            await RefreshRecords(firstPage);
        }


        #region Navigation

        public async Task NavigateToPage(string direction)
        {
            int curPageB4Change = curPage;
            if (direction == "next")
            {
                if (curPage < totalPages)
                {
                    curPage += 1;
                    if (curPage == endPage)
                    {
                        SetPagerSize("back");
                    }
                    else
                    {
                        SetPagerSize("forward");
                    }
                }
            }
            else if (direction == "previous")
            {
                if (curPage > 1)
                {
                    curPage -= 1;
                    if (curPage == 1)
                    {
                        SetPagerSize("forward");
                    }
                    else
                    {
                        SetPagerSize("back");
                    }
                }
            }
            await RefreshRecords(curPage);
        }

        public async Task RefreshRecords(int pageToRefresh)
        {
            LoadDataPagerEventArgs args = new LoadDataPagerEventArgs
            {
                PageToLoad = pageToRefresh,
                PageSize = PageSize
            };
            if (OnLoadDataFromDb != null)
            {
                LoadDataPageResult loadResult = await OnLoadDataFromDb(args);
                if (loadResult != null)
                {
                    totalPages = loadResult.TotalPages;
                    
                    if ((curPage != loadResult.PageNumber) || (args.PageToLoad != loadResult.PageNumber))
                    {
                        curPage = loadResult.PageNumber;
                        if (loadResult.TotalRecords > 0)
                        {
                            if (loadResult.PageNumber < loadResult.TotalPages)
                                SetPagerSize("forward");
                            else
                                SetPagerSize("back");
                        }
                    }
                }
                this.StateHasChanged();
            }
        }

        public void SetPagerSize(string direction)
        {
            if (totalPages <= PagerSize)
            {
                if (startPage == 0)
                {
                    startPage = 1;
                    endPage = totalPages;
                }
            }
            else if (direction == "forward")
            {
                if (startPage == 0)
                {
                    startPage = 1;
                    endPage = startPage + PagerSize - 1;
                }
                else if (curPage >= endPage)
                {
                    if (curPage > endPage)
                    {
                        startPage = curPage;
                        endPage = startPage + PagerSize - 1;
                        if (endPage > totalPages)
                        {
                            endPage = totalPages;
                        }
                    }
                    else
                    {
                        startPage = PagerSize + 1;
                        if ((startPage + PagerSize - 1) > totalPages)
                        {
                            endPage = totalPages;
                        }
                        else
                        {
                            endPage = startPage + PagerSize - 1;
                        }
                    }
                }
            }
            else if (direction == "back")
            {
                if (startPage == 0)
                {
                    if (curPage == totalPages)
                    {
                        endPage = curPage;
                        startPage = curPage - PagerSize + 1;
                        if (startPage < 1)
                        {
                            startPage = 1;
                            if ((startPage + PagerSize - 1) > totalPages)
                            {
                                endPage = totalPages;
                            }
                            else
                            {
                                endPage = startPage + PagerSize - 1;
                            }
                        }
                    }
                    else
                    {
                        endPage = curPage;
                        startPage = curPage - PagerSize + 1;
                        if (startPage < 1)
                        {
                            startPage = 1;
                            if ((startPage + PagerSize - 1) > totalPages)
                            {
                                endPage = totalPages;
                            }
                            else
                            {
                                endPage = startPage + PagerSize - 1;
                            }
                        }
                    }
                }
                else if (totalPages > PagerSize)
                {
                    if (curPage <= startPage)
                    {
                        endPage = curPage;
                        startPage = curPage - PagerSize + 1;
                        if (startPage < 1)
                        {
                            startPage = 1;
                            if ((startPage + PagerSize - 1) > totalPages)
                            {
                                endPage = totalPages;
                            }
                            else
                            {
                                endPage = startPage + PagerSize - 1;
                            }
                        }
                    }
                }
            }
        }

        #endregion Navigation
    }
}
