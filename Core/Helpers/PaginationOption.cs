namespace Core.Helpers
{
    public class PaginationOption
    {
        private const int _maxPageSize = 30;

        private int _pageSize = 15;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > _maxPageSize ? _maxPageSize : value;
            }
        }

        public int CurrentPage { get; set; } = 1;
    }
}