export interface ApiResponseModelBase {
    statusCode: number;
    message?: string;
}

export interface ApiResponseModel<TData = unknown> extends ApiResponseModelBase {
    statusCode: number;
    message?: string;
    data?: TData;
}
