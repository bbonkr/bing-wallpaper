export interface PagedModel<TData = {}> {
    currentPage: number;
    limit: number;
    totalItems: number;
    totalPages: number;
    items: TData[];
}
