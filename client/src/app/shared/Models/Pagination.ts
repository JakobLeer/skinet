import { Product } from './Products';

export interface Pagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    result: Product[];
}
