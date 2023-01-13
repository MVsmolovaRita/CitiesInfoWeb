namespace CitiesInfoWeb.Services
{
    public class MetaDataPagination //(методанные страницы - (инфа о доп инфе) сервис для получения данных о странницы)
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }    
        public int MaxPageLength { get; set; }
        public int MaxItems { get; set; }
        public MetaDataPagination(int pageSize, int currentPage, int maxItems)
      
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            MaxItems = maxItems;
            MaxPageLength = (int)(Math.Ceiling(maxItems /(double) PageSize));
        }



    }
}
