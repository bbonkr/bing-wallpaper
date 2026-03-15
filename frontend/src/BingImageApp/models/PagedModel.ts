export interface PagedModel<TData = unknown> {
    currentPage: number;
    limit: number;
    totalItems: number;
    totalPages: number;
    items: TData[];
}
