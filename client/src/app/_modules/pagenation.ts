export interface Pagenation {
    CurrentPage: number;
    ItemsPerPage: number;
    TotalItems: number;
    TotalPages: number;
}

export class PagenatedResult<T>{
    result: T;
    pagenation: Pagenation;
}