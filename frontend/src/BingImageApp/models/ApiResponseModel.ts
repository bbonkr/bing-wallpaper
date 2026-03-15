export interface ApiResponseModelBase {
    statusCode: number;
    message?: string;
}

export interface ApiResponseModel<TData = {}> extends ApiResponseModelBase {
    statusCode: number;
    message?: string;
    data?: TData;
}
