import { Product } from "./Models/Products"

export interface Pagination {
    _List: Product[]
    pageNumber: number
    pageSize: number
    totalCount: number
}
