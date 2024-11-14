namespace PrintingApi.Model
{
    public class PaginatedList<T>
    { 
        public List<T> Data { get; } 
        public int PageIndex { get; }   
        public int TotalPages { get; }

        public bool HasPreviousPage => PageIndex > 1; 
        public bool HasNextPage => PageIndex < TotalPages; 


        public PaginatedList(List<T> items,int pageIndex,int pageSize)
        {
            Data = items;
            PageIndex = pageIndex;
            TotalPages = pageSize;
        }   
    } 
}
