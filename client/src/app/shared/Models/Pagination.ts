import { Product } from './Products';

// TODO: Make this generic and rename to PaginatedResponse
export interface Pagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    result: Product[];
}
