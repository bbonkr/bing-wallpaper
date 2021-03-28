export interface ApiResponseModelBase {
    statusCode: number;
    message?: string;
}

export interface ApiResponseModel<TData = {}> extends ApiResponseModelBase {
    statusCode: number;
    message?: string;
    data?: TData;
}

export interface ImageItemModel {
    id: string;
    fileName: string;
    fileExtension: string;
    fileSize: number;
    createdAt: Date;
}

export interface ImageItemDetailModel extends ImageItemModel {
    filePath: string;
    contentType: string;
}

export interface ImagesApiResponseModel
    extends ApiResponseModel<ImageItemModel[]> {}
